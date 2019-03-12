using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAttack : NetworkBehaviour
{
    /*===================================================================================================================
     * Attribute
     * 
     ===================================================================================================================*/
    private Sprite spriteDefault;
    private Sprite spriteAttack;

    public int attackKnockback;
    private SpriteRenderer spriteRenderer;

    private BoxCollider2D bc2d;
    List<GameObject> enemy = new List<GameObject>();

    public GameObject player;

    /*===================================================================================================================
     * On Start
     * 
     ===================================================================================================================*/
    void Start()
    {
        spriteDefault = player.GetComponent<PlayerChangeEquipment>().spriteDefault;
        spriteAttack = player.GetComponent<PlayerChangeEquipment>().spriteAttack;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /*===================================================================================================================
     * On Update
     * 
     ===================================================================================================================*/
    void Update()
    {
        if (hasAuthority == false) return;

        if (Input.GetKeyDown(KeyCode.LeftShift)) //If key is pushed
        {
            spriteDefault = player.GetComponent<PlayerChangeEquipment>().spriteDefault;
            spriteAttack = player.GetComponent<PlayerChangeEquipment>().spriteAttack;
            spriteRenderer.sprite = spriteAttack;//Change sprite to attack animation
            attack();
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                spriteRenderer.sprite = spriteDefault;//Change the sprite to default one
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            spriteRenderer.sprite = spriteDefault;//Change the sprite to default one
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
    Vector2 knockbackDirection;
    bool tmpbool;
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
                    target.GetComponent<MonsterAi>().kill();
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
}
