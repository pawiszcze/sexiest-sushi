using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * K8's notes:
 * 
 * As far as I know, this class is never being directly instanced
 * so it could be marked as abstract.
 * 
 * Alternatively, you could turn it into an interface.
 * It has only public members, and its only method is marked with virtual to enable overriding it inside subclasses.
 * 
 * Although interfaces cannot have fields, they can have properties.
 * So you can change e.g.: 
 *  field:       float hitPoints
 *  into:        float HitPoints {get; set:}
 * 
 * 
 * Interfaces has this this plus that you can inherid multiple interfaces at once, while classes can inherit
 * after only one class. However, in this case you would need to supply MonoBehaviour inheritance in other way.
 * 
 * */
public class DamageableScript : MonoBehaviour
{
    public float hitPoints;
    public bool isBurning;

    virtual public void GetDamaged(float damage, Collider2D instigator)
    {

    }
}
