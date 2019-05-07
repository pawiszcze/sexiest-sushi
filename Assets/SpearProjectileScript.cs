using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearProjectileScript : MonoBehaviour {

    public GameObject staticSpear;
    public int type;

    Rigidbody2D rig;
    Samurai_Script player;
    FixedJoint2D connection;
    public Sprite[] spears = new Sprite[6];

    void Start () {
        player = Samurai_Script.instance;
        rig = gameObject.GetComponent<Rigidbody2D>();
        connection = gameObject.GetComponent<FixedJoint2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = spears[type];
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.gameObject.GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.feetsies.gameObject.GetComponent<Collider2D>(), true);
        Physics2D.IgnoreLayerCollision(11, 11, true);
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
