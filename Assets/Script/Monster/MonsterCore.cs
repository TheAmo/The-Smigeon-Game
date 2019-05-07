using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCore : MonoBehaviour
{
    /*===================================================================================================================
         * Attribute
         * 
         ===================================================================================================================*/
    private int m_monster_id;
    private string m_name;
    private int m_hitPoint;
    private int m_armorClass;
    private int m_attackBonus;
    private int m_damageBonus;
    private int m_damageDice;
    
    public Sprite spriteKill;
    public Sprite spriteLootbag;
    private SpriteRenderer srLootbag;
    public SpriteRenderer spriteRenderer;

    public BoxCollider2D bc2d;

    public GameObject lootbag;

    private bool dead;

    //private MonsterDatabase monsterDatabase;
   
 
    /*===================================================================================================================
    * Start
    * 
    ===================================================================================================================*/
    public void ininitialiseMonster(int monster_id, int positionX, int positionY)
    {
        Debug.Log("Constructeur du Monster");
        m_monster_id = monster_id;
        m_hitPoint = 10;


        //Put monster on good position
        Vector3 temp = new Vector3(positionX, positionY, 0);
        GameObject.Find("Monster").transform.position += temp;
    }
    /*===================================================================================================================
    * Save
    * 
    ===================================================================================================================*/
    /*
   
    */
    /*===================================================================================================================
    * Killed
    * 
    ===================================================================================================================*/
    public void kill()
    {
        GameObject lootbag = new GameObject();
        lootbag.name = "loot" + this.name;
        lootbag.AddComponent<SpriteRenderer>();
        lootbag.AddComponent<BoxCollider2D>().size = new Vector2(3, 3);
        lootbag.GetComponent<BoxCollider2D>().isTrigger = true;
        lootbag.tag = "Lootbag";
        lootbag.GetComponent<SpriteRenderer>().sprite = spriteLootbag;
        lootbag.GetComponent<SpriteRenderer>().sortingOrder = 3;
        lootbag.transform.position = this.transform.position;
        this.GetComponent<Rigidbody2D>().constraints = 0;

        bc2d.enabled = false;
        spriteRenderer.sprite = spriteKill;
        spriteRenderer.sortingOrder = 2;
        this.GetComponent<MonsterAi>().enabled = false;
      
        

    }

    /*===================================================================================================================
     * On Start
     * 
     ===================================================================================================================*/
    void Start()
    {
        ininitialiseMonster(0, 17,-4);
        dead = false;
    }


    /*===================================================================================================================
      * On Update
      * 
      ===================================================================================================================*/
    void Update()
    {
        if (!dead)
        {
            //Things do do
        }
    }
    /*===================================================================================================================
     * Damage/ Damage Calculator
     * 
     ===================================================================================================================*/
    public bool isDead()
    {
        return (dead);
    }

    //Give Attack
    public bool getAttack(int attackBonus)
    {
        attackBonus += Random.Range(1, 20);

        if (m_armorClass <= attackBonus)
        {
            Debug.Log("Attack HIT: " + attackBonus + "  VS Armor: " + m_armorClass);
            return (true); //Attack Touch
        }
        else
        {
            Debug.Log("Attack MISS: " + attackBonus + "  VS Armor: " + m_armorClass);
            return (false); //Attack Miss
        }
    }

    //Receive Damage
    public bool ReceiveDamage(int damage)
    {
        m_hitPoint -= damage;
        Debug.Log("Dealt: " + damage + "   HP left: " + m_hitPoint);
        if (m_hitPoint <= 0)
        {
            kill();

            return (true); //Is dead
        }
        else return (false); //Is alive
    }

    //Calculate Damage
    public int CalculateDamage()
    {
        return (Random.Range(1, m_damageDice) + m_damageBonus);
    }

}
