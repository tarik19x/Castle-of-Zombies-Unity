using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject diamonPrefab;

    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;  
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA,pointB;
    protected Vector3 currtentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected int cnt = 0;
    protected Vector3 playerPos;
    protected Player player;
    protected bool isDead = false;
    

    protected bool isHit = false;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
       
    }

    public void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat")==false )
        {
            return;
        }
        if(isDead == false)
               Movement();

    }
    public virtual void Movement()
    {
        if (transform.position == pointA.position)
        {
            cnt += 1;
            sprite.flipX = false;
            currtentTarget = pointB.position;
            if (cnt > 1)
                anim.Play("Idle");

        }
        else if (transform.position == pointB.position)
        {
            currtentTarget = pointA.position;
            sprite.flipX = true;
            anim.Play("Idle");

        }

       

        

        if (isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, currtentTarget, speed * Time.deltaTime);

            
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance>3.0f)
        {
           
            isHit = false;
            anim.SetBool("InCombat", false);

        }

        Vector3 direction = player.transform.localPosition - transform.localPosition;
        if (direction.x > 0 && this.anim.GetBool("InCombat") == true)
        {
            this.sprite.flipX = false;
        }
        else if (direction.x < 0 && this.anim.GetBool("InCombat") == true)
        {
            this.sprite.flipX = true;
        }

        if (currtentTarget.x > transform.localPosition.x && this.anim.GetBool("InCombat") == false)
        {
            sprite.flipX = false;

        }
        else if (currtentTarget.x < transform.localPosition.x && this.anim.GetBool("InCombat") == false)
        {
            sprite.flipX = true;

        }



    }









}
