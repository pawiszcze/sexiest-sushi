using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuordScript : MonoBehaviour {

    public static SuordScript instance;
    public Collider2D blade_edge;
    public SpriteRenderer visibility;

    /*
     * K8's notes:
     * Please, always add explicitly private access modifier if needed.
     * 
     * Of course private is the default access level in C#, but
     * if one day a person without knowledge of C# syntax will have to
     * do something in your code, he or she might have problems with
     * understanding what is going on and have to spend some time to
     * learn about some features of your scripts syntax
     */
    SamuraiScript player;
    Collider2D bodyCollider;

	// Use this for initialization
	void Awake () {
        instance = this;
        /*
         * K8's notes:
         * 
         * Always check if GetComponent() not returns null 
         * before performing any operations on this result object!
         * 
         * If there is a situation when you need to have some types
         * of components on your object, but this object doesn't have them,
         * you could add such component from code with AddComponent() method.
         * e.g:
         * 
         *  var myComponent = myGameObject.GetComponent<MyComponentType>();
         *  if (myComponent == null)
         *  {
         *       myComponent = myGameObject.AddComponent<MyComponentType>();
         *  }
         *  myComponent.DoSth();
         *  
         * 
         */
        visibility = GetComponent<SpriteRenderer>();
            visibility.color = new Color(1f, 1f, 1f, 0f);

        blade_edge = GetComponent<Collider2D>();
            blade_edge.enabled = false;

        bodyCollider = gameObject.GetComponent<Collider2D>();
	}

    private void Start()
    {
        player = SamuraiScript.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Foe")
        {
            /*
             * K8's notes:
             * 
             * Got you!
             * 
             * Again, such long lines with multiple switching between following objects' fields are dangerous.
             * You never know which part of such instruction will got a null reference that'll cause throwing an exception.
             * 
             * In this example, you should cache to local variable the result of method call: GetComponent<DamageableScript>(),
             * check if this result is != null and if so, finally call on this object GetDamaged method.
             * 
             * What is more, you can cache DamageableScript instance before this if_else block, because in both cases you need to
             * perform this operation. You'll get rid of redundant part of code.
             * 
             */
            if (player.heavyWeaponSelected) {
                collision.gameObject.GetComponent<DamageableScript>().GetDamaged(player.heavyDamage, bodyCollider);
            }
            else
            {
                collision.gameObject.GetComponent<DamageableScript>().GetDamaged(player.lightDamage, bodyCollider);
            }
        }
    }
}
