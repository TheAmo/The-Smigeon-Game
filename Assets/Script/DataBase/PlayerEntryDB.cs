using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntryDB
{
    public string name;
    public string className;
    public int experience;
    public float[] position;
    public int gold;
    public string weapon;
    public string armor;

    public PlayerEntryDB(string name, string className, int experience, float[] position, int gold)
    {
        this.name = name;
        this.className = className;
        this.experience = experience;
        this.position = position;
        this.gold = gold;
    }
    public PlayerEntryDB(string name, int experience, string weapon, string armor, float[] position, int gold)
    {
        this.name = name;
        this.experience = experience;
        this.weapon = weapon;
        this.armor = armor;
        this.position = position;
        this.gold = gold;
    }


}
