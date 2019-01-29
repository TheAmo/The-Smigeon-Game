﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAi : MonoBehaviour
{  
    public float speed=1;
    public int distanceDetection;

    public Sprite spriteDefault;
    public Sprite spriteAttack;
    public Sprite spriteKill;

    float degfactor = 360 / (2 * Mathf.PI);
    float sqrt2 = 1 / Mathf.Sqrt(2);

    private float updateInterval;
    public float attackCooldown;
    private float updatetime=0;
    private float attacktime = 0;

    private float horizontalAxis;
    private float verticalAxis;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;

    // Start is called before the first frame update

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();

        if (spriteRenderer == null)
            spriteRenderer.sprite = spriteDefault;
        rb2d = GetComponent<Rigidbody2D>();
        updateInterval = Random.Range(1.0f, 5.0f);
    }

    //To kill the monster
    public void kill()
    {
        Destroy(bc2d,0);
        //Destroy(rb2d,0);
        speed = 0;
        updateInterval = -1;
        attacktime = -1;
        distanceDetection = 0;
        spriteRenderer.sprite = spriteKill;
        spriteRenderer.sortingOrder = 1;
    }
    // Update is called once per frame
    void Update()
    {

        //Movement
        updatetime += Time.deltaTime;
        Transform player = GameObject.Find("Player").transform;
        if (Vector2.Distance(player.position, this.transform.position) > distanceDetection)
        {
            if (updatetime >= updateInterval && updateInterval != -1)
            {
                updatetime = 0;

                //check mur
                verticalAxis = Random.Range(-2.0f, 2.0f);
                horizontalAxis = Random.Range(-2, 2);


                //movement
                if (verticalAxis != 0 || horizontalAxis != 0)
                {

                    //random movements
                    int angle = (int)Mathf.Floor(degfactor * Mathf.Atan(-horizontalAxis / verticalAxis));
                    if (verticalAxis < 0)
                        angle += 180;
                    transform.localRotation = Quaternion.Euler(0, 0, angle);

                }
            }
            Vector2 movement = new Vector2(horizontalAxis, verticalAxis);
            rb2d.AddForce(movement * speed);
        }
        else
        {
            //turn around towards the target 
            int angle = (int)Mathf.Floor(degfactor * Mathf.Atan(-(transform.position.x- player.position.x ) / (transform.position.y- player.position.y)));
            if(player.position.y < transform.position.y)
            {
                angle = angle - 180;
            }

            //do the action of rotation
            transform.localRotation = Quaternion.Euler(0, 0, angle);

            // move sprite towards the target
            if (Vector2.Distance(player.position, this.transform.position) > 2)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, 0.09f);
            }
          
        }

        //Attack
        attacktime += Time.deltaTime;

        if(attacktime>=attackCooldown && attackCooldown!=-1 && Vector2.Distance(player.position, this.transform.position) < 2)
        {
            attacktime = 0;
            Debug.Log("Attack");
        }
    }


}
