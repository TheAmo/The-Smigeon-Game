using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAi : MonoBehaviour
{  
    public float speed=1;
    float degfactor = 360 / (2 * Mathf.PI);
    private Rigidbody2D rb2d;
    float sqrt2 = 1 / Mathf.Sqrt(2);
    private float updateInterval;
    private float time=0;

    private float horizontalAxis;
    private float verticalAxis;

    public int distanceDetection;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        updateInterval = Random.Range(1.0f, 5.0f);
    }


    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        Transform player = GameObject.Find("Player").transform;

        if (Vector2.Distance(player.position, this.transform.position) > distanceDetection)
        {
            if (time >= updateInterval)
            {
                time = 0;

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
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, 0.09f); 

        }       
    }


}
