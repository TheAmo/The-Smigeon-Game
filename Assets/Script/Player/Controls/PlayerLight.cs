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
    [SyncVar]
    public bool lanternLightToggle;

    public GameObject lantern;

    /*===================================================================================================================
     * On Start
     * 
     ===================================================================================================================*/
    void Start()
    {
        if (!hasAuthority) return;
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

        if (hasAuthority == false) return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            lanternLightToggle = !lanternLightToggle;
            CmdToggleLight(lanternLightToggle);
        }
    }

    [Command]
    void CmdToggleLight(bool lanternio)
    {
        lanternLightToggle = lanternio;
        lantern.SetActive(lanternLightToggle);
    }
}
