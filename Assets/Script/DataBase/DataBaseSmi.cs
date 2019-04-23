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
     * Get Monster Stats List by id
     * 
    ===================================================================================================================*/
    public List<MonsterStats> getMonster(int id)
    {
        List<MonsterStats> monster = new List<MonsterStats>();
        string name;
        int xp, hitpoint, armorclass, attackbonus, damagebonus, damagedice;

        string strSelect = "SELECT name, xp, hitpoint, armorclass, attackbonus, damagebonus, damagedice FROM monster_stats WHERE id = " + id;

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);

        for (int i = 0; i < m_dbTable.Rows.Count; i++)
        {
            name = (m_dbTable.Rows[i]["name"]).ToString();
            xp = Convert.ToInt16(m_dbTable.Rows[i]["xp"]);
            hitpoint = Convert.ToInt16(m_dbTable.Rows[i]["hitpoint"]);
            armorclass = Convert.ToInt16(m_dbTable.Rows[i]["armorclass"]);
            attackbonus = Convert.ToInt16(m_dbTable.Rows[i]["attackbonus"]);
            damagebonus = Convert.ToInt16(m_dbTable.Rows[i]["damagebonus"]);
            damagedice = Convert.ToInt16(m_dbTable.Rows[i]["damagedice"]);

            monster.Add(new MonsterStats(id, name, xp, hitpoint, armorclass, attackbonus, damagebonus, damagedice));
        }
        return monster;
    }
    /*===================================================================================================================
    * Get all materials
    * Select Material
    * 
    ===================================================================================================================*/
    public List<Items> getAllMaterials()
    {
        List<Items> material = new List<Items>();
        string name;
        int id, damage, price;

        string strSelect = "SELECT id, name, price, damage FROM material";

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);

        for (int i = 0; i < m_dbTable.Rows.Count; i++)
        {
            id = Convert.ToInt16(m_dbTable.Rows[i]["id"]);
            name = (m_dbTable.Rows[i]["name"]).ToString();
            damage = Convert.ToInt16(m_dbTable.Rows[i]["damage"]);
            price = Convert.ToInt16(m_dbTable.Rows[i]["price"]);

            material.Add(new Items(id, name, damage, price));

        }

        return material;
    }
    /*===================================================================================================================
    * Get all armors
    * Select Armor
    * 
    ===================================================================================================================*/
    public List<Items> getAllArmors()
    {
        List<Items> armor = new List<Items>();
        string name;
        int id, defense, price;

        string strSelect = "SELECT id, name, price, defense FROM armor";

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);

        for (int i = 0; i < m_dbTable.Rows.Count; i++)
        {
            id = Convert.ToInt16(m_dbTable.Rows[i]["id"]);
            name = (m_dbTable.Rows[i]["name"]).ToString();
            price = Convert.ToInt16(m_dbTable.Rows[i]["price"]);
            defense = Convert.ToInt16(m_dbTable.Rows[i]["defense"]);

            Debug.Log("ID : " + id + " name : " + name + " price : " + price + " defense : " + defense);

            armor.Add(new Items(id, name, defense, price));
        }

        return armor;
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

        string strSelect = "SELECT name, class_name, positionx, positiony, gold FROM player_entry";

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
    * Get Name  by id
    * Get material name or armor name
    ===================================================================================================================*/
    public string getItemName(int id, string table)
    {
        id++;
        string strSelect = "SELECT name FROM " + table + " WHERE id = " + id;

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);

        string name = (m_dbTable.Rows[0]["name"]).ToString();

        Debug.Log("id : " + id + "name : " + name);

        return name;
    }
    /*===================================================================================================================
    * Get Class name by id
    * 
    ===================================================================================================================*/
    public string getClassName(string table, int id)
    {
        string strSelect = "SELECT class_name FROM \"" + table + "\" WHERE id = " + id;

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);

        string class_name = (m_dbTable.Rows[0]["class_name"]).ToString();

        return class_name;
    }
    /*===================================================================================================================
    * Get position x and position y by id
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
    * Get experience by id
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
    * Get gold by id
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
    * Get material damage by id
    * 
    ===================================================================================================================*/
    public int getMaterialDamage(int id)
    {
        id++;
        string strSelect = "SELECT damage FROM \"material\" WHERE id = " + id;

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);
        int damage = Convert.ToInt16(m_dbTable.Rows[0]["damage"]);

        return damage;
    }
    /*===================================================================================================================
    * Get armor defense by id
    * 
    ===================================================================================================================*/
    public int getArmorDefense(int id)
    {
        string strSelect = "SELECT defense FROM armor WHERE id = " + id;

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);
        int defense = Convert.ToInt16(m_dbTable.Rows[0]["defense"]);
        return defense;
    }
    /*===================================================================================================================
    * Get price by id
    * 
    ===================================================================================================================*/
    public int getPrice(int id, string table)
    {
        string temp;
        string strSelect = "SELECT price FROM " + table + " WHERE id = " + id;

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

    public void UpdatePlayerInfo(string infoToSave, int id, string newValue)
    {
        string strQuery = "UPDATE save_player SET " + infoToSave + " = :infoToSave WHERE id = :ID";
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

    public List<PlayerEntryDB> getPlayerByName(string name)
    {
        List<PlayerEntryDB> playerList = new List<PlayerEntryDB>();

        int experience, gold;
        string weapon, armor;
        var position = new float[2];

        string strSelect = "SELECT experience, weapon, armor, positionx, positiony, gold FROM player_entry WHERE name = " + name;

        m_dbTable = new DataTable();
        m_dbTable = Select(strSelect);

        
        experience = Convert.ToInt32(m_dbTable.Rows[0]["experience"]);
        weapon = (m_dbTable.Rows[1]["weapon"]).ToString();
        armor = (m_dbTable.Rows[1]["armor"]).ToString();
        position[0] = Convert.ToInt32(m_dbTable.Rows[2]["positionx"]);
        position[1] = Convert.ToInt32(m_dbTable.Rows[3]["positiony"]);
        gold = Convert.ToInt32(m_dbTable.Rows[4]["gold"]);

        playerList.Add(new PlayerEntryDB(name, experience, weapon, armor, position, gold));
        
        return playerList;
    }

    public void SaveNewPlayer(string name, int experience, string weapon, string armor, float[] position, int gold)
    {
        NpgsqlDataAdapter dbAdapter = new NpgsqlDataAdapter();
        NpgsqlParameter dbParam = new NpgsqlParameter();
        dbConnection = new NpgsqlConnection(strConnection);

        string strQuery = "INSERT INTO save_player (name, experience, weapon, armor, positionx, positiony, gold) VALUES(@name, @experience, @weapon, @armor, @positionx, @positiony, @gold)";

        dbConnection.Open();

        dbCmd = new NpgsqlCommand(strQuery, dbConnection);

        dbCmd.Parameters.Add(new NpgsqlParameter("@name", NpgsqlDbType.Text));
        dbCmd.Parameters.Add(new NpgsqlParameter("@experience", NpgsqlDbType.Integer));
        dbCmd.Parameters.Add(new NpgsqlParameter("@weapon", NpgsqlDbType.Text));
        dbCmd.Parameters.Add(new NpgsqlParameter("@armor", NpgsqlDbType.Text));
        dbCmd.Parameters.Add(new NpgsqlParameter("@positionx", NpgsqlDbType.Integer));
        dbCmd.Parameters.Add(new NpgsqlParameter("@positiony", NpgsqlDbType.Integer));
        dbCmd.Parameters.Add(new NpgsqlParameter("@gold", NpgsqlDbType.Integer));


        dbCmd.Parameters[0].Value = name;
        dbCmd.Parameters[1].Value = experience;
        dbCmd.Parameters[2].Value = weapon;
        dbCmd.Parameters[3].Value = armor;
        dbCmd.Parameters[4].Value = position[0];
        dbCmd.Parameters[5].Value = position[1];
        dbCmd.Parameters[6].Value = gold;

        dbCmd.ExecuteNonQuery();
        Debug.Log("Player saved");
        dbConnection.Close();
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
