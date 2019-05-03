using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Stats : MonoBehaviour
{
    /*===================================================================================================================
     * Stats
     * 
     ===================================================================================================================*/

    public string name;

    //Ability

    private int m_strength;
    private int m_dexterity;
    private int m_constitution;
    private int m_intelligence;
    private int m_wisdom;
    private int m_charisma;

    //Ability Modifier
    private int m_strengthModifier;
    private int m_dexterityModifier;
    private int m_constitutionModifier;
    private int m_intelligenceModifier;
    private int m_wisdomModifier;
    private int m_charismaModifier;

    //Hp
    
    private int m_hitPoint;
    
    private int m_mana = 100;

    //Attack / defence
    private int m_armorClass;
    private int m_attackBonus;

    //Damage
    private int m_damageBonus;
    private int m_damageDie;

    //Gold
    private int m_gold;

    //Experience
    private int m_experience;

    /*===================================================================================================================
     * Constructor
     * 
     ===================================================================================================================*/
    //Default Constructor: when you create a new character
    public Stats()
    {
        Debug.Log("Generated Default Stats");

        //Random Ability roll
        setStrength(10);
        setDexterity(10);
        setConstitution(10);
        setIntelligence(10);
        setWisdom(10);
        setCharisma(10);

        //Calculate Modifier
        calculateAllModifier();

        //Attack-Armor
        setArmorClass(10 + getDexterityModifier());
        setAttackBonus(getDexterityModifier());
        setDamageBonus(getStrengthModifier());

        setDamageDie(getDamageDie());

        //HitPoint
        setHitPoint(10 + getConstitutionModifier());

        //Gold;ge
        setGold(0);

        //Experience
        setExperience(0);

        Debug.Log("Stats Generated: Str: " + m_strength + "Dex: " + m_dexterity + "Con: " + m_constitution + "Int: " + m_intelligence + "Wis: " + m_wisdom + "Cha: " + m_charisma);
    }
    /*
    public Stats(PlayerEntryDB player, ClassEntry playerClass)
    {
        Debug.Log("Generating from player: " + player.name);

        //Ability roll
        setStrength(playerClass.baseAbility[0]);
        setDexterity(playerClass.baseAbility[1]);
        setConstitution(playerClass.baseAbility[2]);
        setIntelligence(playerClass.baseAbility[3]);
        setWisdom(playerClass.baseAbility[4]);
        setCharisma(playerClass.baseAbility[5]);

        //Calculate Modifier
        calculateAllModifier();

        //Attack-Armor
        setArmorClass(10 + getDexterityModifier());
        setAttackBonus(getDexterityModifier());
        setDamageBonus(getStrengthModifier());

        setDamageDie(playerClass.damageDice);

        //HitPoint
        setHitPoint(playerClass.hitDice + getConstitutionModifier());

        //Gold
        setGold(player.gold);

        //Experience
        setExperience(player.experience);

        Debug.Log("Stats Generated Succesfully: Str: " + m_strength + " Dex: " + m_dexterity + " Con: " + m_constitution + " Int: " + m_intelligence + " Wis: " + m_wisdom + " Cha: " + m_charisma + " On position (" + player.position[0]+","+player.position[1] + ")");
    }
    */
    /*===================================================================================================================
    * Stats Calculator
    * 
    ===================================================================================================================*/
    private int calculateModifier(int abilityScore) { return (Mathf.RoundToInt(Mathf.Floor((m_strength - 10) / 2))); }



    private void calculateAllModifier()
    {
        //Ability Modifier
        m_strengthModifier = calculateModifier(m_strength);
        m_dexterityModifier = calculateModifier(m_dexterity);
        m_constitutionModifier = calculateModifier(m_constitution);
        m_intelligenceModifier = calculateModifier(m_intelligence);
        m_wisdomModifier = calculateModifier(m_wisdom);
        m_charismaModifier = calculateModifier(m_charisma);
    }

    private void ajustToLevel(int level)
    {
        m_hitPoint = (10 + m_constitutionModifier) + ((level - 1) * (6 + m_constitutionModifier));

        m_damageBonus = m_strengthModifier + Mathf.RoundToInt(Mathf.Floor(level / 2));
        m_attackBonus = m_dexterityModifier + Mathf.RoundToInt(Mathf.Floor(level / 2));
    }

    /*===================================================================================================================
     * Getter
     * 
     ===================================================================================================================*/
    //Ability
    public int getStrength() { return (m_strength); }
    public int getDexterity() { return (m_dexterity); }
    public int getConstitution() { return (m_constitution); }
    public int getIntelligence() { return (m_intelligence); }
    public int getWisdom() { return (m_wisdom); }
    public int getCharisma() { return (m_charisma); }

    //Ability Modifier
    public int getStrengthModifier() { return (m_strengthModifier); }
    public int getDexterityModifier() { return (m_dexterityModifier); }
    public int getConstitutionModifier() { return (m_constitutionModifier); }
    public int getIntelligenceModifier() { return (m_intelligenceModifier); }
    public int getWisdomModifier() { return (m_wisdomModifier); }
    public int getCharismaModifier() { return (m_charismaModifier); }

    //Hp
    public int getHitPoint() { return (m_hitPoint); }
    public int getMana() { return (m_mana); }

    //Attack / defence
    public int getArmorClass() { return (m_armorClass); }
    public int getAttackBonus() { return (m_attackBonus); }

    //Damage
    public int getDamageBonus() { return (m_damageBonus); }
    public int getDamageDie() { return (m_damageDie); }

    //Gold
    public int getGold() { return (m_gold); }

    //Experience
    public int getExperience() { return (m_experience); }

    /*===================================================================================================================
     * Setter
     * 
     ===================================================================================================================*/
    //Ability
    public void setStrength(int strength) { m_strength = strength; }
    public void setDexterity(int dexterity) { m_dexterity = dexterity; }
    public void setConstitution(int constitution) { m_constitution = constitution; }
    public void setIntelligence(int intelligence) { m_intelligence = intelligence; }
    public void setWisdom(int wisdom) { m_wisdom = wisdom; }
    public void setCharisma(int charisma) { m_charisma = charisma; }

    //Hp
    public void setHitPoint(int hitPoint) {
        m_hitPoint = hitPoint;
       
    }
    public void setMana(int mana) {
        m_mana = mana;
        
    }

    //Attack / defence
    public void setArmorClass(int armorClass) { m_armorClass = armorClass; }
    public void setAttackBonus(int attackBonus) { m_attackBonus = attackBonus; }

    //Damage
    public void setDamageBonus(int damageBonus) { m_damageBonus = damageBonus; }
    public void setDamageDie(int damageDie) { m_damageDie = damageDie; }

    //Gold

    public void setGold(int gold) {
        m_gold = gold;
        
    }


    public void changeGoldByValue(int gold) {
        m_gold += gold;
        
    }

    //Experience
    public void setExperience(int experience) { m_experience = experience; }
    public void changeExperienceByValue(int experience) { m_experience += experience; }
}


