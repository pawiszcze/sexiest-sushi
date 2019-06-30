using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearProjectileScript : MonoBehaviour {

    public GameObject staticSpear;
    public int type;

    Rigidbody2D rig;
    SamuraiScript player;
    FixedJoint2D connection;
    Collider2D thisCollider;
    public Sprite[] spears = new Sprite[6];

    void Start () {
        player = SamuraiScript.instance;
        rig = gameObject.GetComponent<Rigidbody2D>();
        thisCollider = gameObject.GetComponent<Collider2D>();
        connection = gameObject.GetComponent<FixedJoint2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = spears[type];
        rig.AddForce(transform.up * 10f, ForceMode2D.Impulse);
	}
	
	void Update () {
        var dir = rig.velocity;
        if (dir != Vector2.zero)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rig.constraints = RigidbodyConstraints2D.FreezeAll;

        if (collision.gameObject.tag == "Foe")
        {
            connection.enabled = true;
            connection.connectedBody = collision.rigidbody;
            rig.velocity = new Vector2(0,0);
            /*
             * K8's notes:
             * 
             * You twice using GetComponent to get a FoeScript instance out of collision.gameobject.
             * It would be better if you cache the FoeScript instance to a local variable and
             * perform following operation on that variable.
             * E.g.:
             * 
             *      var foeScript = collision.gameObject.GetComponent<FoeScript>();
             *      if (foeScript != null)
             *      {
             *          foeScript.attachedProjectiles.Add(this.gameObject);
             *          foeScript.GetDamaged(5, this.gameObject.GetComponent<Collider2D>());
             *      }
             * 
             */
            collision.gameObject.GetComponent<FoeScript>().attachedProjectiles.Add(this.gameObject);
            collision.gameObject.GetComponent<FoeScript>().GetDamaged(5, this.gameObject.GetComponent<Collider2D>());
            Destroy(GetComponent<Collider2D>());
            GameObject wlocznia = Instantiate(staticSpear, collision.transform);
            Destroy(gameObject, 1);
        } else if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Oneway")
        {
            Destroy(GetComponent<Collider2D>());
            Instantiate(staticSpear, this.transform.position, this.transform.rotation, collision.transform);
            Destroy(gameObject, 5);
            Destroy(this);
        }    
    }
}
