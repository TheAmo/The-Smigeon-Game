using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Stats
{
    public int damage;
    public float explosionTime;

    public Sprite spriteDefault;
    public Sprite spriteExplosion1;
    public Sprite spriteExplosion2;
    public Sprite spriteExplosion3;
    public Sprite spriteExplosion4;
    public Sprite spriteSmoke;



    private SpriteRenderer spriteRenderer;

    private bool hasExploded=false;
    private bool isDefused=false;
    private bool trapFinish = false;
    private bool hasDamaged=false;
    private float timeElapsed=0;

    List<GameObject> playerlist = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer.sprite = spriteDefault;
    }

    //Explode if player enter
    void OnTriggerEnter2D(Collider2D range)
    {
        if (range.gameObject.tag == "Player")
        {
            hasExploded = true;
            playerlist.Add(range.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D range)
    {
     
    }
    bool tmp;
    // Update is called once per frame
    void Update()
    {
        if (hasExploded == true && !trapFinish)
        {
            if (!hasDamaged)
            {
                foreach (GameObject player in playerlist)
                {
                    tmp = player.GetComponent<Stats>().getDamage(damage);
                    if (tmp == true)
                    {
                        playerlist.Remove(player);
                        player.GetComponent<Player>().kill();
                    }
                }
                hasDamaged = true;
            }
            
            Debug.Log("Trap Exploded!");
            spriteRenderer.sprite = spriteExplosion1;
            spriteRenderer.sortingOrder =10;
            timeElapsed += Time.deltaTime;
            if (timeElapsed > explosionTime/4)
            {
                spriteRenderer.sprite = spriteExplosion2;
                if (timeElapsed > explosionTime / 2)
                {
                    spriteRenderer.sprite = spriteExplosion3;
                    if (timeElapsed > 3*explosionTime/4)
                    {
                        spriteRenderer.sprite = spriteExplosion4;
                        if (timeElapsed >1.3*explosionTime)
                        {
                            spriteRenderer.sprite = spriteSmoke;
                            if (timeElapsed > 2 * explosionTime)
                            {
                                trapFinish = true;
                                spriteRenderer.sortingOrder = -1;
                            }
                        }                     
                    }   
                }             
            }
        }

    }
}
