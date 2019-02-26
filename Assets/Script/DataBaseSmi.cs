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
    private double m_valWea;
    private double m_valMa;
 
    private string strConnection = "Server=localhost; Port=5432; DataBase=dbsmigeon; Username=postgres; Password=Milena14";

    private NpgsqlConnection dbConnection = null;
    private NpgsqlCommand dbCmd = null;
    private NpgsqlDataReader dbReader = null;

    public DataTable SelectWeapons()
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

    public DataTable SelectMaterials()
    {
        m_dbTableMa = new DataTable();
        NpgsqlDataAdapter dbAdapter;

        dbConnection = new NpgsqlConnection(strConnection);
        dbConnection.Open();

        string strSelect = "SELECT * FROM \"material\"";

        dbCmd = new NpgsqlCommand(strSelect, dbConnection);

        dbAdapter = new NpgsqlDataAdapter(dbCmd);

        dbAdapter.Fill(m_dbTableMa);

        dbConnection.Close();
        
        return m_dbTableMa;
    }

    public DataTable SelectWeaponsMaterials()
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

    public List<Items> getWeapons()
    {
        List<Items> weapons = new List<Items>();
        string name;
        int id, damage, defense;
        double price;

        DataTable m_dbTableWea = new DataTable();
        m_dbTableWea = SelectWeapons();

        for (int i = 0; i < m_dbTableWea.Rows.Count; i++)
        {
            id = Convert.ToInt32(m_dbTableWea.Rows[i]["id"]);
            name = (m_dbTableWea.Rows[i]["name"]).ToString();
            damage = Convert.ToInt32(m_dbTableWea.Rows[i]["damage"]);
            defense = Convert.ToInt32(m_dbTableWea.Rows[i]["defense"]);
            price = Convert.ToDouble(m_dbTableWea.Rows[i]["price"]);

            weapons.Add(new Items(id, name, damage, defense, price));
        }

        return weapons;
    }

    public List<Items> getMaterials()
    {
        List<Items> materials = new List<Items>();
        string name;
        int id, damage, defense;
        double price;

        DataTable m_dbTableMa = new DataTable();
        m_dbTableMa = SelectMaterials();

        for (int i = 0; i < m_dbTableMa.Rows.Count; i++)
        {
            id = Convert.ToInt32(m_dbTableMa.Rows[i]["id"]);
            name = (m_dbTableMa.Rows[i]["name"]).ToString();
            damage = Convert.ToInt32(m_dbTableMa.Rows[i]["damage"]);
            defense = Convert.ToInt32(m_dbTableMa.Rows[i]["defense"]);
            price = Convert.ToDouble(m_dbTableMa.Rows[i]["price"]);

            materials.Add(new Items(id, name, damage, defense, price));
            
        }

        return materials;
    }

    public double getWeaInfo(String info, String name, String mat)
    {
        dbConnection = new NpgsqlConnection(strConnection);
        dbConnection.Open();

        string strSelectWea = "SELECT " + info + " FROM weapon WHERE weapon."+ info + " =  \'" + name + "\'";
        string strSelectMa = "SELECT " + info + " FROM material WHERE material." + info +" =  \'" + mat + "\'";


        using (dbCmd = new NpgsqlCommand(strSelectWea, dbConnection))
        {
            dbReader = dbCmd.ExecuteReader();
            while (dbReader.Read())
            {
                m_valWea = double.Parse(dbReader[0].ToString());
                m_valWea = double.Parse(dbReader[0].ToString());
            }
        }

        using (dbCmd = new NpgsqlCommand(strSelectMa, dbConnection))
        {
            dbReader = dbCmd.ExecuteReader();
            while (dbReader.Read())
            {
                m_valMa = double.Parse(dbReader[0].ToString());
            }
        }

        dbConnection.Close();

        return (m_valWea + m_valMa);
    }

   

    // Start is called before the first frame update
    void Start()
    {
        string info = "price";
        string name = "mace";
        string mat = "bronze";
        double price = getWeaInfo(info, name, mat);

    }

    // Update is called once per frame
    void Update()
    {
   

    }

    
}
