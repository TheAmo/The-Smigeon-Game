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

    void getDamage(int damage)
    {
        hp -= damage;
        if (damage<0)
        {
            Destroy(this, 0);
        }
    }
    public void dealDamage(Stats receiver)
    {
        receiver.getDamage(5);
        /*
        int roll20 = Random.Range(1, 20)+att;
        if (roll20>=receiver.ac)
        {
            receiver.getDamage(Random.Range(1, 6) + Mathf.FloorToInt((strenght - 10) / 2));

        }
        */
    }

}


