using System.Collections;
using UnityEngine;

public class MoveWASD : MonoBehaviour
{
    public float speed=1;


    float degfactor = (360 / (2 * Mathf.PI));

    private Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float sqrt2 = 1 / Mathf.Sqrt(2);
        float horizontalAxis = Input.GetAxisRaw ("Horizontal");
        float verticalAxis = Input.GetAxisRaw ("Vertical");
        
        Vector2 movement = new Vector2(horizontalAxis, verticalAxis);
        rb2d.AddForce (movement * 5000);

        if (verticalAxis != 0 || horizontalAxis != 0)
        {
            int angle = (int)Mathf.Floor((360 / (2 * Mathf.PI)) * Mathf.Atan(-horizontalAxis / verticalAxis));
            if (verticalAxis < 0)
                angle += 180;
            
            transform.localRotation = Quaternion.Euler(0, 0, angle);

            /*controller.Move(new Vector3(0, speed * Time.deltaTime, 0));
            if (verticalAxis != 0 && horizontalAxis != 0)
                controller.Move(new Vector3(sqrt2 * speed * Time.deltaTime * horizontalAxis, sqrt2 * speed * Time.deltaTime * verticalAxis, 0));
            else controller.Move(new Vector3(speed * Time.deltaTime * horizontalAxis, speed * Time.deltaTime * verticalAxis, 0));*/
        }
    }
 }

