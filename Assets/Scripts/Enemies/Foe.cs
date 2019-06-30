using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foe : Damageable
{
    public List<GameObject> attachedProjectiles;
    public GameObject parent;
    public Sprite spent;
    public int expValue;
    public float maxSpeed;
    public float currentSpeed;
    protected float force;
    protected int damage;
    protected bool facingRight;
    protected bool isHit;
    public bool isGrounded;
    public bool isScaredShitless;
    protected float cooloffTime;
    protected RaycastHit2D bla;
    protected Rigidbody2D rig;
    protected Samurai player;
    protected SpriteRenderer sprite;
    protected Spawner daddy;
    protected Collider2D thisCollider;
    protected Collider2D samuraiBody;

    /*
     * K8's notes:
     * 
     * It's my subjective opinion, but if you have a class that at some level inherits from MonoBehaviour,
     * you should avoid naming your methods with names that already exist inside MonoBehaviour.
     * It is to me very misleading if such method is the one that is automatically called from Unity or just
     * an ordinary C# method that I need to call manually.
     * 
     * If you need to perform set of operations at certain phase of game lifetime, you could create an extra
     * method in your base class that is called in specific moment. And this method would be overriden by all
     * subclasses.
     * E.g.:
     * 
     *      public class Base : MonoBehaviour
     *      {
     *          // default MonoBehaviour.Start method:
     *          private void Start()
     *          {
     *              this.OnStart();
     *          }
     *          
     *          // custom method that is called on MonoBehaviour.Start
     *          protected virtual void OnStart()
     *          {
     *              // do stuff ...
     *          }
     *      }
     *      
     *      
     *      public class SubClass : Base
     *      {
     *          protected override void OnStart()
     *          {
     *              // perform base OnStart logic; you can skip such call:
     *              base.OnStart();
     *              
     *              // do custom SubClass stuff ...
     *          }
     *      }
    */
    protected virtual void Start()
    {
        player = Samurai.instance;
        if (parent != null)
        {
            daddy = parent.GetComponent<Spawner>();
        }
        thisCollider = gameObject.GetComponent<Collider2D>();
        samuraiBody = player.gameObject.GetComponent<Collider2D>();
    }
    
    void FixedUpdate () {
        Move();
    }

    protected void Flip(bool i)
    {
        sprite.flipX = i;
        facingRight = !i;
        currentSpeed = -currentSpeed;
        maxSpeed = -maxSpeed;
    }

    virtual protected void Move()
    {

    }

    override public void GetDamaged(float damage, Collider2D instigator)
    {
        bool isLeft = false;
        if (instigator != null)
        {
            isLeft = instigator.gameObject.transform.position.x > transform.position.x;
        }
        currentSpeed = 0;
        rig.velocity = Vector2.zero;
        isHit = true;
        StartCoroutine(Knockback(1f, 0.9f, transform.position, isLeft));
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            player.AddEXP(expValue);
            Die();
        }
    }

    virtual protected void DealDamage(int damage)
    {
        if (!player.isInvulnerable)
        {
            player.GetDamaged(damage, thisCollider);
        }
    }

    virtual protected void Die()
    {
        if (daddy)
        {
            daddy.lostABaby = true;
            daddy.isBabyDead = true;
        }
        player.fireInRange.Remove(gameObject);
        player.voidInRange.Remove(gameObject);
        player.earthInRange.Remove(gameObject);
        while (attachedProjectiles.Count > 0)
        {
            Destroy(attachedProjectiles[0]);
            attachedProjectiles.RemoveAt(0);
        }
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 10)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player.isInvulnerable)
            {
                Physics2D.IgnoreCollision(thisCollider, samuraiBody, true);
            }
            else
            {
                Physics2D.IgnoreCollision(thisCollider, samuraiBody, false);
                DealDamage(damage);
            }
        }
    }

    protected IEnumerator Knockback(float knockbackTime, float knockbackForce, Vector2 knockbackDirection, bool rightOrLeft)
    {
        float timer = 0;
        timer += 0.01f;

        rig.AddForce(new Vector2(7f * (1 - (2 * System.Convert.ToInt32(rightOrLeft))), knockbackDirection.y * knockbackForce), ForceMode2D.Impulse);

        while (timer < knockbackTime * 60)
        {
            timer++;
            yield return new WaitForSeconds(0);
        }

        isHit = false;
        currentSpeed = maxSpeed;

        yield return knockbackTime;
    }
}
