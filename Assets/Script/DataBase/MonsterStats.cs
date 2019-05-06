using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public int id;
    public string name;
    public int xp;
    public int hitpoint;
    public int armorclass;
    public int attackbonus;
    public int damagebonus;
    public int damagedice;

    public MonsterStats(int id, string name, int xp, int hitpoint, int armorclass, int attackbonus, int damagebonus, int damagedice)
    {
        this.id = id;
        this.name = name;
        this.xp = xp;
        this.hitpoint = hitpoint;
        this.armorclass = armorclass;
        this.attackbonus = attackbonus;
        this.damagebonus = damagebonus;
        this.damagedice = damagedice;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
