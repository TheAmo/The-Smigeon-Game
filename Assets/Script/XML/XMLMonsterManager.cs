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

    //List of Monster
    public MonsterDatabase monsterDB;

    //Load function
    public void LoadMonster()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(MonsterDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/monster_data.xml", FileMode.OpenOrCreate);

        monsterDB = serializer.Deserialize(stream) as MonsterDatabase;
        stream.Close();

        foreach(MonsterEntry monster in monsterDB.monsterList)
        {
            Debug.Log("Loaded Monster " + monster.name);
        }
    }
}

[System.Serializable]
public class MonsterEntry               //What will be populating the list
{
    public string name;
    public int hitpoint;

    public int armorclass;
    public int attackbonus;

    public int damagebonus;

    public int damagedice;
}

[System.Serializable]
public class MonsterDatabase            //What gets made into a xml file for us
{
    public List<MonsterEntry> monsterList = new List<MonsterEntry>();
}