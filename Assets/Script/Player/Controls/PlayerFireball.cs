using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    public Sprite fbSprite;
    private bool fireActivated = false;
    private GameObject canvas;
    GameObject fb;

    // Jo helped with float :)
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && fireActivated == false && this.GetComponent<Stats>().getMana() >= 15)
        {
            //fireActivated = true;
            canvas = GameObject.Find("HUDCanvas");
            this.GetComponent<Stats>().setMana((this.GetComponent<Stats>().getMana() - 15));
            canvas.transform.Find("SliderMana").GetComponent<UnityEngine.UI.Slider>().value = this.GetComponent<Stats>().getMana();
            fb = new GameObject();
            fb.name = "Fireball";
            fb.tag = "Projectile";
            fb.AddComponent<SpriteRenderer>().sprite = fbSprite;
            fb.GetComponent<SpriteRenderer>().sortingOrder = 2;
            fb.AddComponent<Rigidbody2D>().gravityScale = 0;
            fb.GetComponent<Rigidbody2D>().MoveRotation(this.GetComponent<Rigidbody2D>().rotation);
            fb.AddComponent<BoxCollider2D>().size = new Vector2((float)2.8, (float)4.7);
            fb.GetComponent<Rigidbody2D>().angularDrag = 25000;
            fb.GetComponent<Rigidbody2D>().drag = 0;
            fb.GetComponent<Rigidbody2D>().mass = 1000;
            fb.AddComponent<FireBallCollision>();
            Vector2 v2 = this.GetComponent<Rigidbody2D>().transform.position;
            fb.transform.position = v2;
           
          
            Debug.Log(this.GetComponent<Rigidbody2D>().rotation);

            fb.GetComponent<Rigidbody2D>().velocity = new Vector2(10*Mathf.Cos(this.GetComponent<Rigidbody2D>().rotation+270), 10*Mathf.Sin(this.GetComponent<Rigidbody2D>().rotation+270));
        }
    }

    
}
