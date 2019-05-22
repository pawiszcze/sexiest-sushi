using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableScript : MonoBehaviour
{
    public int hitPoints;
    public bool isBurning;

    virtual public void GetDamaged(int damage, Collider2D instigator)
    {

    }
}
