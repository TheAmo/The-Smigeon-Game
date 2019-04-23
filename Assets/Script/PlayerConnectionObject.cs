using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{
    public int m_player_id;
    public GameObject PlayerObjectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
        if (!isLocalPlayer)
        {
            Debug.Log("PlayerConnection::Start --Is not local");
            return;
        }
      
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
    GameObject player;
    [Command]
    void CmdSpawnMyUnit()
    {
        //We are on the server
        GameObject go = Instantiate(PlayerObjectPrefab);

        player = go;
    
        //We have to propagate the object on all the client
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

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
