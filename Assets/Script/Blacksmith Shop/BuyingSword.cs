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
        float money = player.GetComponent<Player>().stats.getGold();
        //Blacksmith shop
        if (GameObject.Find("Blacksmith").transform.Find("Shop").gameObject.activeSelf == true)
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
                player.GetComponent<Player>().stats.changeGoldByValue(price*-1);

                //Set player weapon and damage
                player.GetComponent<Player>().equipement.setWeapon(id - 1);
                player.GetComponent<Player>().stats.setAttackBonus(db.getMaterialDamage(id - 1));

                //show gold amount
                textm.text = (money - price).ToString();

                //show current sword
                equipementType.text = db.getItemName(id - 1, "material");

            }
            else 
                //Armor shop
            {
                equipementType = GameObject.Find("Armor Stats").GetComponent<Text>();

                //remove gold
                player.GetComponent<Player>().stats.changeGoldByValue(price * -1);

                //Change armor and defense
                player.GetComponent<Player>().equipement.setArmor(id - 1);
                player.GetComponent<Player>().stats.setArmorClass(db.getArmorDefense(id - 1));

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
