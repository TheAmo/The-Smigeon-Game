using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityNpgsql;
using UnityNpgsqlTypes;

public class DataBaseSmi : MonoBehaviour
{
    /*===================================================================================================================
    * Data Table
    * 
    ===================================================================================================================*/
    private DataTable m_dbTable;

    private DataTable m_dbtmpTable;

    private string m_val;
    private int m_valI;
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
     * Get Monster Stat by Id
     * 
    ===================================================================================================================*/
    public DataTable getMonsterInfoById(int id)
    {
        m_dbTable = new DataTable();
        m_dbTable = Connection("SELECT * FROM monster_stats WHERE id=" + id);

        return m_dbTable;
    }

    /*===================================================================================================================
    * Get all materials
    * Select Material
    * 
    ===================================================================================================================*/
    public List<Items> getAllMaterials()
    {
        List<Items> materials = new List<Items>();
        string name;
        int id, damage, defense;
        double price;

        string strSelect = "SELECT id, name, price, damage, defense FROM material";

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);

        for (int i = 0; i < m_dbTable.Rows.Count; i++)
        {
            id = Convert.ToInt32(m_dbTable.Rows[i]["id"]);
            name = (m_dbTable.Rows[i]["name"]).ToString();
            damage = Convert.ToInt32(m_dbTable.Rows[i]["damage"]);
            defense = Convert.ToInt32(m_dbTable.Rows[i]["defense"]);
            price = Convert.ToDouble(m_dbTable.Rows[i]["price"]);

            materials.Add(new Items(id, name, damage, defense, price));

        }

        return materials;
    }
    /*===================================================================================================================
    * Select player entry
    * 
    ===================================================================================================================*/
    public List<PlayerEntryDB> getPlayerEntry()
    {
        List<PlayerEntryDB> playerList = new List<PlayerEntryDB>();
        string name, className;
        int experience, gold;
        var position = new float[2];

        string strSelect = "SELECT name, class_name, positionx, positiony, gold FROM \"player_entry\"";

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);

        for (int i = 0; i < m_dbTable.Rows.Count; i++)
        {
            name = (m_dbTable.Rows[i]["name"]).ToString();
            className = (m_dbTable.Rows[i]["class_name"]).ToString();
            experience = Convert.ToInt32(m_dbTable.Rows[i]["experience"]);
            position[0] = Convert.ToInt32(m_dbTable.Rows[i]["positionx"]);
            position[1] = Convert.ToInt32(m_dbTable.Rows[i]["positiony"]);
            gold = Convert.ToInt32(m_dbTable.Rows[i]["gold"]);

            playerList.Add(new PlayerEntryDB(name, className, experience, position, gold));
        }

        return playerList;
    }
    /*===================================================================================================================
    * Get Name 
    * 
    ===================================================================================================================*/
    public string getMaterialName(int i)
    {
        string strSelect = "SELECT name FROM \"material\" WHERE id = " + (i + 1);

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);

        string name = (m_dbTable.Rows[0]["name"]).ToString();
        
        return name;
    }
    /*===================================================================================================================
    * Get Class name 
    * 
    ===================================================================================================================*/
    public string getClassName(string table, int id)
    {
        string strSelect = "SELECT class_name FROM \""+ table +"\" WHERE id = " + id;

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);

        string class_name = (m_dbTable.Rows[0]["class_name"]).ToString();

        return class_name;
    }
    /*===================================================================================================================
    * Get position x and position y
    * 
    ===================================================================================================================*/
    public int getPosition(string table, int id, string pos)
    {
        string strSelect = "SELECT position" + pos + " FROM \"" + table + "\" WHERE id = " + id;

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);

        int position = Convert.ToInt16(m_dbTable.Rows[0]["position"]);

        return position;
    }
    /*===================================================================================================================
    * Get experience
    * 
    ===================================================================================================================*/
    public int getExperience(string table, int id)
    {
        string strSelect = "SELECT experience FROM \"" + table + "\" WHERE id = " + id;

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);

        int experience = Convert.ToInt32(m_dbTable.Rows[0]["experience"]);

        return experience;
    }
    /*===================================================================================================================
    * Get gold
    * 
    ===================================================================================================================*/
    public int getGold(string table, int id)
    {
        string strSelect = "SELECT gold FROM \"" + table + "\" WHERE id = " + id;

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);

        int gold = Convert.ToInt32(m_dbTable.Rows[0]["gold"]);

        return gold;
    }
    /*===================================================================================================================
    * Get damage
    * 
    ===================================================================================================================*/
    public int getMaterialDamage(int id)
    {
        string strSelect = "SELECT damage FROM \"material\" WHERE id = " + id + 1;

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);
        int damage = Convert.ToInt16(m_dbTable.Rows[0]["damage"]);

        Debug.Log("--------------------------------------getMaterialDamage" + damage);

        return damage;
    }
    /*===================================================================================================================
    * Get defense
    * 
    ===================================================================================================================*/
    public int getDefense(string table, int id)
    {
        string strSelect = "SELECT defense FROM \"" + table + "\" WHERE id = " + id;

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);
        int defense = Convert.ToInt16(m_dbTable.Rows[0]["defense"]);
        return defense;
    }
    /*===================================================================================================================
    * Get price
    * 
    ===================================================================================================================*/
    public int getPrice(int i)
    {
        string temp;
        string strSelect = "SELECT price FROM \"material\" WHERE id = " + i + 1;

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);
        temp = (m_dbTable.Rows[0]["price"]).ToString();
        int price = Convert.ToInt16(temp);
        return price;
    }


    /*===================================================================================================================
     * Sauvegarde
     * 
    ===================================================================================================================*/
    public void SaveInfoPlayerEntry(string infoToSave, int id, string newValue)
    {

        string strQuery = "UPDATE player_entry SET \"" + infoToSave + "\" = :infoToSave WHERE \"id\" = :ID";
        dbConnection = new NpgsqlConnection(strConnection);

        dbConnection.Open();

        dbCmd = new NpgsqlCommand(strQuery, dbConnection);
        dbCmd.Parameters.Add(new NpgsqlParameter("infoToSave", NpgsqlDbType.Text));
        dbCmd.Parameters.Add(new NpgsqlParameter("ID", NpgsqlDbType.Integer));
        dbCmd.Parameters[0].Value = newValue;
        dbCmd.Parameters[1].Value = id;
        dbCmd.ExecuteNonQuery();

        Debug.Log("UpdateWeapon");

        dbConnection.Close();
    }

    public void SavePlayer(string name, int experience, string weapon, string armor, float[] position)
    {




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
