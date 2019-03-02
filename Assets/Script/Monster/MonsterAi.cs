using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAi : MonoBehaviour
{

    public new const string path = "Monsters";
    public Monster monster;

    public int id = 0;

    public float speed=1;
    public int distanceDetection;

    public Sprite spriteDefault;
    public Sprite spriteAttack;
    public Sprite spriteKill;
    public Sprite spriteLootbag;
    float degfactor = 360 / (2 * Mathf.PI);
    float sqrt2 = 1 / Mathf.Sqrt(2);

    public float updateInterval;
    public float attackCooldown;
    private float updatetime=0;
    private float attacktime = 0;
    private bool tmpbool;

    private float horizontalAxis;
    private float verticalAxis;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    private SpriteRenderer SRLootbag;

    public int attackKnockback=50000;
    //public GameObject lootbag;
    List<GameObject> playerlist = new List<GameObject>();

    // Start is called before the first frame update

    void Start()
    {
        /*
        MonsterContainer mc = new MonsterContainer();
        monster = mc.Monsters[id];

        Debug.Log(monster.name + " Loaded!");
        */


        SRLootbag = GetComponent<SpriteRenderer>();
        SRLootbag.sprite = spriteLootbag;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();


        if (spriteRenderer == null)
            spriteRenderer.sprite = spriteDefault;
        rb2d = GetComponent<Rigidbody2D>();
        updateInterval = Random.Range(1.0f, 5.0f);
    }

    //To kill the monster
    public void kill()
    {
        GameObject lootbag = new GameObject();
        lootbag.name = "loot"+this.name;
        lootbag.AddComponent<SpriteRenderer>();
        lootbag.AddComponent<BoxCollider2D>().size = new Vector2(3, 3);
        lootbag.GetComponent<BoxCollider2D>().isTrigger = true;
        lootbag.tag = "Lootbag";
        lootbag.GetComponent<SpriteRenderer>().sprite = spriteLootbag;
        lootbag.GetComponent<SpriteRenderer>().sortingOrder = 2;
        lootbag.transform.position = this.transform.position;
        
        Destroy(bc2d,0);
        speed = 0;
        updateInterval = -1;
        attackCooldown = -1;
        distanceDetection = 0;
        spriteRenderer.sprite = spriteKill;
        spriteRenderer.sortingOrder = 1;
    }

    //Player in range
    void OnTriggerEnter2D(Collider2D range)
    {
        if (range.gameObject.tag == "Player")
        {
            playerlist.Add(range.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D range)
    {
        if (range.gameObject.tag == "Player")
        {
            playerlist.Remove(range.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {

        //Movement
        updatetime += Time.deltaTime;
        Transform player = GameObject.Find("Player").transform;
        if (Vector2.Distance(player.position, this.transform.position) > distanceDetection)
        {
            if (updatetime >= updateInterval && updateInterval != -1)
            {
                updatetime = 0;

                //check mur
                verticalAxis = Random.Range(-2.0f, 2.0f);
                horizontalAxis = Random.Range(-2, 2);


                //movement
                if (verticalAxis != 0 || horizontalAxis != 0)
                {

                    //random movements
                    int angle = (int)Mathf.Floor(degfactor * Mathf.Atan(-horizontalAxis / verticalAxis));
                    if (verticalAxis < 0)
                        angle += 180;
                    transform.localRotation = Quaternion.Euler(0, 0, angle);

                }
            }
            Vector2 movement = new Vector2(horizontalAxis, verticalAxis);
            rb2d.AddForce(movement * speed);
        }
        else
        {
            //turn around towards the target 
            int angle = (int)Mathf.Floor(degfactor * Mathf.Atan(-(transform.position.x- player.position.x ) / (transform.position.y- player.position.y)));
            if(player.position.y < transform.position.y)
            {
                angle = angle - 180;
            }

            //do the action of rotation
            transform.localRotation = Quaternion.Euler(0, 0, angle);

            // move sprite towards the target
            if (Vector2.Distance(player.position, this.transform.position) > 2)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, 0.09f);
            }
          
        }

        //Attack
        attacktime += Time.deltaTime;
        Vector2 knockbackDirection;
        if (attacktime>=attackCooldown && attackCooldown!=-1 && Vector2.Distance(player.position, this.transform.position) < 2 && playerlist.Count > 0)
        {
            spriteRenderer.sprite = spriteAttack;
            attacktime = 0;

            
            foreach (GameObject target in playerlist)
            {
                tmpbool = target.GetComponent<Player>().getAttack(monster.attackbonus);
                if (tmpbool == true)//Target is hit
                {
                    tmpbool = target.GetComponent<Player>().ReceiveDamage(monster.CalculateDamage());
                    if (tmpbool == true)//Target is dead
                    {
                        playerlist.Remove(target);
                        target.GetComponent<Player>().kill();
                    }
                    else
                    {
                        knockbackDirection = target.transform.position - this.transform.position;
                        target.GetComponent<Rigidbody2D>().AddForce(knockbackDirection.normalized * attackKnockback);
                    }
                }
                break;
            }
        }
        if (attacktime>(attackCooldown/2) && attackCooldown!=-1) //To put back the default sprite
        {
            spriteRenderer.sprite = spriteDefault;
        }
    }


}
