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

    /*===================================================================================================================
    * Connection
    * 
    ===================================================================================================================*/
    private string strConnection = "Server=localhost; Port=5432; DataBase=dbsmigeon; Username=postgres; Password=Milena14";

    private NpgsqlConnection dbConnection = null;
    private NpgsqlCommand dbCmd = null;

    private DataTable receiveFromQuerry(string querry)
    {
        NpgsqlDataAdapter dbAdapter;

        dbConnection = new NpgsqlConnection(strConnection);

        dbConnection.Open();
        dbCmd = new NpgsqlCommand(querry, dbConnection);
        dbAdapter = new NpgsqlDataAdapter(dbCmd);
        dbAdapter.Fill(m_dbtmpTable);
        dbConnection.Close();

        Debug.Log("Database connection succesful: " + querry);

        return (m_dbtmpTable);
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
        m_dbTableMonsterStats = receiveFromQuerry("SELECT * FROM monster_stats WHERE id=" + id);
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
    * Select Weapon
    * 
    ===================================================================================================================*/
    public DataTable SelectWeapons()
    {
         m_dbTableWeapon = new DataTable();
        NpgsqlDataAdapter dbAdapter;

        dbConnection = new NpgsqlConnection(strConnection);
        dbConnection.Open();

        string strSelect = "SELECT * FROM \"weapon\"";

        dbCmd = new NpgsqlCommand(strSelect, dbConnection);

        dbAdapter = new NpgsqlDataAdapter(dbCmd);

        dbAdapter.Fill(m_dbTableWeapon);

        dbConnection.Close();

        return m_dbTableWeapon;
    }
    /*===================================================================================================================
    * Select Material
    * 
    ===================================================================================================================*/
    public DataTable SelectMaterials()
    {
        m_dbTableMaterial = new DataTable();
        NpgsqlDataAdapter dbAdapter;

        dbConnection = new NpgsqlConnection(strConnection);
        dbConnection.Open();

        string strSelect = "SELECT * FROM \"material\"";

        dbCmd = new NpgsqlCommand(strSelect, dbConnection);

        dbAdapter = new NpgsqlDataAdapter(dbCmd);

        dbAdapter.Fill(m_dbTableMaterial);

        dbConnection.Close();
        
        return m_dbTableMaterial;
    }
    /*===================================================================================================================
    * Select weapon material
    * 
    ===================================================================================================================*/
    public DataTable SelectWeaponsMaterials()
    {
        m_dbTableMaterialWeapon = new DataTable();
        NpgsqlDataAdapter dbAdapter;

        dbConnection = new NpgsqlConnection(strConnection);
        dbConnection.Open();

        string strSelect = "SELECT * FROM \"weapon_material\"";

        dbCmd = new NpgsqlCommand(strSelect, dbConnection);

        dbAdapter = new NpgsqlDataAdapter(dbCmd);

        dbAdapter.Fill(m_dbTableMaterialWeapon);

        dbConnection.Close();

        return m_dbTableMaterialWeapon;

    }
    /*===================================================================================================================
    * Get Weapons
    * 
    ===================================================================================================================*/
    public List<Items> getWeapons()
    {
        List<Items> weapons = new List<Items>();
        string name;
        int id, damage, defense, price;

        DataTable m_dbTableWea = new DataTable();
        m_dbTableWea = SelectWeapons();

        for (int i = 0; i < m_dbTableWea.Rows.Count; i++)
        {
            id = Convert.ToInt32(m_dbTableWea.Rows[i]["id"]);
            name = (m_dbTableWea.Rows[i]["name"]).ToString();
            damage = Convert.ToInt32(m_dbTableWea.Rows[i]["damage"]);
            defense = Convert.ToInt32(m_dbTableWea.Rows[i]["defense"]);
            price = Convert.ToInt32(m_dbTableWea.Rows[i]["price"]);

            weapons.Add(new Items(id, name, damage, defense, price));
        }

        return weapons;
    }
    /*===================================================================================================================
    * Get Material
    * 
    ===================================================================================================================*/
    public List<Items> getMaterials()
    {
        List<Items> materials = new List<Items>();
        string name;
        int id, damage, defense, price;

        DataTable m_dbTableMa = new DataTable();
        m_dbTableMa = SelectMaterials();

        //Debug.Log(m_dbTableMa.Rows.Count);
        for (int i = 0; i < m_dbTableMa.Rows.Count; i++)
        {
            id = Convert.ToInt32(m_dbTableMa.Rows[i]["id"]);
            name = (m_dbTableMa.Rows[i]["name"]).ToString();
            damage = Convert.ToInt32(m_dbTableMa.Rows[i]["damage"]);
            defense = Convert.ToInt32(m_dbTableMa.Rows[i]["defense"]);
            price = Convert.ToInt32(m_dbTableMa.Rows[i]["price"]);

            //Debug.Log(id + " " + name + " " + damage + " " + defense + " " + price);

            materials.Add(new Items(id, name, damage, defense, price));
            
        }

        return materials;
    }
    /*===================================================================================================================
    * Get Material Weapon
    * 
    ===================================================================================================================*/
    public List<Items> getMaterialWeapon()
    {
        int id_weapon, id_material;

        List<Items> weapons = new List<Items>();
        weapons = getWeapons();

        List<Items> materials = new List<Items>();
        materials = getMaterials();

        List<Items> weapon_material = new List<Items>();

        DataTable m_dbTableMaWea = new DataTable();
        m_dbTableMaWea = SelectWeaponsMaterials();

        for (int i = 0; i < m_dbTableMaWea.Rows.Count; i++)
        {
            id_weapon = Convert.ToInt32(m_dbTableMaWea.Rows[i]["id_weapon"]);
            id_material = Convert.ToInt32(m_dbTableMaWea.Rows[i]["id_material"]);

            for (int j = 0; j < weapons.Count; j++)
            {
                if (id_weapon == weapons[j].getId())
                {
                    Debug.Log("                    DANS IF ");
                    weapon_material.Add(new Items(weapons[j].getName(),
                        weapons[j].getDamage() + materials[id_material - 1].getDamage(),
                        weapons[j].getDefense() + materials[id_material - 1].getDefense(),
                        weapons[j].getPrice() + materials[id_material - 1].getPrice(),
                        materials[id_material - 1].getName()));
                    Debug.Log(weapon_material[j].getName() + " " + weapon_material[j].getDamage() + " " + weapon_material[j].getDefense() + " " + weapon_material[j].getPrice() + " " + weapon_material[j].getMaterial() + "                       ITEM ");
                }
                else Debug.Log(id_weapon + " != " + weapons[j].getId());
            }
        }

        return weapon_material;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("avant");
        List<Items> weaponMaterial = getMaterialWeapon();
        Debug.Log("apres");

    }

    // Update is called once per frame
    void Update()
    {
   

    }
}
