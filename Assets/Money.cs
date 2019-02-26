using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Money : MonoBehaviour
{
    public string moneys;
    public float money;
    Text textm;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        textm = this.GetComponent<Text>();
        moneys = player.GetComponent<Stats>().getGold().ToString();
        Debug.Log(textm.text);
    }
    
    // Update is called once per frame
    void Update()
    {
        //moneys = player.GetComponent<Stats>().getGold().ToString();

        if(textm.text != moneys)
        {
            textm.text = moneys;
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            money += 1;
            moneys = money.ToString();
            Debug.Log (money);
        }
    }
}