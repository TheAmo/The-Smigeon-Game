using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    
    public string EnemyName;
    string drop;
    public Loot()
    {

    }

    public Loot(string name)
    {
        EnemyName = name;
    }
    public int DropType(string name)
    {
        int gold = 0;
        if (name.Contains("Goblin"))
        {
            gold = GoblinDrop();
        }
        return gold;
    }
    public int GoblinDrop()
    {
        int gold = 0;
        int rand = Random.Range(1, 100);
        if (rand == 100)
        {
            gold = 10000;
        }
        if (rand < 99 && rand > 80)
        {
            gold = 500;
        }
        if (rand < 75 && rand > 50)
        {
            gold = 100;
        }
        else
        {
            gold = 10;
        }
        Debug.Log(gold);
        return gold;
    }

}

