using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerLight : NetworkBehaviour
{
    /*===================================================================================================================
     * Attribute
     * 
     ===================================================================================================================*/
    private bool lanternLightToggle;

    public GameObject lantern;

    /*===================================================================================================================
     * On Start
     * 
     ===================================================================================================================*/
    void Start()
    {
        //lantern = GameObject.Find("Light Lantern");
        lantern.SetActive(false);
        lanternLightToggle = false;
    }

    /*===================================================================================================================
     * On Update
     * 
     ===================================================================================================================*/
    void Update()
    {
        if (hasAuthority == false) return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            lanternLightToggle = !lanternLightToggle;
            lantern.SetActive(lanternLightToggle);
        }
    }
}
