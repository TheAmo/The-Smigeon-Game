using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject player;
    public Text CurrentMoney;
    private float money;
    // Start is called before the first frame update
    void Start()
    { 
        money = player.GetComponent<Stats>().getGold();
        CurrentMoney.GetComponent<Text>().text = money.ToString();
        player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("BlacksmithShop");
           UnityEngine.SceneManagement.SceneManager.LoadScene("UI", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
    }
}
