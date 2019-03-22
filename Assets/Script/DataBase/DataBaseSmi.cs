using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityNpgsql;

public class DataBaseSmi : MonoBehaviour
{
    /*===================================================================================================================
    * Data Table
    * 
    ===================================================================================================================*/
    private DataTable m_dbTable;
    private DataTable m_dbTableWeapon;
    private DataTable m_dbTableMaterial;
    private DataTable m_dbTableMaterialWeapon;
    private DataTable m_dbTableMonsterStats;
    private DataTable m_dbtmpTable;

    private double m_val;
    private double m_valMaterial;

    /*===================================================================================================================
    * Connection
    * 
    ===================================================================================================================*/
    private string strConnection = "Server=localhost; Port=5432; DataBase=dbsmigeon; Username=postgres; Password=Milena14";
    private NpgsqlConnection dbConnection = null;
    private NpgsqlCommand dbCmd = null;
    private NpgsqlDataReader dbReader = null;

    private DataTable Connection(string strQuery)
    {
        m_dbTable = new DataTable();
        NpgsqlDataAdapter dbAdapter;

        dbConnection = new NpgsqlConnection(strConnection);

        dbConnection.Open();
        dbCmd = new NpgsqlCommand(strQuery, dbConnection);
        dbAdapter = new NpgsqlDataAdapter(dbCmd);
        dbAdapter.Fill(m_dbTable);
        dbConnection.Close();

        return m_dbTable;
    }

    private DataTable Select(string strSelect)
    {
        m_dbTable = new DataTable();

        m_dbTable = Connection(strSelect);

        return m_dbTable;
    }

    /*===================================================================================================================
 *** =================================================================================================================
 *** =================================================================================================================
 *** Stats
 *** 
 ***
 *** 
 *** =================================================================================================================
 *** =================================================================================================================
 ===================================================================================================================*/

    /*===================================================================================================================
     * Get Monster Stat by Id
     * 
    ===================================================================================================================*/
    public DataTable getMonsterInfoById(int id)
    {
        m_dbTableMonsterStats = new DataTable();
        m_dbTableMonsterStats = Connection("SELECT * FROM monster_stats WHERE id=" + id);

        return m_dbTableMonsterStats;
    }

    /*===================================================================================================================
    *** =================================================================================================================
    *** =================================================================================================================
    *** Items:
    *** 
    ***
    *** 
    *** =================================================================================================================
    *** =================================================================================================================
    ===================================================================================================================*/


    /*===================================================================================================================
    * Select Weapons and materials
    * 
    ===================================================================================================================*/

    public DataTable SelectWeaponsMaterials()
    {

        m_dbTableMaterialWeapon = new DataTable();

        string strSelect = "SELECT * FROM \"weapon_material\"";

        m_dbTableMaterialWeapon = Connection(strSelect);

        return m_dbTableMaterialWeapon;
    }

    /*===================================================================================================================
    * Select Weapon
    * 
    ===================================================================================================================*/
    public List<Items> getAllWeapons()
    {
        List<Items> weapons = new List<Items>();
        string name;
        int id, damage, defense;
        double price;

        string strSelect = "SELECT * FROM \"weapon\"";

        m_dbTableWeapon = new DataTable();
        m_dbTableWeapon = Select(strSelect);

        for (int i = 0; i < m_dbTableWeapon.Rows.Count; i++)
        {
            id = Convert.ToInt32(m_dbTableWeapon.Rows[i]["id"]);
            name = (m_dbTableWeapon.Rows[i]["name"]).ToString();
            damage = Convert.ToInt32(m_dbTableWeapon.Rows[i]["damage"]);
            defense = Convert.ToInt32(m_dbTableWeapon.Rows[i]["defense"]);
            price = Convert.ToDouble(m_dbTableWeapon.Rows[i]["price"]);

            weapons.Add(new Items(id, name, damage, defense, price));
        }

        return weapons;
    }

    /*===================================================================================================================
    * Select Material
    * 
    ===================================================================================================================*/
    public List<Items> getAllMaterials()
    {
        List<Items> materials = new List<Items>();
        string name;
        int id, damage, defense;
        double price;

        string strSelect = "SELECT * FROM \"material\"";

        m_dbTableMaterial = new DataTable();
        m_dbTableMaterial = Select(strSelect);

        for (int i = 0; i < m_dbTableMaterial.Rows.Count; i++)
        {
            id = Convert.ToInt32(m_dbTableMaterial.Rows[i]["id"]);
            name = (m_dbTableMaterial.Rows[i]["name"]).ToString();
            damage = Convert.ToInt32(m_dbTableMaterial.Rows[i]["damage"]);
            defense = Convert.ToInt32(m_dbTableMaterial.Rows[i]["defense"]);
            price = Convert.ToDouble(m_dbTableMaterial.Rows[i]["price"]);

            materials.Add(new Items(id, name, damage, defense, price));

        }

        return materials;
    }
   /*===================================================================================================================
   * Get Table value
   * 
   ===================================================================================================================*/
    private double getTableValue(String strSelect)
    {
        dbConnection = new NpgsqlConnection(strConnection);
        dbConnection.Open();

        using (dbCmd = new NpgsqlCommand(strSelect, dbConnection))
        {
            dbReader = dbCmd.ExecuteReader();
            while (dbReader.Read())
            {
                m_val = double.Parse(dbReader[0].ToString());
            }
        }
        dbConnection.Close();

        return m_val;
    }
    /*===================================================================================================================
    * Get Table info
    * armur = getTableInfo("armur", "player", " ")
    ===================================================================================================================*/
    public double getTableInfo(String info, String name, String table, String material)
    {
        string strSelect = "SELECT " + info + " FROM weapon WHERE " + table + "." + info + " =  \'" + name + "\'";

        m_val = getTableValue(strSelect);

        if (table == "weapon")
        {
            string strSelectMa = "SELECT " + info + " FROM material WHERE material." + info + " =  \'" + material + "\'";

            m_valMaterial = getTableValue(strSelectMa);

            return (m_val + m_valMaterial);
        }

        return m_val;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }


}
