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
        GameObject currentGameObject = GameObject.Find("Monster");
        RaycastHit2D[] hitsRightFront;
        RaycastHit2D[] hitsLeftFront;
        RaycastHit2D[] hitsRight;
        RaycastHit2D[] hitsLeft;

        //Vector2 direction = Vector3.Normalize(transform.InverseTransformDirection(rb2d.velocity));
        //Vector2 direction = this.transform.localRotation.eulerAngles;
        //Vector2 direction = new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z), Mathf.Sin(transform.rotation.eulerAngles.z)); 
        //Vector2 direction = new Vector2(horizontalAxis, verticalAxis);

        Vector2 positionLeft = new Vector2(currentGameObject.transform.position.x + 0.8f, currentGameObject.transform.position.y);
        Vector2 positionRight = new Vector2(currentGameObject.transform.position.x - 1.2f, currentGameObject.transform.position.y);

        //make vector left and right relatif to the AI
        Vector2 left = new Vector2(-currentGameObject.transform.up.y, currentGameObject.transform.up.x);
        Vector2 right = new Vector2(currentGameObject.transform.up.y, -currentGameObject.transform.up.x);


        //draw ray  
        /*
        Debug.DrawRay(positionLeft, currentGameObject.transform.up * 4, Color.red);
        Debug.DrawRay(positionRight, currentGameObject.transform.up * 4, Color.blue);
        Debug.DrawRay(currentGameObject.transform.position, left * 5, Color.green);
        Debug.DrawRay(currentGameObject.transform.position, right * 5, Color.black);*/

        hitsLeftFront = Physics2D.RaycastAll(positionLeft, currentGameObject.transform.up, 4);
        hitsRightFront = Physics2D.RaycastAll(positionRight, currentGameObject.transform.up, 4);
        hitsLeft = Physics2D.RaycastAll(currentGameObject.transform.position, left, 5);
        hitsRight = Physics2D.RaycastAll(currentGameObject.transform.position, right, 5);



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
    int RayCastIndexFinder(RaycastHit2D[] hits, string nameObject)
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
    bool isRayCastObject(RaycastHit2D[] hits, string nameObject)
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


    //function to move the AI toward a specific point
    //while going around obstacle
    void TargetMovement(GameObject currentGameObject, Vector2 target)
    {
        RaycastHit2D[] hitsRightFront;
        RaycastHit2D[] hitsLeftFront;
        RaycastHit2D[] hitsRight;
        RaycastHit2D[] hitsLeft;
        Vector2 positionLeft;
        Vector2 positionRight;
        Vector2 positionForward;
        float constant45angle = 1 / Mathf.Sqrt(2);

        //transforme angle from a degre to a radian 
        //and put 0 degrees a the right like a normale trigometric cercle
        float angle = -((currentGameObject.transform.rotation.eulerAngles.z - 90) * Mathf.Deg2Rad);

        //start point in the left and right point
        positionLeft = new Vector2(currentGameObject.transform.position.x + Mathf.Sin(angle), currentGameObject.transform.position.y + Mathf.Cos(angle));
        positionRight = new Vector2(currentGameObject.transform.position.x - Mathf.Sin(angle), currentGameObject.transform.position.y - Mathf.Cos(angle));

        //created the vector forward with the position left and right
        positionForward = positionLeft - positionRight;
        positionForward = new Vector2(-positionForward.y, positionForward.x);

        //make vector left and right relatif to the AI 
        Vector2 right = new Vector2((-positionForward.y + positionForward.x) / 2, (positionForward.x + positionForward.y) / 2);
        Vector2 left = new Vector2((positionForward.y + positionForward.x) / 2, (-positionForward.x + positionForward.y) / 2);

        //make the vector lenght of 1
        left = left.normalized;
        right = right.normalized;

        //draw ray (temp) useful for debug
        Debug.DrawRay(positionLeft, positionForward * 2, Color.red);
        Debug.DrawRay(positionRight, positionForward * 2, Color.blue);
        Debug.DrawRay(positionLeft, left * 3, Color.green);
        Debug.DrawRay(positionRight, right * 3, Color.black);

        //make the raycast
        hitsLeftFront = Physics2D.RaycastAll(positionLeft, positionForward, 4);
        hitsRightFront = Physics2D.RaycastAll(positionRight, positionForward, 4);
        hitsLeft = Physics2D.RaycastAll(positionLeft, left, 6);
        hitsRight = Physics2D.RaycastAll(positionRight, right, 6);
        Debug.DrawRay(currentGameObject.transform.position, target * 10, Color.yellow);

        //=====================================movement=====================================

        //null vector ready to be manipulated
        Vector2 movement = new Vector2(horizontalAxis, verticalAxis);

        //angle is calculate with (arctan / 2) * angle of shift
        //so to simplifie I put the /2 and angle shift in one constant
        float angleTurn = Mathf.Atan(-Mathf.Abs(hitsRightFront[RayCastIndexFinder(hitsRightFront, "Collidable")].distance
                - hitsLeftFront[RayCastIndexFinder(hitsLeftFront, "Collidable")].distance) * Mathf.Deg2Rad * 22.5f);
        float diff = Mathf.Abs(hitsRightFront[RayCastIndexFinder(hitsRightFront, "Collidable")].distance
                - hitsLeftFront[RayCastIndexFinder(hitsLeftFront, "Collidable")].distance);

        if (isRayCastObject(hitsLeftFront, "Collidable") && isRayCastObject(hitsRightFront, "Collidable"))
        {
            if (hitsRightFront[RayCastIndexFinder(hitsRightFront, "Collidable")].distance == hitsLeftFront[RayCastIndexFinder(hitsLeftFront, "Collidable")].distance)
            {
                //case where the player is at the right of the AI
                if (this.transform.position.x < target.x)
                {
                    //go right
                    transform.localRotation = Quaternion.Euler(0, 0, angle + 2);
                }
                //case where the player is at the left of the AI
                else
                {
                    //go left
                    transform.localRotation = Quaternion.Euler(0, 0, angle - 2);
                }
            }
            else
            {
                //calculation for the rate of speed
                //when both front ray isn't touch at the same distance
                transform.localRotation = Quaternion.Euler(0, 0, angle - angleTurn);

            }
            movement = new Vector2(2, diff);
        }
        //edge cases
        else if (isRayCastObject(hitsLeftFront, "Collidable"))
        {
            //go right 
            movement = right;
        }
        else if (isRayCastObject(hitsRightFront, "Collidable"))
        {
            //go left 
            movement = left;
        }
        else
        {
            //just go toward the target 
            movement = target;

            //turn around towards the target 
            int angleRotation = (int)Mathf.Floor(degfactor * Mathf.Atan(-(currentGameObject.transform.position.x - target.x) /
                (currentGameObject.transform.position.y - target.y)));
            if (target.y < currentGameObject.transform.position.y)
            {
                angleRotation = angleRotation - 180;
            }
            transform.localRotation = Quaternion.Euler(0, 0, angleRotation);
        }
        Debug.Log("movement : " + movement);
        Debug.Log("targer   : " + target);
        Debug.Log("position : " + currentGameObject.transform.position);

        //push the AI in the direction of the vector movement
        //rb2d.AddForce(movement * speed * 2f); 
        currentGameObject.transform.position = Vector2.MoveTowards(currentGameObject.transform.position, target, 0.1f); ;
    }

    void BasicAI(GameObject currentGameObject, GameObject player)
    {
        TargetMovement(currentGameObject, (player.transform.position));
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
        GameObject player = GameObject.Find("Player(Clone)");
        GameObject monstre = GameObject.Find("Monster");

        //Attack
        attacktime += Time.deltaTime;
        //if the player is in range attack 
        if (Vector2.Distance(player.transform.position, monstre.transform.position) < 3)
        {
            if (attacktime >= attackCooldown && attackCooldown != -1 && playerlist.Count > 0)
            {
                spriteRenderer.sprite = spriteAttack;
                attacktime = 0;

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
            if (attacktime > (attackCooldown / 2) && attackCooldown != -1) //To put back the default sprite
            {
                spriteRenderer.sprite = spriteDefault;
            }
        }
        //else move toward the player
        else if (Vector2.Distance(player.transform.position, monstre.transform.position) < 100)
        {
            BasicAI(monstre, player);
        }
    }
}