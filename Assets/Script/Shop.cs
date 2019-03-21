using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject PlayerCamera;
    public Camera ShopCamera;
    public GameObject player;
    public Text CurrentMoney;
    private float money;
    // Start is called before the first frame update
    void Start()
    { 
        money = player.GetComponent<Stats>().getGold();
        CurrentMoney.GetComponent<Text>().text = money.ToString();
        //PlayerCamera = GameObject.Find("Maria");
        PlayerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //PlayerCamera.enabled = false;
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("BlacksmithShop"));

    }

    // Update is called once per frame
    void Update()
    {
      

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShopCamera.enabled = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("UI", UnityEngine.SceneManagement.LoadSceneMode.Additive);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("SampleScene"));
            Debug.Log("2");
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("BlacksmithShop");

           
        }
    }
}
