using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy,IDamageable
{
   

    public int Health { get; set; }

   

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();
       
    }
    

    public void Damage()
    {
        if (isDead == true)
            return;

        Health--;
        base.anim.SetTrigger("Hit");
        base.isHit = true;
        base.anim.SetBool("InCombat", true);


        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamonPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
            Destroy(this.gameObject, 5.0f);

           // Destroy(this.gameObject);
        }



    }
   
}
