using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNpgsql;


public class Items 
{
    private string name;
    private int damage;
    private int defense;
    private int price;

    public Items(string name, int damage, int defense, int price) 
    {
        this.name = name;
        this.damage = damage;
        this.defense = defense;
        this.price = price;
    }

    public Items()
    {
        
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
