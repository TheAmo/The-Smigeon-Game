using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour { 
    /*===================================================================================================================
     * Attribute
     * 
     ===================================================================================================================*/

    public bool lanternLightToggle;

    public GameObject lantern;

    /*===================================================================================================================
     * On Start
     * 
     ===================================================================================================================*/
    void Start()
    {
        
        lantern.SetActive(true);
        lanternLightToggle = true;
    }

    /*===================================================================================================================
     * On Update
     * 
     ===================================================================================================================*/
    void Update()
    {
        lantern.SetActive(lanternLightToggle);

       
        if (Input.GetKeyDown(KeyCode.R))
        {
            lanternLightToggle = !lanternLightToggle;
           
        }
    }

    
}
