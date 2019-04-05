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

    [SyncVar]
    private int weaponId;
    [SyncVar]
    private int armorId;

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
        item.setArmor(armorId);
        item.setWeapon(weaponId);
        WearEquipment(armorId, weaponId);

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
        if (hasAuthority == false) return;
        int weapon = Random.Range(0, 7);// item.getWeapon();
        int armor = Random.Range(0, 4);// item.getArmor();

        CmdChangePlayerEquipment(armor, weapon);

        if (hasAuthority == false) return;
        item.setArmor(armor);
        item.setWeapon(weapon);


    }

    public void WearEquipment(int armor,int weapon)
    {
        int combination = (armor * 21 + weapon * 3);
        spriteDefault = sprites[combination];
        spriteAttack = sprites[combination + 1];
        spriteInteraction = sprites[combination + 2];
    }

    [Command]
    void CmdChangePlayerEquipment(int armor, int weapon)
    {
        Debug.Log("Sending new Equipment");

        weaponId = weapon;
        armorId = armor;
    }
}
