using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipement:MonoBehaviour
{
    public int weaponType=1;
    public int armorType=1;

    public Equipement()
    {
        armorType = 1;
        weaponType = 1;

        Debug.Log("Generating new Equipment");
    }

    public Equipement(int weapon, int armor)
    {
        armorType = armor;
        weaponType = weapon;
   
        Debug.Log("Generating Equipment W:" + weapon + " A:" + armor);
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
