using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstantiate : MonoBehaviour
{
    public Sprite BSsprite;
    /*
    private XMLPlayerManagement playerManagement;
    private XMLClassManagement classManagement;

    public List<PlayerEntry> players;
    public List<ClassEntry> classes;
    */
    void Start()
    {
        GameObject blackSmith = new GameObject();
        blackSmith.name = "Blacksmith";
        blackSmith.tag = "Blacksmith";
        blackSmith.AddComponent<BoxCollider2D>().size = new Vector2(3.208687f, 1.919655f);
        blackSmith.AddComponent<Rigidbody2D>().gravityScale = 0;
        blackSmith.GetComponent<Rigidbody2D>().angularDrag = 25000;
        blackSmith.GetComponent<Rigidbody2D>().drag = 25000;
        blackSmith.GetComponent<Rigidbody2D>().mass = 1000;
        Vector2 v2 = new Vector2(35, 63);
        blackSmith.transform.position = v2;
        Dialogue d = new Dialogue();
        blackSmith.AddComponent<SpriteRenderer>().sprite = BSsprite;
        blackSmith.GetComponent<SpriteRenderer>().sortingOrder = 2;
        string[] sentences = { "Welcome to my shop!", "Please do look around", "If you have any questions, seek out my son" };
        d.sentences = sentences;
        d.name = "Avencci-chan";
        blackSmith.AddComponent<DialogueTrigger>().dialogue = d;
            



        /*
        playerManagement.LoadPlayer();
        classManagement.LoadClasses();
        
        //Loading player
        foreach(PlayerEntry player in playerManagement.playerDB.playerList)
        {
            players.Add(player);
            Debug.Log("Loaded Player: "+player.name);
        }

        //Loading classes
        foreach (ClassEntry lilClass in classManagement.classDB.classList)
        {
            classes.Add(lilClass);
            Debug.Log("Loaded Player: " + lilClass.name);
        }
        */
    }
}
