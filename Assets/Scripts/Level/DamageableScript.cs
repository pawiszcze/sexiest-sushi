using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableScript : MonoBehaviour
{
    public float hitPoints;
    public bool isBurning;

    virtual public void GetDamaged(float damage, Collider2D instigator)
    {

    }
}
