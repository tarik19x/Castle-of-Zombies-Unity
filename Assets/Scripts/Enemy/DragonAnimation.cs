using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimation : MonoBehaviour
{
    private Dragon _dragon;

    private void Start()
    {
        _dragon = transform.parent.GetComponent<Dragon>();
    }

    public void Fire()
    {

        _dragon.Attack();
    }
}
