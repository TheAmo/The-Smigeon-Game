using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCollision : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Yeet");
        if (col.gameObject.name.Contains("Collidable") || col.gameObject.name.Contains("Player"))
        {
            Debug.Log("HP" + col.gameObject.GetComponent<Stats>().getHitPoint());
            GameObject canvas = GameObject.Find("HUDCanvas");
            Debug.Log("Boom");
           
            if (col.gameObject.name.Contains("Player"))
            {
                FindObjectOfType<Player>().ReceiveDamage(1);
                //canvas.transform.Find("SliderHealth").GetComponent<UnityEngine.UI.Slider>().value = FindObjectOfType<Player>().GetComponent<Stats>().getHitPoint();
            } else
            {
                col.gameObject.GetComponent<Stats>().setHitPoint(col.gameObject.GetComponent<Stats>().getHitPoint() - 1);
            }
            Destroy(GameObject.FindGameObjectWithTag("Projectile"));
            //do stuff
        }
    }
}
