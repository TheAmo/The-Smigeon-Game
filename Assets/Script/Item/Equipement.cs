using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipement:MonoBehaviour
{
    public int weaponType = 0;
    public int armorType = 0;

    public Equipement()
    {
        
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
