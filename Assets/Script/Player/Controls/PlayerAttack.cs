using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour { 
    /*===================================================================================================================
     * Attribute
     * 
     ===================================================================================================================*/
    public Sprite spriteDefault;
    public Sprite spriteAttack;

    public int attackKnockback;

    public bool isAttacking=false;

    public SpriteRenderer spriteRenderer;

    private BoxCollider2D bc2d;
    List<GameObject> enemy = new List<GameObject>();

    public GameObject player;

    /*===================================================================================================================
     * On Start
     * 
     ===================================================================================================================*/
    void Start()
    {
        spriteDefault = this.GetComponent<PlayerChangeEquipment>().spriteDefault;
        spriteAttack = this.GetComponent<PlayerChangeEquipment>().spriteAttack;

        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    /*===================================================================================================================
     * On Update
     * 
     ===================================================================================================================*/
    void Update()
    {
        spriteDefault = player.GetComponent<PlayerChangeEquipment>().spriteDefault;
        spriteAttack = player.GetComponent<PlayerChangeEquipment>().spriteAttack;
        if (isAttacking)
        {
            spriteRenderer.sprite = spriteAttack;//Change sprite to attack animation
        }
        else
        {
            if (this.GetComponent<PlayerInteraction>().isInteracting == false)
                spriteRenderer.sprite = spriteDefault;//Change the sprite to default one
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) //If key is pushed
        {
            isAttacking = true;
            
            //attack();
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isAttacking = false;
               
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isAttacking = false;
            
        }
    }

    /*===================================================================================================================
     * Collider for enemy range
     * 
     ===================================================================================================================*/
    void OnTriggerEnter2D(Collider2D range)
    {
        if (range.gameObject.tag == "Enemy")
        {
            enemy.Add(range.gameObject);
            //Debug.Log("Enemy in range");
        }
    }
    void OnTriggerExit2D(Collider2D range)
    {
        if (range.gameObject.tag == "Enemy")
        {
            enemy.Remove(range.gameObject);
            //Debug.Log("Enemy out of range");
        }
    }
    /*===================================================================================================================
     * Attack function
     * 
     ===================================================================================================================*/
    //Vector2 knockbackDirection;
    //bool tmpbool;

    /*
    public void attack()
    {
        foreach (GameObject target in enemy)
        {
            tmpbool = target.GetComponent<Monster>().getAttack(GetComponent<Player>().stats.getAttackBonus());
            if (tmpbool == true)//Target is hit
            {
                tmpbool = target.GetComponent<MonsterCore>().ReceiveDamage(GetComponent<Player>().CalculateDamage());
                if (tmpbool == true)//Target is dead
                {
                    enemy.Remove(target);
                    target.GetComponent<MonsterAI>().kill();
                }
                else
                {
                    knockbackDirection = target.transform.position - this.transform.position;
                    target.GetComponent<Rigidbody2D>().AddForce(knockbackDirection.normalized * attackKnockback);
                }
            }
            break;
        }
    }
    */
}
