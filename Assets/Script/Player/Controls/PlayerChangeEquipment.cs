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

    private int weaponId;
    
    private int armorId;

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
        
        item.setArmor(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().equipement.getArmor());
        item.setWeapon(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().equipement.getWeapon());
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
