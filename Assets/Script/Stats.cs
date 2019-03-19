using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats: MonoBehaviour
{
    public int hp = 10;
    public int ac = 15;
    public int att = 5;

    private int strenght=10;
    private int dexterity=10;
    private int constitution=10;
    private int intelligence=10;
    private int wisdom=10;
    private int charisma=10;

    public bool getDamage(int damage)
    {
        hp = hp - damage;
        Debug.Log("Dealt " + damage+"hp left "+hp);
        if (hp <= 0)
        {
            return (true);
        }
        else return (false);
    }
    public int dealDamage()
    {
       return(Random.Range(1, 6) + att);
    }

}


