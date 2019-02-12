using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipement
{
    public int weaponType;
    public int armorType;

    public Equipement(int weapon, int armor)
    {
        armor = armorType;
        weapon = weaponType;
    }
    public void setWeapon(int weapon)
    {
        weaponType = weapon;
    }
    public int getWeapon()
    {
        return weaponType;
    }
    public void setArmor(int armor)
    {
        armorType = armor;
    }
    public int getArmor()
    {
        return armorType;
    }
}
