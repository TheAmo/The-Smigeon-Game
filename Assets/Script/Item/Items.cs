using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNpgsql;


public class Items
{
    public int id;
    public string material;
    public int damage;
    public int defense;
    public double price;

    public Items(int id, string material, int damage, int defense, double price)
    {
        this.id = id;
        this.material = material;
        this.damage = damage;
        this.defense = defense;
        this.price = price;
    }


    public Items()
    {

    }

    public int getId()
    {
        return id;
    }

    public string getMaterial()
    {
        return material;
    }

    public int getDamage()
    {
        return damage;
    }

    public int getDefense()
    {
        return defense;
    }

    public double getPrice()
    {
        return price;
    }
}