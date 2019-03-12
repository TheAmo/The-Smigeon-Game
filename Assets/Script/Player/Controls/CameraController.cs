using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour
{
    public GameObject player;
    public int height;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 temp = new Vector3(0,70,height);
        transform.position = temp;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       Vector3 temp = new Vector3(player.transform.position.x, player.transform.position.y, height);
       transform.position = temp;
    }
}
