using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canDamage = true;
    private void OnTriggerEnter2D(Collider2D Other)
    {
       
        

        IDamageable hit = Other.GetComponent<IDamageable>();
       

        if (hit !=null)
        {
            if(_canDamage == true)
            {
               // Debug.Log("Hit ---> " + Other.name);
                hit.Damage();
                _canDamage = false;
                StartCoroutine(ResetDamage());
            }
              

        }
    }

    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(2.0f);
        _canDamage = true;


    }

}
