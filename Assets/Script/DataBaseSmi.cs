using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityNpgsql;

public class DataBaseSmi : MonoBehaviour
{
    private DataTable m_dbTable;
    private DataTable m_dbTableWea;
    private DataTable m_dbTableMa;
    private DataTable m_dbTableMaWea;


    private string strConnection = "Server=localhost; Port=5432; DataBase=dbsmigeon; Username=postgres; Password=Milena14 ";

    private NpgsqlConnection dbConnection = null;
    private NpgsqlCommand dbCmd = null;

    DataTable SelectWeapons()
    {
         m_dbTableWea = new DataTable();
        NpgsqlDataAdapter dbAdapter;

        dbConnection = new NpgsqlConnection(strConnection);
        dbConnection.Open();

        string strSelect = "SELECT * FROM \"weapon\"";

        dbCmd = new NpgsqlCommand(strSelect, dbConnection);

        dbAdapter = new NpgsqlDataAdapter(dbCmd);

        dbAdapter.Fill(m_dbTableWea);

        dbConnection.Close();

        return m_dbTableWea;
    }

    DataTable SelectMaterials()
    {
        m_dbTableMa = new DataTable();
        NpgsqlDataAdapter dbAdapter;

        dbConnection = new NpgsqlConnection(strConnection);
        dbConnection.Open();

        string strSelect = "SELECT * FROM \"weapon\"";

        dbCmd = new NpgsqlCommand(strSelect, dbConnection);

        dbAdapter = new NpgsqlDataAdapter(dbCmd);

        dbAdapter.Fill(m_dbTableMa);

        dbConnection.Close();

        return m_dbTableMa;
    }


    DataTable SelectWeaponsMaterials()
    {
        m_dbTableMaWea = new DataTable();
        NpgsqlDataAdapter dbAdapter;

        dbConnection = new NpgsqlConnection(strConnection);
        dbConnection.Open();

        string strSelect = "SELECT * FROM \"weapon_material\"";

        dbCmd = new NpgsqlCommand(strSelect, dbConnection);

        dbAdapter = new NpgsqlDataAdapter(dbCmd);

        dbAdapter.Fill(m_dbTableMaWea);

        dbConnection.Close();


        return m_dbTableMaWea;
    }

    List<Items> getWeapons()
    {
        List<Items> weapons = new List<Items>();
        string name;
        int damage, defense, price;

        for (int i = 0; i < m_dbTableWea.Rows.Count; i++)
        {
            name = (m_dbTableWea.Rows[i]["name"]).ToString();
            damage = Convert.ToInt32(m_dbTableWea.Rows[i]["damage"]);
            defense = Convert.ToInt32(m_dbTableWea.Rows[i]["defense"]);
            price = Convert.ToInt32(m_dbTableWea.Rows[i]["price"]);

            Items item1 = new Items(name, damage, defense, price);

            weapons.Add(item1);
        }

        return weapons;
    }

    List<Items> getMaterials()
    {
        List<Items> materials = new List<Items>();
        string name;
        int damage, defense, price;

        for (int i = 0; i < m_dbTableWea.Rows.Count; i++)
        {
            name = (m_dbTableWea.Rows[i]["name"]).ToString();
            damage = Convert.ToInt32(m_dbTableWea.Rows[i]["damage"]);
            defense = Convert.ToInt32(m_dbTableWea.Rows[i]["defense"]);
            price = Convert.ToInt32(m_dbTableWea.Rows[i]["price"]);

            Items item1 = new Items(name, damage, defense, price);

            materials.Add(item1);
        }

        return materials;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_dbTable = new DataTable();

        m_dbTable = SelectWeapons();

        List<Items> weapons = getWeapons();

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
