using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerChangeEquipment : MonoBehaviour
{
    /*===================================================================================================================
     * Attribute
     * 
     ===================================================================================================================*/
    private SpriteRenderer spriteRenderer;

    private int weaponId=1;
    
    private int armorId=1;

    public Sprite spriteDefault;
    public Sprite spriteAttack;
    public Sprite spriteInteraction;

    public Stats stats;
    public Equipement item;

    public Sprite[] sprites;

    public GameObject player;

    /*===================================================================================================================
     * On Start
     * 
     ===================================================================================================================*/
    void Start()
    {
        item.setArmor(1);
        item.setWeapon(1);
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
        if (stats.getHitPoint() <= 0) { return; }
        item.setArmor(armorId);
        item.setWeapon(weaponId);

        //if (hasAuthority == false) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeEquipement();
            Debug.Log("Changing armor to" + armorId + "Changing Weapon to " + weaponId);
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
        int weapon = item.getWeapon();
        int armor = item.getArmor();
        item.setArmor(armor);
        item.setWeapon(weapon);

        int combination = (armor * 21 + weapon * 3);
        Debug.Log(combination);

        spriteDefault = sprites[combination];
        spriteAttack = sprites[combination + 1];
        spriteInteraction = sprites[combination + 2];
    }

  
}
