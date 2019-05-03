using System.Collections.Generic;
using UnityEngine;

public class MonsterAi : MonoBehaviour
{
    public float speed = 1;
    public int distanceDetection;

    public Sprite spriteDefault;
    public Sprite spriteAttack;
    public Sprite spriteKill;

    float degfactor = 360 / (2 * Mathf.PI);
    float sqrt2 = 1 / Mathf.Sqrt(2);

    private float updateInterval;
    public float attackCooldown;
    private float updatetime = 0;
    private float attacktime = 0;
    private bool tmpbool;

    private float horizontalAxis;
    private float verticalAxis;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;

    List<GameObject> playerlist = new List<GameObject>();

    private Vector3 startPoint;
    public GameObject player;

    // Start is called before the first frame update

    void Start()
    {
        startPoint = GameObject.Find("Monster").transform.position;
        spriteRenderer = GameObject.Find("Monster").GetComponent<SpriteRenderer>();
        rb2d = GameObject.Find("Monster").GetComponent<Rigidbody2D>();
        bc2d = GameObject.Find("Monster").GetComponent<BoxCollider2D>();

        if (spriteRenderer == null)
            spriteRenderer.sprite = spriteDefault;
        rb2d = GetComponent<Rigidbody2D>();
        updateInterval = Random.Range(1.0f, 5.0f);
    }

