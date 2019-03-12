using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{
    public int m_player_id;
    public GameObject PlayerObjectPrefab;
    public GameObject PlayerCameraPrefab;
    public GameObject LightLanternPrefab;
    public GameObject HudPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
        if (!isLocalPlayer)
        {
            Debug.Log("PlayerConnection::Start --Is not local");
            return;
        }

        Destroy(GameObject.Find("Camera"));

        Debug.Log("PlayerConnection::Start --Spawning a personal player");

        canvas = Instantiate(HudPrefab);
        lightLantern = Instantiate(LightLanternPrefab);


        CmdSpawnMyUnit();

        playerCamera=Instantiate(PlayerCameraPrefab);

        playerCamera.GetComponent<CameraController>().setPlayer(player);
        lightLantern.GetComponent<CameraController>().setPlayer(player)
            ;
 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*===================================================================================================================
    * Commands
    * 
    ===================================================================================================================*/
    GameObject playerCamera;
    GameObject lightLantern;
    GameObject canvas;
    GameObject player;

    [Command]
    void CmdSpawnMyUnit()
    {
        //We are on the server
        GameObject go = Instantiate(PlayerObjectPrefab);


        player = go;
        
        player.GetComponent<Player>().canvas = canvas;
       

        player.GetComponent<Player>().player = player;
        player.GetComponent<PlayerAttack>().player = player;
        player.GetComponent<PlayerInteraction>().player = player;
        player.GetComponent<PlayerLight>().lantern = lightLantern;

        lightLantern.GetComponent<CameraController>().player = player;

        player.GetComponent<PlayerLight>().lantern = lightLantern;

        //We have to propagate the object on all the client
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
        //NetworkServer.SpawnWithClientAuthority(ll, connectionToClient);
        //NetworkServer.SpawnWithClientAuthority(can, connectionToClient);
    }
    /*===================================================================================================================
    * Save
    * 
    ===================================================================================================================*/
    /*
    public void SavePlayer()
    {
        GameObject.Find("XML Players Manager").GetComponent<XMLPlayerManagement>().playerDB.playerList[m_player_id].position[0] = go.transform.position.x;
        GameObject.Find("XML Players Manager").GetComponent<XMLPlayerManagement>().playerDB.playerList[m_player_id].position[1] = go.transform.position.y;

        Debug.Log("Saved Position " + GameObject.Find("XML Players Manager").GetComponent<XMLPlayerManagement>().playerDB.playerList[m_player_id].position[0] + "," + GameObject.Find("XML Players Manager").GetComponent<XMLPlayerManagement>().playerDB.playerList[m_player_id].position[1] + ")");
        GameObject.Find("XML Players Manager").GetComponent<XMLPlayerManagement>().SavePlayer();
        Debug.Log("Saved Player");
    }
    */
}
