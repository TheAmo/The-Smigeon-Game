using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Sprite spriteDefault;
    public Sprite spriteInteraction;
    public Sprite spriteKill;
    private SpriteRenderer spriteRenderer;

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
    }
    void OnTriggerExit2D(Collider2D range)
    {
        if (range.gameObject.tag == "Interactable")
        {
            interact.Remove(range.gameObject);
            //Debug.Log("Enemy out of range");
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
                Destroy(target, 0);
                interact.Remove(target);
                break;
            }
        }
        if (Input.GetKeyUp(KeyCode.F)&&spriteRenderer.sprite != spriteKill)
        {
            spriteRenderer.sprite = spriteDefault;//Change the sprite to default one
        }
    }
}
