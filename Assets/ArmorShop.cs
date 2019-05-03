using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorShop : MonoBehaviour
{
    public Camera ShopCamera;
    public GameObject player;
    private Text ArmorType;
    private string currentText;
    private DataBaseSmi db;

    public GameObject[] button;
    public List<Items> listItem;
    // Start is called before the first frame update
    void Start()
    {
        ArmorType = GameObject.Find("Armor Stats").GetComponent<Text>();

        player = GameObject.FindGameObjectWithTag("Player");


        db = new DataBaseSmi();
        //Set the shop scene to be active


        button = GameObject.FindGameObjectsWithTag("Button");
        listItem = db.getAllArmors();
        foreach (Items item in listItem) //Assign the name, price and defense to the button
        {
            button[item.id - 1].GetComponentInChildren<Text>().text = item.name + " " + item.price + "gp  +" + item.damage_defense;
        }

        ArmorType.text = db.getItemName(player.GetComponent<Player>().equipement.getArmor(), "armor");//Show the name of the current armor
    }

    // Update is called once per frame
    void Update()
    {
      

        if (Input.GetKeyDown(KeyCode.Escape))
        { /* when the player press escape
         the shop camera is disabled, the UI scene is loaded, the shop is unloaded
         and the main Scene is activated*/
            GameObject.Find("ShopArmor").SetActive(false);

        }

    }

}
