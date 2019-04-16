using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Camera ShopCamera;
    public GameObject player;
    public Text CurrentMoney;
    private float money;
    private Text SwordType;
    private string currentText;
    private DataBaseSmi db;
    public GameObject[] button;
    public List<Items> listItem;
    // Start is called before the first frame update
    void Start()
    {
        SwordType = GameObject.Find("Sword Stats").GetComponent<Text>();

        player = GameObject.FindGameObjectWithTag("Player");
        money = player.GetComponent<Stats>().getGold();
        CurrentMoney.GetComponent<Text>().text = money.ToString();


        db = new DataBaseSmi();
        //Set the shop scene to be active
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("BlacksmithShop"));


        button = GameObject.FindGameObjectsWithTag("Button");


        listItem = db.getAllMaterials();
        foreach (Items item in listItem)//Assign the name, price and damage to the button
        {
            button[item.id - 1].GetComponentInChildren<Text>().text = item.name + " " + item.price + "gp  +" + item.damage_defense;
        }

        SwordType.text = db.getItemName(player.GetComponent<Player>().equipement.getWeapon(), "material"); //Show the name of the current sword
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        /* when the player press escape
         the shop camera is disabled, the UI scene is loaded, the shop is unloaded
         and the main Scene is activated*/
        {

            ShopCamera.enabled = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("UI", UnityEngine.SceneManagement.LoadSceneMode.Additive);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("SampleScene"));
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("BlacksmithShop");
            Money money = new Money();
            money.changeMoney(player.GetComponent<Player>().stats.getGold());
            
        }

    }

}
