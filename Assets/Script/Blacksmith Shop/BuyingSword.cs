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
        //Blacksmith shop
        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName("BlacksmithShop").isLoaded == true)
        {
            bsShopIsOpen = true;
            price = db.getPrice(id, "material");
            itemMaterial = player.GetComponent<Player>().equipement.getWeapon();
            itemMaterial++;

        }
        else
        //Armor shop
        {
            price = db.getPrice(id, "armor");
            itemMaterial = player.GetComponent<Player>().equipement.getArmor();
            itemMaterial++;
        }
        if(itemMaterial == id)
            //Item is already bought
        {
            errorText.GetComponent<Text>().text = "You already have this item";
        }
        else if(money >= price)
        {
            if(bsShopIsOpen == true)
                //Blacksmith shop
            {
                equipementType = GameObject.Find("Sword Stats").GetComponent<Text>();

                //remove gold
                player.GetComponent<Stats>().changeGoldByValue(price*-1);

                //Set player weapon and damage
                player.GetComponent<Player>().equipement.setWeapon(id - 1);
                player.GetComponent<Stats>().setAttackBonus(db.getMaterialDamage(id - 1));

                //show gold amount
                textm.text = (money - price).ToString();

                //show current sword
                equipementType.text = db.getItemName(id - 1, "material");

            }
            else if(UnityEngine.SceneManagement.SceneManager.GetSceneByName("ArmorShop").isLoaded == true)
                //Armor shop
            {
                equipementType = GameObject.Find("Armor Stats").GetComponent<Text>();

                //remove gold
                player.GetComponent<Stats>().changeGoldByValue(price * -1);

                //Change armor and defense
                player.GetComponent<Player>().equipement.setArmor(id - 1);
                player.GetComponent<Stats>().setArmorClass(db.getArmorDefense(id - 1));

                //Show new gold amount
                textm.text = (money - price).ToString();

                //Show current armor
                equipementType.text = db.getItemName(id - 1, "armor");
            }
        }
        else
        {
            errorText.GetComponent<Text>().text = "You don't have enough money to buy this";
        }
        //Call the function to change the sprite of the player
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetComponentInChildren<PlayerChangeEquipment>().ChangeEquipement();        
    }
}
