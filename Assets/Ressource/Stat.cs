using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public class CharacterStats
    {
        int strenght;
        int dexterity;
        int constitution;
        int intelligence;
        int wisdom;
        int charisma;
        public Dictionary<string, int> StatsList = new Dictionary<string, int>();
        CharacterStats(int str, int dex, int con, int intel, int wis, int cha)
        {
            str = strenght;
            con = constitution;
            dex = dexterity;
            intel = intelligence;
            wis = wisdom;
            cha = charisma;
            StatsList.Add("str", str);
            StatsList.Add("dex", dex);
            StatsList.Add("con", con);
            StatsList.Add("intel", intel);
            StatsList.Add("wis", wis);
            StatsList.Add("cha", cha);


        }
        void setValue(string statname, int value)
        {
            StatsList[statname] = value;
        }
        int getValue(string statname)
        {
            int value;

            return StatsList[statname];
        }
        int getModifier(string statname)
        {
            int modifier;
            int value;
            value = StatsList[statname];
            modifier = Mathf.FloorToInt(value / 2 - 5);
            return modifier;
        }
        int DealDamage(string statname)
        {
            System.Random rand = new System.Random();
            int damage;
            int modifier = getModifier(statname);
            damage = rand.Next(1, 11) + modifier;//La valeur du random doit pouvoir être changer
            return damage;
        }
    }
}

