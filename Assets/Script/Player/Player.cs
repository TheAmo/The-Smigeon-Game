using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.UIElements;

public class Player : Stats
{
    public Sprite spriteDefault;
    public Sprite spriteAttack;
    public Sprite spriteInteraction;
    public Sprite spriteKill;
    public UnityEngine.UI.Slider sliderHealth;
    public int attackKnockback;

    private SpriteRenderer spriteRenderer;
    private bool tmpbool;
    private bool isDead;
    private BoxCollider2D bc2d;

    List<GameObject> enemy = new List<GameObject>();
    List<GameObject> interact = new List<GameObject>();

    //To kill the player
    public void kill()
    {
        Destroy(bc2d, 0);
        spriteRenderer.sprite = spriteKill;
        spriteRenderer.sortingOrder = 1;
        Destroy(this.GetComponent<MoveWASD>(), 0);
        Debug.Log("Player is Dead!!!");
    }

    // Start is called before the first frame update
    void Start()
    {
        sliderHealth = GameObject.Find("SliderHealth").GetComponent<UnityEngine.UI.Slider>();
        sliderHealth.maxValue = hp;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer.sprite = spriteDefault;
    }

    // Getting the list of ennemy within range
    void OnTriggerEnter2D(Collider2D range)
    {
        if (range.gameObject.tag == "Enemy")
        {
            enemy.Add(range.gameObject);
            //Debug.Log("Enemy in range");
        }
        else if (range.gameObject.tag == "Interactable")
        {
            interact.Add(range.gameObject);
            //Debug.Log("Enemy in range");
        }
        else if (range.gameObject.tag.Contains("Door"))
        {
            interact.Add(range.gameObject);
        }
        else if (range.gameObject.tag == ("Blacksmith"))
        {
            interact.Add(range.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D range)
    {
        if (range.gameObject.tag == "Enemy")
        {
            enemy.Remove(range.gameObject);
            //Debug.Log("Enemy out of range");
        }
        else if (range.gameObject.tag == "Interactable")
        {
            interact.Remove(range.gameObject);
            //Debug.Log("Enemy in range");
        }
        else if (range.gameObject.tag.Contains("Door"))
        {
            interact.Remove(range.gameObject);
        }
        else if (range.gameObject.tag == ("Blacksmith"))
        {
            interact.Remove(range.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            //Attack
            if (Input.GetKeyUp(KeyCode.LeftShift) && spriteRenderer.sprite != spriteKill)
            {
                spriteRenderer.sprite = spriteDefault;//Change the sprite to default one
            }
            sliderHealth.value = hp;
            if (Input.GetKeyDown(KeyCode.LeftShift) && spriteRenderer.sprite != spriteKill) //If key is pushed
            {
                spriteRenderer.sprite = spriteAttack;//Change sprite to attack animation
                foreach (GameObject target in enemy)
                {
                    tmpbool = target.GetComponent<Stats>().getDamage(this.dealDamage());
                    target.GetComponent<Rigidbody2D>().AddForce(transform.forward * attackKnockback);//knockback
                    if (tmpbool == true)
                    {
                        enemy.Remove(target);
                        target.GetComponent<MonsterAi>().kill();

                        // GameObject.FindGameObjectWithTag("SliderHealth").GetComponent<Slider>().SetValueWithoutNotify(50f);

                    }
                    break;
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && spriteRenderer.sprite != spriteKill)
            {
                spriteRenderer.sprite = spriteDefault;//Change the sprite to default one
            }
            //Interaction
            if (Input.GetKeyDown(KeyCode.F) && spriteRenderer.sprite != spriteKill) //If key is pushed
            {
                spriteRenderer.sprite = spriteInteraction;//Change sprite to attack animation
                Vector2 playerPosition = transform.position;
                foreach (GameObject target in interact)
                {
                    if (target.tag == "Door")
                    {
                        if (target.transform.eulerAngles.z == 0)
                        {

                            target.transform.Translate(0, 2.5f, 0);
                            target.transform.Rotate(0, 0, 90, Space.Self);
                        }
                        else
                        {
                            target.transform.Rotate(0, 0, -90, Space.Self);
                            target.transform.Translate(0, -2.5f, 0);
                        }

                    }
                    else if (target.tag == "Doorh")
                    {
                        if (target.transform.eulerAngles.z == 90)
                        {
                            target.transform.Rotate(0, 0, -90, Space.Self);
                            target.transform.Translate(-2.5f, 0, 0);
                        }
                        else
                        {
                            target.transform.Translate(2.5f, 0, 0);
                            target.transform.Rotate(0, 0, 90, Space.Self);
                        }
                    }
                    else if(target.tag == "Blacksmith")
                    {

                    }
                    /* Destroy(target, 0);
                     interact.Remove(target);*/
                    break;
                }
            }
            if (Input.GetKeyUp(KeyCode.F) && spriteRenderer.sprite != spriteKill)
            {
                spriteRenderer.sprite = spriteDefault;//Change the sprite to default one
            }
        }
    }
}
