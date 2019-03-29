using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNpgsql;


public class Items
{
    private int id;
    private string name;
    private int damage;
    private int defense;
    private double price;
    private string material;

    public Items(int id, string name, int damage, int defense, double price)
    {
        this.id = id;
        this.name = name;
        this.damage = damage;
        this.defense = defense;
        this.price = price;
    }
    public Items(int id, string name, int damage, int defense, double price, string material)
    {
        this.id = id;
        this.name = name;
        this.damage = damage;
        this.defense = defense;
        this.price = price;
        this.material = material;
    }
    public Items(string name, int damage, int defense, double price, string material)
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
        return id;
    }

    public string getName()
    {
        return name;
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

    public string getMaterial()
    {
        return material;
    }

}
