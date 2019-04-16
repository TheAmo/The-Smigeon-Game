using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Money : MonoBehaviour
{
    private string moneys;
    public float money;
    public Text textm;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        textm = GameObject.Find("Current Money").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        money = player.GetComponent<Stats>().getGold();
        moneys = money.ToString();
        textm.text = moneys;
    }

    // Update is called once per frame
    void Update()
    {
        
        money = player.GetComponent<Stats>().getGold();
        moneys = money.ToString();
        if (textm.text != moneys)
        {
            textm = GameObject.Find("Current Money").GetComponent<Text>();
            textm.text = moneys;
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            money += 1;
            moneys = money.ToString();
            Debug.Log(money);
        }
       
    }
    public void changeMoney(int currentGold)
    {
        textm = GameObject.Find("Current Money").GetComponent<Text>();
        moneys = currentGold.ToString();
        textm.text = moneys;
    }
}
