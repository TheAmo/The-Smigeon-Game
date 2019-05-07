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
        player = GameObject.FindGameObjectWithTag("Player");
        textm = GameObject.Find("Current Money").GetComponent<Text>();
        money = player.GetComponent<Player>().stats.getGold();
        moneys = money.ToString();
        textm.text = moneys;
    }

    // Update is called once per frame
    void Update()
    {
        /*   if (player.name != "P1")
           {
               if (player.active == true)
               {
                   player = GameObject.FindGameObjectWithTag("Player");
                   player.name = "P1";
                   money = player.GetComponent<Stats>().getGold();
                   moneys = money.ToString();
               }

           }*/



        player = GameObject.FindGameObjectWithTag("Player");
        textm = GameObject.Find("Current Money").GetComponent<Text>();
        money = player.GetComponent<Player>().stats.getGold();
        textm.text = money.ToString();
        
        
  
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
