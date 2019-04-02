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
    private Text text;
    public GameObject[] button;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       // money = player.GetComponent<Stats>().getGold();
        CurrentMoney.GetComponent<Text>().text = money.ToString();
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("BlacksmithShop"));
        DataBaseSmi db = new DataBaseSmi();

    //Debug.Log(db.getName("material", 1));

    button = GameObject.FindGameObjectsWithTag("Button");
    List<Items> items = db.getAllMaterials();

        for (int i = 0; i < 7; i++)
        {

            button[i].GetComponentInChildren<Text>().text = db.getName(i);



            string name = "Sword " + i;
            Debug.Log(name);
            Debug.Log(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
      

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShopCamera.enabled = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("UI", UnityEngine.SceneManagement.LoadSceneMode.Additive);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("SampleScene"));
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("BlacksmithShop");
        }
    }
}
