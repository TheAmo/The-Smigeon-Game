using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    /*===================================================================================================================
     * Attribute
     * 
     ===================================================================================================================*/
    private short lanternLightToggle;

    private GameObject lantern;

    /*===================================================================================================================
     * On Start
     * 
     ===================================================================================================================*/
    void Start()
    {
        lantern = GameObject.Find("Light Lantern");
        lanternLightToggle = -1;
    }

    /*===================================================================================================================
     * On Update
     * 
     ===================================================================================================================*/
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (lanternLightToggle == -1)
            {
                lanternLightToggle *= -1;
                lantern.SetActive(true);
            }
            else if (lanternLightToggle == 1)
            {
                lanternLightToggle *= -1;
                lantern.SetActive(false);
            }
        }
    }
}
