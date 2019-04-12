using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCollision : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Yeet");
        if (col.gameObject.name.Contains("Collidable"))
        {
            Debug.Log(col.gameObject.GetComponent<Stats>().getHitPoint());
            GameObject canvas = GameObject.Find("HUDCanvas");
            col.gameObject.GetComponent<Stats>().setHitPoint((col.gameObject.GetComponent<Stats>().getHitPoint() - 15));
            canvas.transform.Find("SliderHealth").GetComponent<UnityEngine.UI.Slider>().value = FindObjectOfType<Player>().GetComponent<Stats>().getHitPoint();
            Debug.Log("Boom");
            col.gameObject.GetComponent<Stats>().setHitPoint(col.gameObject.GetComponent<Stats>().getHitPoint() - 1);
            Destroy(GameObject.FindGameObjectWithTag("Projectile"));
            //do stuff
        }
    }
}
