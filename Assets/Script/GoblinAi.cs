using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAi : MonoBehaviour
{
    public float speed=1;
    float degfactor = 360 / (2 * Mathf.PI);
    private Rigidbody2D rb2d;
    float sqrt2 = 1 / Mathf.Sqrt(2);
    private float updateInterval;
    private float time=0;

    private float horizontalAxis;
    private float verticalAxis;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        updateInterval = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= updateInterval)
        {
            time = 0;

            verticalAxis = Random.Range(-2.0f, 2.0f);
            horizontalAxis = Random.Range(-2, 2);

            if (verticalAxis != 0 || horizontalAxis != 0)
            {
                int angle = (int)Mathf.Floor(degfactor * Mathf.Atan(-horizontalAxis / verticalAxis));
                if (verticalAxis < 0)
                    angle += 180;
                transform.localRotation = Quaternion.Euler(0, 0, angle);
            }
            
        }
        Vector2 movement = new Vector2(horizontalAxis, verticalAxis);
        rb2d.AddForce(movement * speed);
    }
}
