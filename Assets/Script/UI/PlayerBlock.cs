using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBlock : MonoBehaviour
{
    public Text playerName, playerClass, playerExperience, playerGold;

    public void Display(PlayerEntry player)
    {
        playerName.text = player.name;
        playerClass.text = player.className;
        playerExperience.text = "Experience"+player.experience.ToString()+"exp";
        playerGold.text = "Gold"+player.gold.ToString()+"gp";
    }
}
