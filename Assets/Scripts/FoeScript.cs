using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeScript : MonoBehaviour {

    public List<GameObject> attachedProjectiles;
    public GameObject parent;
    public Sprite spent;
    public int expValue;
    public float maxSpeed;
    public float currentSpeed;
    protected float force;
    protected int damage;
    public int hitPoints;
    protected bool facingRight;
    protected bool isHit;
    public bool isBurning;
    public bool isGrounded;
    public bool isScaredShitless;
    protected float cooloffTime;
    protected RaycastHit2D bla;
    protected Rigidbody2D rig;
    protected Samurai_Script player;

    private void Start()
    {
        player = Samurai_Script.instance;
        
    }
    
    void Update () {
        Move();
	}

    protected void Flip(bool i)
    {
        transform.GetComponent<SpriteRenderer>().flipX = i;
        facingRight = !i;
        currentSpeed = -currentSpeed;
        maxSpeed = -maxSpeed;
    }

    virtual protected void Move()
    {

    }

    virtual public void GetDamaged(int damage, Collider2D instigator)
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
            Debug.Log("Player XP = " + player.currentExperience);
            Die();
        }
    }

    virtual protected void DealDamage(int damage)
    {
        if (!player.isInvulnerable)
        {
            player.getDamaged(damage, gameObject.GetComponent<Collider2D>());
        }
    }

    virtual protected void Die()
    {
        parent.GetComponent<SpawnerScript>().isBabyDead = true;
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
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), true);
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Samurai_Script>().feetsies.GetComponent<Collider2D>(), true);
            }
            else
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), false);
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Samurai_Script>().feetsies.GetComponent<Collider2D>(), false);
                DealDamage(damage);
            }

        } else if (collision.gameObject.GetComponent<FoeScript>())
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), true);
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
