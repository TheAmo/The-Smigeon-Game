using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.UIElements;


public class Player : MonoBehaviour
{
    /*===================================================================================================================
     * Attribute
     * 
     ===================================================================================================================*/
    public Stats stats;
    public Equipement equipement;
    
    public Sprite spriteKill;

    private SpriteRenderer spriteRenderer;

    public GameObject sliderHealth;
    public GameObject sliderMana;
    public GameObject textMoney;  


    private BoxCollider2D bc2d;
    public GameObject canvas;

    private bool isShowing;
    private bool dead;

    private float time;


    private int m_player_id;

    public Camera PlayerCamera;
    private Rigidbody2D rb2d;

    /*===================================================================================================================
    * Start
    * 
    ===================================================================================================================*/
    public void initialisePlayer(int player_id, int class_id)
    {
        //GameObject.Find("DB Players Manager").GetComponent<DBPlayerManagement>().LoadPlayer();
        Debug.Log("Constructeur du player");
        m_player_id = player_id;

        stats.setGold(5000);
        //get PlayerEntryDB
       // playerDatabase = GameObject.Find("DB Players Manager").GetComponent<DBPlayerManagement>().playerDB;
        //get ClassEntry
        //classDatabase = GameObject.Find("DB Class Manager").GetComponent<DBClassManagement>().classDB;

        //Add Entry's info in Stats
        //stats = new Stats(playerDatabase.playerList[player_id], classDatabase.classList[class_id]);

        //Calculate everything for player level

        //Put player on good position
        Vector3 temp = new Vector3(70,70, 0);
        rb2d.MovePosition(temp);
        //temp= new Vector3(playerDatabase.playerList[player_id].position[0], playerDatabase.playerList[player_id].position[1], 0);
        rb2d.position=(temp);
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

        Debug.Log("Player is Dead!!!");
    }

    /*===================================================================================================================
     * On Start
     * 
     ===================================================================================================================*/
    void Start()
    {
        time = 0;
        rb2d = this.GetComponent<Rigidbody2D>();
        initialisePlayer(0, 0);
        isShowing = true;
        dead = false ;

        sliderHealth = GameObject.Find("SliderHealth");
        sliderMana = GameObject.Find("SliderMana");
        textMoney = GameObject.Find("Current Money");

        sliderHealth.GetComponent<UnityEngine.UI.Slider>().maxValue = stats.getHitPoint();
        sliderMana.GetComponent<UnityEngine.UI.Slider>().maxValue = stats.getMana();
        textMoney.GetComponent<UnityEngine.UI.Text>().text = stats.getGold().ToString();
        
         
        //Initialize stats
    }


    /*===================================================================================================================
      * On Update
      * 
      ===================================================================================================================*/
    void Update()
    {
      
        //Debug.Log(stats.getHitPoint());
        if (PlayerCamera.enabled==false) PlayerCamera.enabled = true;

        if (!dead)
        {
           
            time = Time.deltaTime + time;


            if (time >= 1 && stats.getMana()<100)
            {
                stats.setMana(stats.getMana() + 1);
                time = 0;
            }
            if (sliderHealth) sliderHealth.GetComponent<UnityEngine.UI.Slider>().value = stats.getHitPoint();
            if (sliderMana) sliderMana.GetComponent<UnityEngine.UI.Slider>().value = stats.getMana();

            if ((GameObject.Find("Blacksmith").transform.Find("Shop").gameObject.activeSelf == true)|| (GameObject.Find("Armorer").transform.Find("ShopArmor").gameObject.activeSelf == true))

            {
                if (this.GetComponent<MoveWASD>().enabled == true)
                {
                    this.GetComponent<MoveWASD>().enabled = false;
                }
                if (this.GetComponent<PlayerInteraction>().enabled == true)
                {
                    this.GetComponent<PlayerInteraction>().enabled = false;
                }
        
            } else
            {

                if (this.GetComponent<MoveWASD>().enabled == false)
                {
                    this.GetComponent<MoveWASD>().enabled = true;
                }
                if (this.GetComponent<PlayerInteraction>().enabled == false)
                {
                    this.GetComponent<PlayerInteraction>().enabled = true;
                }

            }
            
            

            if (PlayerCamera.enabled == false) PlayerCamera.enabled = true;
            
            if  (this.GetComponent<Player>().stats.getHitPoint() == 0)
            {
                this.transform.position = new Vector2(192, 207);
                this.GetComponent<Player>().stats.changeGoldByValue(-(this.GetComponent<Player>().stats.getGold() / 10));
            }


            if (Input.GetKeyDown("escape")) //TO Open Inventory. Doesn't work.
            {
                isShowing = !isShowing;
                canvas.SetActive(isShowing);
            }
            if (Input.GetKeyDown(KeyCode.K)) //To smack yourself in the face. Doesn't work.
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
        //canvas.transform.Find("SliderHealth").GetComponent<UnityEngine.UI.Slider>().value = stats.getHitPoint();
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
