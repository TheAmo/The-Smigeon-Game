using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Sprite spriteDefault;
    public Sprite spriteInteraction;
    public Sprite spriteKill;
    private SpriteRenderer spriteRenderer;
    public int i = 0;

    List<GameObject> interact = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer.sprite = spriteDefault;
    }


    // Getting the list of ennemy within range
    void OnTriggerEnter2D(Collider2D range)
    {
        if (range.gameObject.tag == "Interactable")
        {
            interact.Add(range.gameObject);
            //Debug.Log("Enemy in range");
        } 
         else if (range.gameObject.tag.Contains("Door"))
        {
            interact.Add(range.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D range)
    {
        if (range.gameObject.tag == "Interactable")
        {
            interact.Remove(range.gameObject);
            //Debug.Log("Enemy out of range");
        }
        else if (range.gameObject.tag.Contains("Door"))
        {
            interact.Remove(range.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && spriteRenderer.sprite != spriteKill) //If key is pushed
        {
            spriteRenderer.sprite = spriteInteraction;//Change sprite to attack animation
            Vector2 playerPosition = transform.position;
            foreach (GameObject target in interact)
            {
                if(target.tag == "Door")
                {
                    if(target.transform.eulerAngles.z==0)
                    {
                        
                        target.transform.Translate(0, 2.5f, 0);
                        target.transform.Rotate(0, 0, 90, Space.Self);
                    }
                    else
                    {
                        target.transform.Rotate(0, 0, -90, Space.Self);
                        target.transform.Translate(0, -2.5f, 0);
                    }
                    
                }else if(target.tag == "Doorh")
                {
                    if (target.transform.eulerAngles.z == 90){
                        target.transform.Rotate(0, 0, -90, Space.Self);
                        target.transform.Translate(-2.5f, 0, 0);
                    }
                    else
                    {
                        target.transform.Translate(2.5f, 0, 0);
                        target.transform.Rotate(0, 0, 90, Space.Self);
                    }
                }
               /* Destroy(target, 0);
                interact.Remove(target);*/
                break;
            }
        }
        if (Input.GetKeyUp(KeyCode.F)&&spriteRenderer.sprite != spriteKill)
        {
            spriteRenderer.sprite = spriteDefault;//Change the sprite to default one
        }
    }
}
