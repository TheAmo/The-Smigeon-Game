using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    public Sprite fbSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Jo helped with float :)
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            GameObject fb = new GameObject();
            fb.name = "Fireball";
            fb.AddComponent<SpriteRenderer>().sprite = fbSprite;
            fb.GetComponent<SpriteRenderer>().sortingOrder = 2;
            fb.AddComponent<Rigidbody2D>().gravityScale = 0;
            fb.AddComponent<BoxCollider2D>().size = new Vector2((float)2.575697, (float)4.518623);
            fb.GetComponent<BoxCollider2D>().isTrigger = true;
            //fb = (GameObject.FindGameObjectWithTag("Projectile"));
            Vector2 v2 = this.GetComponent<Rigidbody2D>().transform.position;
            fb.transform.position = v2;
            Debug.Log(fb.transform.position.ToString());

           // fb.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(GameObject.FindGameObjectWithTag("Player").transform.eulerAngles.y), Mathf.Sin(GameObject.FindGameObjectWithTag("Player").transform.eulerAngles.y));
        }
    }
}
