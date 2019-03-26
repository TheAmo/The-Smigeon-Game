using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    public PlayerBlock blockPrefab;
    
    void Start()
    {
        Display();
    }

    public void Display()
    {
        foreach(PlayerEntry player in DBPlayerManagement.ins.playerDB.playerList)
        {
            Debug.Log("loaded " + player.name);
            PlayerBlock newBlock = Instantiate(blockPrefab) as PlayerBlock;
            newBlock.transform.SetParent(transform, false);
            newBlock.Display(player);
        }
    }
}
