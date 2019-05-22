using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeScript : DamageableScript
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
    protected SamuraiScript player;
    protected SpriteRenderer sprite;
    protected Collider2D colliderS;
    protected SpawnerScript daddy;
    protected Collider2D thisCollider;
    protected Collider2D samuraiBody;
    protected Collider2D samuraiFeet;

    private void Start()
    {
        player = SamuraiScript.instance;
        daddy = parent.GetComponent<SpawnerScript>();
        colliderS = gameObject.GetComponent<Collider2D>();
        samuraiBody = player.gameObject.GetComponent<Collider2D>();
        samuraiFeet = player.gameObject.GetComponent<SamuraiScript>().feetsies.GetComponent<Collider2D>();
        Physics2D.IgnoreLayerCollision(1 << 9, 1 << 9);
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

    override public void GetDamaged(int damage, Collider2D instigator)
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
            player.GetDamaged(damage, colliderS);
        }
    }

    virtual protected void Die()
    {
        daddy.isBabyDead = true;
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
                Physics2D.IgnoreCollision(thisCollider, samuraiFeet, true);
            }
            else
            {
                Physics2D.IgnoreCollision(thisCollider, samuraiBody, false);
                Physics2D.IgnoreCollision(thisCollider, samuraiFeet, false);
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
