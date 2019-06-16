using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleDecoration : DamageableScript
{
    void Start()
    {
        hitPoints = 1;       
    }

    override public void GetDamaged(float damage, Collider2D instigator)
    {
        Destroy(this.gameObject);
    }
}
