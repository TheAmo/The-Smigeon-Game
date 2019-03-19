using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerInteraction : NetworkBehaviour
{
    /*===================================================================================================================
     * Attribute
     * 
     ===================================================================================================================*/
    private Sprite spriteDefault;
    private Sprite spriteInteraction;

    private SpriteRenderer spriteRenderer;

    public GameObject player;

    private BoxCollider2D bc2d;
    List<GameObject> interact = new List<GameObject>();

    /*===================================================================================================================
     * On Start
     * 
     ===================================================================================================================*/
    void Start()
    {
        spriteDefault = player.GetComponent<PlayerChangeEquipment>().spriteDefault;
        spriteInteraction = player.GetComponent<PlayerChangeEquipment>().spriteInteraction;

        spriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    /*===================================================================================================================
     * On Update
     * 
     ===================================================================================================================*/
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            spriteRenderer.sprite = spriteDefault;//Change the sprite to default one
        }
        if (Input.GetKeyDown(KeyCode.F)) //If key is pushed
        {
            spriteDefault = player.GetComponent<PlayerChangeEquipment>().spriteDefault;
            spriteInteraction = player.GetComponent<PlayerChangeEquipment>().spriteInteraction;
            spriteRenderer.sprite = spriteInteraction;//Change sprite to attack animation
            Vector2 playerPosition = transform.position;
            foreach (GameObject target in interact)
            {
                if (target.tag.Contains("Door"))
                {
                    Door(target, target.tag);
                }
                else if (target.tag == "Blacksmith")
                {
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue();
                }
                if (target.tag == "Lootbag")
                {
                    PickLoot(target);
                }

                /* Destroy(target, 0);
                 interact.Remove(target);*/
                break;
            }
        }
    }

    /*===================================================================================================================
     * Collider for interactable range
     * 
     ===================================================================================================================*/
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
        else if (range.gameObject.tag == ("Blacksmith"))
        {
            interact.Add(range.gameObject);
        }
        else if (range.gameObject.tag == ("Lootbag"))
        {
            interact.Add(range.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D range)
    {
        if (range.gameObject.tag == "Interactable")
        {
            interact.Remove(range.gameObject);
            //Debug.Log("Enemy in range");
        }
        else if (range.gameObject.tag.Contains("Door"))
        {
            interact.Remove(range.gameObject);
        }
        else if (range.gameObject.tag == ("Blacksmith"))
        {
            interact.Remove(range.gameObject);
        }
        else if (range.gameObject.tag == ("Lootbag"))
        {
            interact.Remove(range.gameObject);
        }
    }
    /*===================================================================================================================
     * Function
     * 
     ===================================================================================================================*/
    public void PickLoot(GameObject target)
    {
        Loot loot = new Loot();
        loot.DropType(target.name);
        Destroy(target);
        interact.Remove(target);
    }
    public void Door(GameObject target, string tag)
    {
        if (target.tag == "Door")
        {
            if (target.transform.eulerAngles.z == 0)
            {
                target.transform.Translate(0, 2.5f, 0);
                target.transform.Rotate(0, 0, 90, Space.Self);
            }
            else
            {
                target.transform.Rotate(0, 0, -90, Space.Self);
                target.transform.Translate(0, -2.5f, 0);
            }
        }
        else
        {
            if (target.transform.eulerAngles.z == 90)
            {
                target.transform.Rotate(0, 0, -90, Space.Self);
                target.transform.Translate(-2.5f, 0, 0);
            }
            else
            {
                target.transform.Translate(2.5f, 0, 0);
                target.transform.Rotate(0, 0, 90, Space.Self);
            }
        }
    }
}
