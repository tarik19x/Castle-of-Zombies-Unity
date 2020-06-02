using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy, IDamageable
{
    public GameObject acidEffectPrefab;
    
    public int Health { get; set; }
    public bool direction = true;
    public override void Init()
    {
        base.Init();
        Health = base.health;

    }

    public override void Update()
    {
        if (transform.position == pointA.position)
        {
            direction = false;
            sprite.flipX = false;
            currtentTarget = pointB.position;
            anim.Play("Fly");

        }
        else if (transform.position == pointB.position)
        {
            direction = true;
            currtentTarget = pointA.position;
            sprite.flipX = true;
            anim.Play("Fly");

        }
        transform.position = Vector3.MoveTowards(transform.position, currtentTarget, speed * Time.deltaTime);
    }

    public void Damage()
    {
        if (isDead == true)
            return;
        Health--;
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamonPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
            Destroy(this.gameObject);
        }
    }

    public override void Movement()
    {


    }

    public void Attack()
    {
      


        if (direction == true)
            Instantiate(acidEffectPrefab, transform.position, Quaternion.AngleAxis(0, Vector3.up));

        else
            Instantiate(acidEffectPrefab, transform.position, Quaternion.AngleAxis(180, Vector3.up));
    }

}
