using System.Collections;
using UnityEngine;

using System.Collections.Generic;   //Let us use Lists
using System.Xml;                   //Basic xml attribute
using System.Xml.Serialization;     //Access xmlserializer
using System.IO;                    //file management

public class XMLClassManagement : MonoBehaviour
{
    public static XMLClassManagement ins;

    void Awake()
    {
        ins = this;
        LoadClasses();
        //SaveClasses();
    }

    //List of Classes
    public ClassDatabase classDB;

    //Save function
    public void SaveClasses()
    {
        //Open a new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(ClassDatabase));
        FileStream stream = new FileStream(Application.dataPath+"/StreamingFiles/XML/class_data.xml",FileMode.Create);

        serializer.Serialize(stream, classDB);
        stream.Close();
    }

    //Load function
    public void LoadClasses()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ClassDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/class_data.xml", FileMode.Open);

        classDB = serializer.Deserialize(stream) as ClassDatabase;
        stream.Close();

        foreach (ClassEntry lilClass in classDB.classList)
        {
            Debug.Log("Loaded Player: " + lilClass.name);
        }
    }
}

[System.Serializable]
public class ClassEntry         //What will be populating the list
{
    public string name;
    public int[] baseAbility;
    public int hitDice;
    public int[] bbaEachLevel;
    public int damageDice;
}

[System.Serializable]
public class ClassDatabase
{
    public List<ClassEntry> classList = new List<ClassEntry>();
}