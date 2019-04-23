﻿using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MoveWASD : NetworkBehaviour
{
    public float speed = 1;
    float degfactor = 360 / (2 * Mathf.PI);
    float sqrt2 = 1 / Mathf.Sqrt(2);

    private Rigidbody2D rb2d;

    private bool isFast = false;

    Vector2 movement;
    int angle;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasAuthority == false) return;
        
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

       movement = new Vector3(horizontalAxis, verticalAxis,0);


        //Speed vector
        if (Input.GetKeyDown(KeyCode.LeftControl) && isFast == false)
        {
            speed = speed * (float)2;
            isFast = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && isFast == true)
        {
            speed = speed / 2;
            isFast = false;
        }


        if (verticalAxis != 0 && horizontalAxis != 0)
            rb2d.MovePosition(rb2d.position + (movement * speed * sqrt2));
        //rb2d.AddForce(movement * speed * sqrt2);
        else rb2d.MovePosition(rb2d.position + (movement * speed));
        //rb2d.AddForce(movement * speed);

        //Rotation
        if (verticalAxis != 0 || horizontalAxis != 0)
        {
            angle = (int)Mathf.Floor(degfactor * Mathf.Atan(-horizontalAxis / verticalAxis));
            if (verticalAxis < 0)
                angle += 180;
            rb2d.MoveRotation(angle);
            //transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
      
    }
    /*
    [Command]
    void CmdRotation()
    {
        comp= Quaternion.Euler(0, 0, angle);
    }

    void CmdMovement1()
    {
        rb2d.AddForce(movement * speed * sqrt2);
    }
    void CmdMovement2()
    {
        rb2d.AddForce(movement * speed);
    }
    */
}
