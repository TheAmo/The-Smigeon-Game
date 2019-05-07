using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInteraction : MonoBehaviour
{
    /*===================================================================================================================
     * Attribute
     * 
     ===================================================================================================================*/
    private Sprite spriteDefault;
    private Sprite spriteInteraction;

    private SpriteRenderer spriteRenderer;

    public GameObject player;

    public bool isInteracting;

    private BoxCollider2D bc2d;
    List<GameObject> interact = new List<GameObject>();

    private int chest_bonus;

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
        spriteDefault = player.GetComponent<PlayerChangeEquipment>().spriteDefault;
        spriteInteraction = player.GetComponent<PlayerChangeEquipment>().spriteInteraction;
        if (isInteracting)
        {
            spriteRenderer.sprite = spriteInteraction;//Change sprite to attack animation
        }
        else
        {
            if (this.GetComponent<PlayerAttack>().isAttacking==false)
                spriteRenderer.sprite = spriteDefault;//Change the sprite to default one
        }


       
        if (Input.GetKeyUp(KeyCode.F))
        {
            isInteracting = false;
           
        }
        if (Input.GetKeyDown(KeyCode.F)) //If key is pushed
        {
            isInteracting = true;
            Debug.Log("F");
            Vector2 playerPosition = transform.position;
            foreach (GameObject target in interact)
            {
                if (target.tag.Contains("Door"))
                {
                    Door(target, target.tag);
                }
                else if (target.tag == "Blacksmith")
                {
                        target.GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                else if(target.tag == "Armorer")
                {
                    target.GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                if (target.tag == "Lootbag")
                {
                    PickLoot(target);
                }
                if(target.tag == "Chest" || target.tag == "Chest2" || target.tag == "Chest3" || target.tag == "Chest4" || target.tag == "Chest5")
                {
                    Debug.Log("Tag : " + target.tag);
                    Chest(target);
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
        else if(range.gameObject.tag == ("Armorer"))
        {
            interact.Add(range.gameObject);
        }
        else if (range.gameObject.tag == ("Chest") || range.gameObject.tag == ("Chest2") || range.gameObject.tag == ("Chest3") || range.gameObject.tag == ("Chest4") || range.gameObject.tag == ("Chest5"))
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
        else if (range.gameObject.tag == ("Armorer"))
        {
            interact.Remove(range.gameObject);
        }
        else if (range.gameObject.tag == ("Lootbag"))
        {
            interact.Remove(range.gameObject);
        }
        else if (range.gameObject.tag == ("Chest") || range.gameObject.tag == ("Chest2") || range.gameObject.tag == ("Chest3") || range.gameObject.tag == ("Chest4") || range.gameObject.tag == ("Chest5"))
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
        player.GetComponent<Player>().stats.changeGoldByValue(loot.DropType(target.name));
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
    public void Chest(GameObject target)
    {
        int goldUpdated;
        Money money = new Money();

        if (target.tag == "Chest")
        {
            chest_bonus = 10;
            changeChestSprite(target);
            Debug.Log("Chest");
        }
        else if (target.tag == "Chest2")
        {
            chest_bonus = 30;
            changeChestSprite(target);
            Debug.Log("Chest2");
        }
        else if (target.tag == "Chest3")
        {
            chest_bonus = 350;
            changeChestSprite(target);
        }
        else if (target.tag == "Chest4")
        {
            chest_bonus = 500;
            changeChestSprite(target);
        }
        else if (target.tag == "Chest5")
        {
            chest_bonus = 750;
            changeChestSprite(target);
        }
        goldUpdated = (player.GetComponent<Player>().stats.getGold()) + chest_bonus;
        money.changeMoney(goldUpdated);
        player.GetComponent<Player>().stats.setGold(goldUpdated);
    }
    public void changeChestSprite(GameObject target)
    {
        SpriteRenderer spriteRendererChest = target.GetComponent<SpriteRenderer>();
        spriteRendererChest.sprite = Resources.Load<Sprite>("Chest-2");
        target.tag = "Chest_open";
    }
    
}
