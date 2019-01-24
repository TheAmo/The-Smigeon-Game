using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWASD : MonoBehaviour
{
    public float speed=1;
    Input newIn;

    bool isFacingLeft;
    bool isFacingRight;
    bool isFacingUp;
    bool isFacingDown;
    // Start is called before the first frame update
    void Start()
    {
        isFacingLeft = false;
        isFacingRight = false;
        isFacingUp = true;
        isFacingDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        float direction = 0;

        if (Input.GetAxis("Horizontal")!=0)
        {
            if(Input.GetAxis("Horizontal") < 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 270);
            }
            direction = Mathf.Abs(Input.GetAxis("Horizontal") * speed);
            transform.Translate(0, direction * Time.deltaTime, 0);
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 180);
            }
            if (Input.GetAxis("Vertical")  > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            direction = Mathf.Abs(Input.GetAxis("Vertical") * speed);
            transform.Translate(0,direction * Time.deltaTime, 0);
        }
    }
 }

