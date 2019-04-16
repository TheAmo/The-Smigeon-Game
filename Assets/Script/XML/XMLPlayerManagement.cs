using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLPlayerManagement : MonoBehaviour
{
    public static XMLPlayerManagement ins;

    void Awake()
    {
        ins=this;
        //LoadPlayer();
        //SavePlayer();

    }

    //List of Players
    public PlayerDatabase playerDB;

    //Save function
    public void SavePlayer()
    {
        //open a new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerDatabase));
        FileStream stream = new FileStream(Application.dataPath+"/StreamingFiles/XML/player_data.xml",FileMode.Create);

        serializer.Serialize(stream, playerDB);
        stream.Close();
    }

    //Load function
    public void LoadPlayer()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/player_data.xml", FileMode.Open);

        playerDB = serializer.Deserialize(stream) as PlayerDatabase;
        stream.Close();

        foreach (PlayerEntry player in playerDB.playerList)
        {
            Debug.Log("Loaded Player: " + player.name);
        }
    }
}


[System.Serializable]
public class PlayerEntry        //What will be populating the list
{
    public string name;
    public string className;
    public int experience;
    public float[] position;
    public int gold = 1000000000;
}

[System.Serializable]
public class PlayerDatabase
{
    //[XmlArray("SavedPlayer")]
    public List<PlayerEntry> playerList = new List<PlayerEntry>();
} 