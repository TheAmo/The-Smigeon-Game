using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNpgsql;


public class Items
{
    public int id;
    public string name;
    public int price;
    public int damage_defense;

    public Items(int id, string name, int damage_defense, int price)
    {
        this.id = id;
        this.name = name;
        this.damage_defense = damage_defense;
        this.price = price;
    }

    public Items()
    {

    }

    public int getId()
    {
        return id;
    }

    public string getName()
    {
        return name;
    }

    public int getDamageDefense()
    {
        return damage_defense;
    }

    public double getPrice()
    {
        return price;
    }
}