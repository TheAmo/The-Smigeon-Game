using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Monster
{
    [XmlAttribute("name")]
    public string name;

    [XmlElement("Hit_Point")]
    public int hitpoint;

    [XmlElement("Armor_Class")]
    public int armorclass;

    [XmlElement("Attack_Bonus")]
    public int attackbonus;

    [XmlElement("Damage_Bonus")]
    public int damagebonus;

    [XmlElement("Damage_Die")]
    public int damagedie;


    /*===================================================================================================================
    * Damage/ Damage Calculator
    * 
    ===================================================================================================================*/
    //Give Attack
    public bool getAttack(int attackBonus)
    {
        attackBonus += Random.Range(1, 20);

        if (armorclass <= attackBonus)
        {
            Debug.Log("Attack HIT: " + attackBonus + "  VS Armor: " + armorclass);
            return (true); //Attack Touch
        }
        else
        {
            Debug.Log("Attack MISS: " + attackBonus + "  VS Armor: " + armorclass);
            return (false); //Attack Miss
        }
    }

    //Receive Damage
    public bool ReceiveDamage(int damage)
    {
        hitpoint -= damage;
        Debug.Log("Dealt: " + damage + "   HP left: " + hitpoint);
        if (hitpoint <= 0)
        {
            return (true); //Is dead
        }
        else return (false); //Is alive
    }

    //Calculate Damage
    public int CalculateDamage()
    {
        return (Random.Range(1, damagedie) + damagebonus);
    }
}
