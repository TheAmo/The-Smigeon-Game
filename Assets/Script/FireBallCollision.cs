using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCollision : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.tag.Contains("Player"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Projectile"));
        }
        //Debug.Log("Yeet");
        if (col.gameObject.name.Contains("Collidable") || col.gameObject.tag.Contains("Enemy"))
        {
            
            
            if (!col.gameObject.name.Contains("Collidable"))
            {
                Debug.Log("HP" + col.gameObject.GetComponent<Stats>().getHitPoint());
                GameObject canvas = GameObject.Find("HUDCanvas");
                Debug.Log("Boom");

                if (col.gameObject.name.Contains("Player"))
                {
                    FindObjectOfType<Player>().ReceiveDamage(1);
                    //canvas.transform.Find("SliderHealth").GetComponent<UnityEngine.UI.Slider>().value = FindObjectOfType<Player>().GetComponent<Stats>().getHitPoint();
                }
                 else if (col.gameObject.tag.Contains("Enemy"))
                {
                    col.gameObject.GetComponent<MonsterAi>().ReceiveDamage(10); 
                }
                else
                {

                }
            }
           

        }
    }
}
