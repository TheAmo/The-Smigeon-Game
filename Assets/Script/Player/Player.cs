using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    /*===================================================================================================================
     * Attribute
     * 
     ===================================================================================================================*/
    public Stats stats;
    
    public Sprite spriteKill;
    private SpriteRenderer spriteRenderer;

    public GameObject sliderHealth;

    private BoxCollider2D bc2d;
    public GameObject canvas;

    public GameObject player;


    private bool isShowing;
    private bool dead;

    private PlayerDatabase playerDatabase;
    private ClassDatabase classDatabase;

    private int m_player_id;

    /*===================================================================================================================
    * Start
    * 
    ===================================================================================================================*/
    public void ininitialisePlayer(int player_id, int class_id)
    {
        GameObject.Find("XML Players Manager").GetComponent<XMLPlayerManagement>().LoadPlayer();
        Debug.Log("Constructeur du player");
        m_player_id = player_id;
        //get PlayerEntry
        playerDatabase = GameObject.Find("XML Players Manager").GetComponent<XMLPlayerManagement>().playerDB;
        //get ClassEntry
        classDatabase = GameObject.Find("XML Class Manager").GetComponent<XMLClassManagement>().classDB;

        //Add Entry's info in Stats
        stats = new Stats(playerDatabase.playerList[player_id], classDatabase.classList[class_id]);

        //Calculate everything for player level

        //Put player on good position
        //Vector3 temp = new Vector3(playerDatabase.playerList[player_id].position[0], playerDatabase.playerList[player_id].position[1], 0);
        Vector3 temp = new Vector3(0,70, 0);

        player.transform.position = temp;


    }

    /*===================================================================================================================
    * Killed
    * 
    ===================================================================================================================*/
    public void kill()
    {
        Destroy(bc2d, 0);
        spriteRenderer.sprite = spriteKill;
        spriteRenderer.sortingOrder = 1;

        Destroy(player.GetComponent<MoveWASD>(), 0);
        Destroy(player.GetComponent<PlayerAttack>(), 0);
        Destroy(player.GetComponent<PlayerInteraction>(), 0);
        Destroy(player.GetComponent<PlayerChangeEquipment>(), 0);
        Destroy(player.GetComponent<PlayerLight>(), 0);

        Debug.Log("Player is Dead!!!");
    }

    /*===================================================================================================================
     * On Start
     * 
     ===================================================================================================================*/
    void Start()
    {
        ininitialisePlayer(0, 0);
        isShowing = true;
        dead = false ;


        canvas.transform.Find("SliderHealth").GetComponent<UnityEngine.UI.Slider>().maxValue = stats.getHitPoint();



        //Initialize stats
    }


    /*===================================================================================================================
      * On Update
      * 
      ===================================================================================================================*/
    void Update()
    {

        if (hasAuthority == false) return;

        if (!dead)
        {
            canvas.transform.Find("SliderHealth").GetComponent<UnityEngine.UI.Slider>().value = stats.getHitPoint();



            if (Input.GetKeyDown("escape")) //TO Open Inventory. Doesn't work.
            {
                isShowing = !isShowing;
                canvas.SetActive(isShowing);
            }
            if (Input.GetKeyDown(KeyCode.K)) //TO Open Inventory. Doesn't work.
            {
                ReceiveDamage(1);
                Debug.Log("Damage player test");
            }
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

        if (stats.getArmorClass() <= attackBonus)
        {
            Debug.Log("Attack HIT: " + attackBonus + "  VS Armor: " + stats.getArmorClass());
            return (true); //Attack Touch
        }
        else
        {
            Debug.Log("Attack MISS: " + attackBonus + "  VS Armor: " + stats.getArmorClass());
            return (false); //Attack Miss
        }
    }

    //Receive Damage
    public bool ReceiveDamage(int damage)
    {
        stats.setHitPoint(stats.getHitPoint()-damage);
        Debug.Log("Dealt: " + damage + "   HP left: " + stats.getHitPoint());
        if (stats.getHitPoint() <= 0)
        {
            return (true); //Is dead
        }
        else return (false); //Is alive
    }

    //Calculate Damage
    public int CalculateDamage()
    {
        return (Random.Range(1, stats.getDamageDie()) + stats.getDamageBonus());
    }

}
