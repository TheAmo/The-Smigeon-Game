using System.Collections;
using UnityEngine;

public class MoveWASD : MonoBehaviour
{
    public float speed = 1;
    float degfactor = 360 / (2 * Mathf.PI);
    float sqrt2 = 1 / Mathf.Sqrt(2);
    private Rigidbody2D rb2d;
    private bool isFast = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontalAxis, verticalAxis);


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
            rb2d.AddForce(movement * speed * sqrt2);
        else rb2d.AddForce(movement * speed);

        //Rotation
        if (verticalAxis != 0 || horizontalAxis != 0)
        {
            int angle = (int)Mathf.Floor(degfactor * Mathf.Atan(-horizontalAxis / verticalAxis));
            if (verticalAxis < 0)
                angle += 180;

            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}

