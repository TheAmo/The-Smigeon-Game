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
        Destroy(GameObject.Find("Camera"));

        if (isLocalPlayer == false)
        {
            return;
        }

        Debug.Log("PlayerConnection::Start --Spawning a personal player");

        CmdSpawnMyUnit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*===================================================================================================================
    * Commands
    * 
    ===================================================================================================================*/
    [Command]
    void CmdSpawnMyUnit()
    {
        //We are on the server
        GameObject go = Instantiate(PlayerObjectPrefab);
        GameObject ll = Instantiate(LightLanternPrefab);
        GameObject pc = Instantiate(PlayerCameraPrefab);
        GameObject can = Instantiate(HudPrefab);

        go.GetComponent<Player>().canvas = can;
        pc.GetComponent<CameraController>().player = go;

        go.GetComponent<Player>().player = go;
        go.GetComponent<PlayerAttack>().player = go;
        go.GetComponent<PlayerInteraction>().player = go;

        ll.GetComponent<CameraController>().player = go;

        go.GetComponent<PlayerLight>().lantern = ll;


        //We have to propagate the object on all the client
        NetworkServer.Spawn(go);
        NetworkServer.Spawn(ll);
        NetworkServer.Spawn(pc);
        NetworkServer.Spawn(can);
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
