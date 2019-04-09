using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyingSword: MonoBehaviour
{
    public int id;
    private float price;
    public GameObject player;
    private Shop shop;
    public Text textm;
    public int weapon;
    public Text equipementType;

    private DataBaseSmi db;

    public void BuySword()
    {
        shop = new Shop();
        textm = GameObject.Find("Current Money").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        db = new DataBaseSmi();
        price = db.getPrice(id, "material");

        float money = player.GetComponent<Stats>().getGold();
        if (money >= price)
        {
            if(UnityEngine.SceneManagement.SceneManager.GetSceneByName("BlacksmithShop").isLoaded == true)
            {
                equipementType = GameObject.Find("Sword Stats").GetComponent<Text>();
                Debug.Log(id);
                player.GetComponent<Stats>().setGold((int)(money - price));
                player.GetComponent<Player>().equipement.setWeapon(id - 1);
                Debug.Log(player.GetComponent<Player>().equipement.getWeapon());
                textm.text = (money - price).ToString();

                equipementType.text = db.getItemName(id, "material");
                Debug.Log(db.getItemName(player.GetComponent<Player>().equipement.getWeapon(), "material"));
            }else if(UnityEngine.SceneManagement.SceneManager.GetSceneByName("ArmorShop").isLoaded == true)
            {
                equipementType = GameObject.Find("Armor Stats").GetComponent<Text>();
                Debug.Log(id);
                player.GetComponent<Stats>().setGold((int)(money - price));
                player.GetComponent<Player>().equipement.setArmor(id - 1);
                Debug.Log(player.GetComponent<Player>().equipement.getWeapon());
                textm.text = (money - price).ToString();

                equipementType.text = db.getItemName(id, "material");
                Debug.Log(db.getItemName(player.GetComponent<Player>().equipement.getArmor(), "material"));
            }
            

        }


        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetComponentInChildren<PlayerChangeEquipment>().ChangeEquipement();

        
    }
}
