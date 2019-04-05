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
    // Start is called before the first frame update
    void Start()
    {
        SwordType = GameObject.Find("Sword Stats").GetComponent<Text>();

        player = GameObject.FindGameObjectWithTag("Player");
        money = player.GetComponent<Stats>().getGold();
        CurrentMoney.GetComponent<Text>().text = money.ToString();


        db = new DataBaseSmi();

        Debug.Log(db.getMaterialName(player.GetComponent<Equipement>().getWeapon()));

        UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("BlacksmithShop"));


        button = GameObject.FindGameObjectsWithTag("Button");


    List<Items> items = db.getAllMaterials();
        for (int i = 0; i < 7; i++)
        {
            button[i].GetComponentInChildren<Text>().text = db.getMaterialName(i);
        }

        SwordType.text = db.getMaterialName(player.GetComponent<Player>().equipement.getWeapon());       
    }

    // Update is called once per frame
    void Update()
    {
      if(SwordType.text != db.getMaterialName(player.GetComponent<Player>().equipement.getWeapon()))
        {
            SwordType.text = db.getMaterialName(player.GetComponent<Player>().equipement.getWeapon());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShopCamera.enabled = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("UI", UnityEngine.SceneManagement.LoadSceneMode.Additive);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("SampleScene"));
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("BlacksmithShop");
        }
       
    }
   
}
