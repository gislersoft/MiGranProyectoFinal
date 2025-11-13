using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public Ammo ammo;
    void OnTriggerEnter(Collider OtherCollider)
    {
        if (OtherCollider.CompareTag("P1"))
        {
            ammo.ammoLightCount += 10;
        }
    }
}
