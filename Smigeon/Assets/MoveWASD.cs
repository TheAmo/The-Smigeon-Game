using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWASD : MonoBehaviour
{
    public float speed=1;
    Input newIn;

    float degfactor = 360 / (2 * Mathf.PI);

    CharacterController controller;

    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float sqrt2 = 1 / Mathf.Sqrt(2);
        float verticalAxis = Input.GetAxisRaw("Vertical");
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
    
        if (verticalAxis != 0 || horizontalAxis != 0)
        {
            int angle = (int)Mathf.Floor(degfactor * Mathf.Atan(-horizontalAxis / verticalAxis));
            if (verticalAxis < 0)
                angle += 180;
            
            transform.localRotation = Quaternion.Euler(0, 0, angle);

            //controller.Move(new Vector3(0, speed * Time.deltaTime, 0));
            if (verticalAxis != 0 && horizontalAxis != 0)
                controller.Move(new Vector3(sqrt2 * speed * Time.deltaTime * horizontalAxis, sqrt2 * speed * Time.deltaTime * verticalAxis, 0));
            else controller.Move(new Vector3(speed * Time.deltaTime * horizontalAxis, speed * Time.deltaTime * verticalAxis, 0));
        }
    }
 }

