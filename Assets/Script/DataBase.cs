using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityNpgsql;

public class DataBase : MonoBehaviour
{
    private DataTable m_dbTable;

    string strConnection = "Server=localhost; Port=5432; DataBase=dbsmigeon; Username=postgres; Password=Milena14 ";

    NpgsqlConnection dbConnection = null;
    NpgsqlCommand dbCmd = null;

    DataTable SelectWeapons()
    {

        DataTable dbTable = new DataTable();
        NpgsqlDataAdapter dbAdapter;

        dbConnection = new NpgsqlConnection(strConnection);
        dbConnection.Open();

        string strSelect = "SELECT * FROM \"weapon\"";

        dbCmd = new NpgsqlCommand(strSelect, dbConnection);

        dbAdapter = new NpgsqlDataAdapter(dbCmd);

        dbAdapter.Fill(dbTable);

        dbConnection.Close();

        foreach (DataRow row in dbTable.Rows)
        {
            foreach (var item in row.ItemArray)
            {
                Debug.Log("Item: ");
                Debug.Log(item);
                
            }
        }

        return dbTable;
    }

    // Start is called before the first frame update
    void Start()
    {
    
        m_dbTable = new DataTable();

        m_dbTable = SelectWeapons();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