    //To kill the monster
    public void kill()
    {
        Destroy(bc2d, 0);
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

    //===================================begin AI================================
    float BasicEstimination(Vector3 currentPosiiton, Vector3 targetPosition)
    {
        //pythagore
        return Vector3.Distance(currentPosiiton, targetPosition);
    }

    bool isRayCastAllObject(RaycastHit2D[] hits, string nameObject)
    {
        //i = 2 because 0,1 are the AI object for some reason dont juge
        for (int i = 1; i < hits.Length; i++)
        {
            if (hits[i].collider.name.Equals(nameObject))
            {
                //true if the object is in range of the AI
                return true;
            }
        }
        return false;
    }
    int RayCastAllIndexFinder(RaycastHit2D[] hits, string nameObject)
    {
        //i = 2 because 0,1 are the AI object for some reason dont juge
        for (int i = 1; i < hits.Length; i++)
        {
            if (hits[i].collider.name.Equals(nameObject))
            {
                return i;
            }
        }
        return 0;
    }


    int AroundAIFourDirection() //0(forward), 1(right), 3(left)
    {
        RaycastHit2D[] hitsRightFront;
        RaycastHit2D[] hitsLeftFront;
        RaycastHit2D[] hitsRight;
        RaycastHit2D[] hitsLeft;

        //Vector2 direction = Vector3.Normalize(transform.InverseTransformDirection(rb2d.velocity));
        //Vector2 direction = this.transform.localRotation.eulerAngles;
        //Vector2 direction = new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z), Mathf.Sin(transform.rotation.eulerAngles.z)); 
        //Vector2 direction = new Vector2(horizontalAxis, verticalAxis);

        Vector2 positionLeft = new Vector2(GameObject.Find("Monster").transform.position.x + 0.8f, GameObject.Find("Monster").transform.position.y);
        Vector2 positionRight = new Vector2(GameObject.Find("Monster").transform.position.x - 1.2f, GameObject.Find("Monster").transform.position.y);

        //make vector left and right relatif to the AI
        Vector2 left = new Vector2(-GameObject.Find("Monster").transform.up.y, GameObject.Find("Monster").transform.up.x);
        Vector2 right = new Vector2(GameObject.Find("Monster").transform.up.y, -GameObject.Find("Monster").transform.up.x);


        //draw ray  
        Debug.DrawRay(positionLeft, GameObject.Find("Monster").transform.up * 4, Color.red);
        Debug.DrawRay(positionRight, GameObject.Find("Monster").transform.up * 4, Color.blue);
        Debug.DrawRay(GameObject.Find("Monster").transform.position, left * 5, Color.green);
        Debug.DrawRay(GameObject.Find("Monster").transform.position, right * 5, Color.black);

        hitsLeftFront = Physics2D.RaycastAll(positionLeft, GameObject.Find("Monster").transform.up, 4);
        hitsRightFront = Physics2D.RaycastAll(positionRight, GameObject.Find("Monster").transform.up, 4);
        hitsLeft = Physics2D.RaycastAll(GameObject.Find("Monster").transform.position, left, 5);
        hitsRight = Physics2D.RaycastAll(GameObject.Find("Monster").transform.position, right, 5);

       

        Debug.Log("Front : " + isRayCastAllObject(hitsRightFront, "Collidable") + " : " + isRayCastAllObject(hitsLeft, "Collidable"));


        if (isRayCastAllObject(hitsLeftFront, "Collidable") && isRayCastAllObject(hitsRightFront, "Collidable"))
        {
            if (isRayCastAllObject(hitsRight, "Collidable") && isRayCastAllObject(hitsLeft, "Collidable"))
            {
                //case when both ray is touch
                if (hitsLeft[RayCastAllIndexFinder(hitsLeft, "Collidable")].distance > hitsRight[RayCastAllIndexFinder(hitsRight, "Collidable")].distance)
                {
                    return 1;
                }
                else
                {
                    return 3;
                }
            }
            else if (isRayCastAllObject(hitsRight, "Collidable") && !isRayCastAllObject(hitsLeft, "Collidable"))
            {
                return 1;
            }
            else
            {
                return 3;
            }
        }
        else if (isRayCastAllObject(hitsLeftFront, "Collidable") && !isRayCastAllObject(hitsRightFront, "Collidable"))
        {
            return 3;
        }
        else if (!isRayCastAllObject(hitsLeftFront, "Collidable") && isRayCastAllObject(hitsRightFront, "Collidable"))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private int ancienDirection = 0;

    void BasicAI()
    {
        MoveToPoint(ancienDirection, player.transform.position);
    }

    void MoveToStraightForward(int direction)
    {
        Vector2 movement = new Vector2(horizontalAxis, verticalAxis + 10);

        switch (direction)
        {
            case 0:
                //move forward
                movement = new Vector2(horizontalAxis, verticalAxis + 10);
                //do the action of rotation 
                GameObject.Find("Monster").transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1:
                //turn right 90
                movement = new Vector2(horizontalAxis - 10, verticalAxis);
                //do the action of rotation  
                GameObject.Find("Monster").transform.localRotation = Quaternion.Euler(0, 0, 270);
                break;
            case 2:
                //move backward
                movement = new Vector2(horizontalAxis, verticalAxis - 10);
                //do the action of rotation  
                GameObject.Find("Monster").transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;
            case 3:
                //turn left 90
                movement = new Vector2(horizontalAxis - 10, verticalAxis);
                //do the action of rotation   
                GameObject.Find("Monster").transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;
        }
        rb2d.AddForce(movement * speed * 0.5f);
    }
    //==================================================end AI============================

    void MoveToPoint(int direction, Vector2 target)
    {
        Vector2 movement = Vector2.MoveTowards(GameObject.Find("Monster").transform.position, target, 0.6f);

        switch (direction)
        {
            case 0:
                //move forward
                //do nothing
                break;
            case 1:
                //turn right 90
                movement = new Vector2(movement.y, -movement.x);
                break;
            case 2:
                //move backward
                movement = new Vector2(-movement.x, -movement.y);
                break;
            case 3:
                //turn left 90
                movement = new Vector2(-movement.y, movement.x);
                break;
        }
        //do the action of moving  
        GameObject.Find("Monster").transform.position = movement;
    }
    //==================================================end AI============================

    // Update is called once per frame
    void Update()
    {
        BasicAI();
        
        //Attack
        attacktime += Time.deltaTime;
        if(attacktime>=attackCooldown && attackCooldown!=-1 && Vector2.Distance(player.transform.position, this.transform.position) < 2 && playerlist.Count > 0)
        {
            spriteRenderer.sprite = spriteAttack;
            attacktime = 0;
            Debug.Log("fuck "+player.name);
            
            foreach (GameObject target in playerlist)
            {
                tmpbool = target.GetComponent<Player>().ReceiveDamage(GameObject.Find("Monster").GetComponent<MonsterCore>().CalculateDamage());
                if (tmpbool == true)
                {
                    playerlist.Remove(target);
                    target.GetComponent<Player>().kill();
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