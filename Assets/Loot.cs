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
    public void DropType(string name)
    {
        if(name.Contains("Goblin"))
        {
            GoblinDrop();
        }
    }
    public string GoblinDrop()
    {
        string drop = ("");
        Debug.Log("tyl");
        return drop;
    }
 
}
