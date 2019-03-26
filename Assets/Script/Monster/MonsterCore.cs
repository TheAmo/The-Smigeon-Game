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
    private SpriteRenderer spriteRenderer;

    private BoxCollider2D bc2d;

    private bool dead;

    private MonsterDatabase monsterDatabase;
   
 
    /*===================================================================================================================
    * Start
    * 
    ===================================================================================================================*/
    public void ininitialiseMonster(int monster_id, int positionX, int positionY)
    {
        GameObject.Find("DB Monsters Manager").GetComponent<DBMonsterManager>().LoadMonster();
        Debug.Log("Constructeur du Monster");
        m_monster_id = monster_id;
        //get MonsterEntry
        monsterDatabase = GameObject.Find("DB Monsters Manager").GetComponent<DBMonsterManager>().monsterDB;

        //Put monster on good position
        Vector3 temp = new Vector3(positionX, positionY, 0);
        GameObject.Find("Monster").transform.position += temp;
    }
    /*===================================================================================================================
    * Save
    * 
    ===================================================================================================================*/
    /*
    public void SavePlayer()
    {
        GameObject.Find("XML Players Manager").GetComponent<XMLPlayerManagement>().playerDB.playerList[m_player_id].position[0] = GameObject.Find("Player").transform.position.x;
        GameObject.Find("XML Players Manager").GetComponent<XMLPlayerManagement>().playerDB.playerList[m_player_id].position[1] = GameObject.Find("Player").transform.position.y;

        Debug.Log("Saved Position " + GameObject.Find("XML Players Manager").GetComponent<XMLPlayerManagement>().playerDB.playerList[m_player_id].position[0] + "," + GameObject.Find("XML Players Manager").GetComponent<XMLPlayerManagement>().playerDB.playerList[m_player_id].position[1] + ")");
        GameObject.Find("XML Players Manager").GetComponent<XMLPlayerManagement>().SavePlayer();
        Debug.Log("Saved Player");
    }
    */
    /*===================================================================================================================
    * Killed
    * 
    ===================================================================================================================*/
    public void kill()
    {
        Destroy(bc2d, 0);
        spriteRenderer.sprite = spriteKill;
        spriteRenderer.sortingOrder = 1;

        //Destroy(this.GetComponent<MonsterAIMovement>(), 0);
        //Destroy(this.GetComponent<MonsterAIAttack>(), 0);

        Debug.Log("Monster is Dead!!!");
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
