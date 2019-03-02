using System.Collections;
using UnityEngine;

using System.Collections.Generic;   //Let us use Lists
using System.Xml;                   //Basix xml attribute
using System.Xml.Serialization;     //Access xmlserializer
using System.IO;                    //file management

public class XMLMonsterManager: MonoBehaviour
{
    public static XMLMonsterManager ins;

    void Awake()
    {
        ins = this;
    }

    //List of items
    public MonsterDatabase monsterDB;
}

[System.Serializable]
public class MonsterEntry               //What will be populating the list
{
    public string name;
    public int hitpoint;

    public int armorclass;
    public int attackbonus;

    public int damagebonus;

    public int damagedie;
}

[System.Serializable]
public class MonsterDatabase            //What gets made into a xml file for us
{
    public List<MonsterEntry> list = new List<MonsterEntry>();
}