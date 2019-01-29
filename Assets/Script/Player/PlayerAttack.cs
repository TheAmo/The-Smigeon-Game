﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Stats
{
    public Sprite spriteDefault;
    public Sprite spriteAttack;
    public int attackKnockback;

    private SpriteRenderer spriteRenderer;
    private bool tmpbool;
    List<GameObject> enemy = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer.sprite = spriteDefault;
    }

    // Getting the list of ennemy within range
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) //If key is pushed
        {
            spriteRenderer.sprite = spriteAttack;//Change sprite to attack animation
            foreach (GameObject target in enemy)
            {
                tmpbool=target.GetComponent<Stats>().getDamage(this.dealDamage());
                target.GetComponent<Rigidbody2D>().AddForce(transform.forward * attackKnockback);//knockback
                if (tmpbool==true)
                {
                    enemy.Remove(target);
                    target.GetComponent<MonsterAi>().kill();
                }
                break;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            spriteRenderer.sprite = spriteDefault;//Change the sprite to default one
        }
    }
}
