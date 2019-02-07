using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNpgsql;

public class Items : MonoBehaviour
{
    string strConnection = "Server=localhost; Port=5432; DataBase=postgres; Username=postgres; Password=Milena14 ";

    NpgsqlConnection dbConnection = null;
    NpgsqlCommand dbCmd = null;


    // Start is called before the first frame update
    void Start()
    {
        dbConnection = new NpgsqlConnection(strConnection);


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
