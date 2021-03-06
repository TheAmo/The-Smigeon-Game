﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFireball : MonoBehaviour
{
    public Stats stats;
    public Sprite fbSprite;
    static private float Pi = 3.141592653589f;
    private bool fireActivated = false;
    private GameObject canvas;
    private GameObject fb;
    private int cost = 15;

    // Jo helped with float :)

    void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.Q) && stats.getMana() >= cost)
        { 
            canvas = GameObject.Find("HUDCanvas");
            stats.setMana((stats.getMana() - cost));
            

            fb = new GameObject();
            fb.name = "Fireball";
            fb.tag = "Projectile";
            fb.AddComponent<SpriteRenderer>().sprite = fbSprite;
            fb.GetComponent<SpriteRenderer>().sortingOrder = 2;
            fb.AddComponent<Rigidbody2D>().gravityScale = 0;
            fb.GetComponent<Rigidbody2D>().MoveRotation(this.GetComponent<Rigidbody2D>().rotation);
            Debug.Log(this.GetComponent<Rigidbody2D>().rotation);
            fb.GetComponent<Rigidbody2D>().velocity = new Vector2(10 * Mathf.Cos(fb.GetComponent<Rigidbody2D>().rotation), 10 * Mathf.Sin(fb.GetComponent<Rigidbody2D>().rotation));
            fb.AddComponent<BoxCollider2D>().size = new Vector2((float)2.8, (float)4.7);
            fb.GetComponent<Rigidbody2D>().angularDrag = 25000;
            fb.GetComponent<Rigidbody2D>().drag = 0;
            fb.GetComponent<Rigidbody2D>().mass = 1000;
            fb.AddComponent<FireBallCollision>();
            Vector2 v2 = this.GetComponent<Rigidbody2D>().transform.position;
            fb.transform.position = v2;
           
          
            Debug.Log(((this.transform.rotation.z) * 180) / Pi);

            fb.GetComponent<Rigidbody2D>().velocity = (this.transform.rotation) * new Vector2(0, 100);
        }
    }

    
}
