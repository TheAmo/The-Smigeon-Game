using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private int goldUpdated;
    private SpriteRenderer spriteRenderer;
    private Sprite spriteChestOpened;
    private Sprite sprite1;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteChestOpened = Resources.Load<Sprite>("Chest-2");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            spriteRenderer.sprite = spriteChestOpened;

            Player player = new Player();
            Money money = new Money();

            goldUpdated = player.GetComponent<Player>().stats.getGold() + 500;
            money.changeMoney(goldUpdated);
            player.GetComponent<Player>().stats.setGold(goldUpdated);

            Debug.Log("--------------------- gold updated (CHEST) : " + goldUpdated);
            
        }
    }
}
