using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyingSword: MonoBehaviour
{
    public int id;
    private int price;

    public GameObject player;

    private Shop shop;
    public Text textm;

    private int itemMaterial;

    public int weapon;
    public Text equipementType;
    private bool bsShopIsOpen;
    private DataBaseSmi db;
    public Text errorText;
    public void BuySword()
    {
        shop = new Shop();
        textm = GameObject.Find("Current Money").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        errorText = GameObject.Find("Error").GetComponent<Text>();
        errorText.GetComponent<Text>().text = "";


        db = new DataBaseSmi();
        float money = player.GetComponent<Stats>().getGold();

        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName("BlacksmithShop").isLoaded == true)
        {
            bsShopIsOpen = true;
            price = db.getPrice(id, "material");
            itemMaterial = player.GetComponent<Player>().equipement.getWeapon();
            itemMaterial++;

        }
        else
        {
            price = db.getPrice(id, "armor");
            itemMaterial = player.GetComponent<Player>().equipement.getArmor();
            itemMaterial++;
        }
        Debug.Log(itemMaterial + " " + id);
        if(itemMaterial == id)
        {
            errorText.GetComponent<Text>().text = "You already have this item";
        }
        else if(money >= price)
        {
            if(bsShopIsOpen == true)
            {
                
                equipementType = GameObject.Find("Sword Stats").GetComponent<Text>();
                player.GetComponent<Stats>().changeGoldByValue(price*-1);
                player.GetComponent<Player>().equipement.setWeapon(id - 1);
                textm.text = (money - price).ToString();

                equipementType.text = db.getItemName(id - 1, "material");
                Debug.Log(db.getItemName(player.GetComponent<Player>().equipement.getWeapon(), "material"));
            }else if(UnityEngine.SceneManagement.SceneManager.GetSceneByName("ArmorShop").isLoaded == true)
            {
                equipementType = GameObject.Find("Armor Stats").GetComponent<Text>();
                Debug.Log(id);
                player.GetComponent<Stats>().changeGoldByValue(price * -1);
                player.GetComponent<Player>().equipement.setArmor(id - 1);
                Debug.Log(player.GetComponent<Player>().equipement.getWeapon());
                textm.text = (money - price).ToString();

                equipementType.text = db.getItemName(id - 1, "armor");
                Debug.Log(db.getItemName(player.GetComponent<Player>().equipement.getArmor(), "material"));
            }
        }
        else
        {
            errorText.GetComponent<Text>().text = "You don't have enough money to buy this";
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetComponentInChildren<PlayerChangeEquipment>().ChangeEquipement();        
    }
}
