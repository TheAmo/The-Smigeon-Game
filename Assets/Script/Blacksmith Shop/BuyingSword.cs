using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyingSword: MonoBehaviour
{
    public int id;
    public float price;
    public GameObject player;

    private SpriteRenderer spriteRenderer;

    public Sprite spriteDefault;
    public Sprite spriteAttack;
    public Sprite spriteInteraction;

    public Sprite[] sprites;
    public Text textm;

    public void BuySword()
    {
        textm = GameObject.Find("Current Money").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        float money = player.GetComponent<Stats>().getGold();
        if (!(money < price))
        {
            player.GetComponent<Stats>().setGold((int)(money - price));
            player.GetComponent<Player>().equipement.setWeapon(id);
            textm.text = (money - price).ToString();
        }

        
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetComponentInChildren<PlayerChangeEquipment>().ChangeEquipement();

        
    }
}
