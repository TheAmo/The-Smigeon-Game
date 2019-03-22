using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntryDB
{
    private string name;
    private string className;
    private int experience;
    private float[] position;
    private int gold;

    public PlayerEntryDB(string name, string className, int experience, float[] position, int gold)
    {
        this.name = name;
        this.className = className;
        this.experience = experience;
        this.position = position;
        this.gold = gold;
    }

    
}
