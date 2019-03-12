using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            lanternLightToggle = !lanternLightToggle;
            lantern.SetActive(lanternLightToggle);
        }
    }
}
