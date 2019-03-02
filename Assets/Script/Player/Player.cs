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
    
    public Sprite spriteKill;

    private SpriteRenderer spriteRenderer;

    public UnityEngine.UI.Slider sliderHealth;
    
    private bool dead;

    private BoxCollider2D bc2d;

    /*===================================================================================================================
     * Killed
     * 
     ===================================================================================================================*/
    public void kill()
    {
        Destroy(bc2d, 0);
        spriteRenderer.sprite = spriteKill;
        spriteRenderer.sortingOrder = 1;

        Destroy(this.GetComponent<MoveWASD>(), 0);
        Destroy(this.GetComponent<PlayerAttack>(), 0);
        Destroy(this.GetComponent<PlayerInteraction>(), 0);
        Destroy(this.GetComponent<PlayerChangeEquipment>(), 0);
        Destroy(this.GetComponent<PlayerLight>(), 0);

        Debug.Log("Player is Dead!!!");
    }

    /*===================================================================================================================
     * On Start
     * 
     ===================================================================================================================*/
    void Start()
    {
        dead = false ;
        stats = GameObject.Find("Player").GetComponent<Stats>();

        sliderHealth = GameObject.Find("SliderHealth").GetComponent<UnityEngine.UI.Slider>();
        sliderHealth.maxValue = this.stats.getHitPoint() ;
    }


    /*===================================================================================================================
      * On Update
      * 
      ===================================================================================================================*/
    void Update()
    {
        if (dead)
        {
            sliderHealth.value = this.stats.getHitPoint();
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
