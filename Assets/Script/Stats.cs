using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats: MonoBehaviour
{
    public int hp = 10;
    public int ac = 15;
    public int att = 5;
    public int strenght=10;
    public int dexterity=10;
    public int constitution=10;
    public int intelligence=10;
    public int wisdom=10;
    public int charisma=10;

    public bool getDamage(int damage)
    {
        hp = hp - damage;
        Debug.Log("Dealt " + damage+"hp left "+hp);
        if (hp <= 0)
        {
            Debug.Log("enemy killed");
            return (true);
        }
        else return (false);
    }
    public int dealDamage()
    {
       return(Random.Range(1, 6) + att);
    }

}


