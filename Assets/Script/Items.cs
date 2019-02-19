using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNpgsql;


public class Items 
{
    private int id { get; set; }
    private string name { get; set; }
    private int damage { get; set; }
    private int defense { get; set; }
    private int price { get; set; }
    private string material { get; set; }

    public Items(int id, string name, int damage, int defense, int price) 
    {
        this.id = id;
        this.name = name;
        this.damage = damage;
        this.defense = defense;
        this.price = price;
    }
    public Items(int id, string name, int damage, int defense, int price, string material)
    {
        this.id = id;
        this.name = name;
        this.damage = damage;
        this.defense = defense;
        this.price = price;
        this.material = material;
    }
    public Items(string name, int damage, int defense, int price, string material)
    {
        this.name = name;
        this.damage = damage;
        this.defense = defense;
        this.price = price;
        this.material = material;
    }

    public Items()
    {
        
    }

    public int getId()
    {
        return this.id;
    }

    public string getName()
    {
        return this.name;
    }

    public int getDamage()
    {
        return this.damage;
    }

    public int getDefense()
    {
        return this.defense;
    }

    public int getPrice()
    {
        return this.price;
    }

    public string getMaterial()
    {
        return this.material;
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
