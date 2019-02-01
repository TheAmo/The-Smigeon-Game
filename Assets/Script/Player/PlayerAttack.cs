using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.UIElements;

public class PlayerAttack : Stats
{
    public Sprite spriteDefault;
    public Sprite spriteAttack;
    public Sprite spriteKill;
    public UnityEngine.UI.Slider sliderHealth;
    public int attackKnockback;

    private SpriteRenderer spriteRenderer;
    private bool tmpbool;
    private BoxCollider2D bc2d;

    List<GameObject> enemy = new List<GameObject>();

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
    }
    void OnTriggerExit2D(Collider2D range)
    {
        if (range.gameObject.tag == "Enemy")
        {
            enemy.Remove(range.gameObject);
            //Debug.Log("Enemy out of range");
        }
    }

    // Update is called once per frame
    void Update()
    {
        sliderHealth.value = hp;
        if (Input.GetKeyDown(KeyCode.LeftShift) && spriteRenderer.sprite != spriteKill) //If key is pushed
        {
            spriteRenderer.sprite = spriteAttack;//Change sprite to attack animation
            foreach (GameObject target in enemy)
            {
                tmpbool=target.GetComponent<Stats>().getDamage(this.dealDamage());
                target.GetComponent<Rigidbody2D>().AddForce(transform.forward * attackKnockback);//knockback
                if (tmpbool==true)
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
    }
}
