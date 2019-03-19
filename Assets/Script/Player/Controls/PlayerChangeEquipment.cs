using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerChangeEquipment : NetworkBehaviour
{
    /*===================================================================================================================
     * Attribute
     * 
     ===================================================================================================================*/
    private SpriteRenderer spriteRenderer;

    public Sprite spriteDefault;
    public Sprite spriteAttack;
    public Sprite spriteInteraction;

    public Equipement item = new Equipement(1, 1);

    public Sprite[] sprites;

    /*===================================================================================================================
     * On Start
     * 
     ===================================================================================================================*/
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("rogue_sheet");
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer.sprite = spriteDefault;
        ChangeEquipement();
    }

    /*===================================================================================================================
     * On Update
     * 
     ===================================================================================================================*/
    void Update()
    {
        if (hasAuthority == false) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeEquipement();
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            string[] ss = { "asdas", "ffff", "wader" };
            Dialogue d = new Dialogue();
            d.sentences = ss;
            d.name = "The Rock";
            DialogueTrigger dt = new DialogueTrigger(d);
            dt.TriggerDialogue();
        }
    }
    /*===================================================================================================================
     * Function
     * 
     ===================================================================================================================*/
    public void ChangeEquipement()
    {
        int weapon = Random.Range(0, 7);// item.getWeapon();
        int armor = Random.Range(0, 4);// item.getArmor();
        int combination = (armor * 21 + weapon * 3);
        spriteDefault = sprites[combination];
        spriteAttack = sprites[combination + 1];
        spriteInteraction = sprites[combination + 2];
        spriteRenderer.sprite = spriteDefault;
    }
}
