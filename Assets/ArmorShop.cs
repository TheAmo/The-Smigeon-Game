using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorShop : MonoBehaviour
{
    public Camera ShopCamera;
    public GameObject player;
    public Text CurrentMoney;
    private float money;
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
        money = player.GetComponent<Stats>().getGold();
        CurrentMoney.GetComponent<Text>().text = money.ToString();


        db = new DataBaseSmi();
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("ArmorShop"));


        button = GameObject.FindGameObjectsWithTag("Button");


        listItem = db.getAllArmors();
        foreach (Items item in listItem)
        {
            button[item.id - 1].GetComponentInChildren<Text>().text = item.name + " " + item.price + "gp  +" + item.damage_defense;
        }

        ArmorType.text = db.getItemName(player.GetComponent<Player>().equipement.getArmor(), "armor");
    }

    // Update is called once per frame
    void Update()
    {
      

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShopCamera.enabled = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("UI", UnityEngine.SceneManagement.LoadSceneMode.Additive);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("SampleScene"));
            Money money = new Money();
            money.changeMoney(player.GetComponent<Player>().stats.getGold());
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("ArmorShop");
        }

    }

}
