using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class Samurai_Script : MonoBehaviour
{

    public static Samurai_Script instance;

    AudioSource sound;
    public GameObject projectilePrefab;
    GameObject spawnedProjectile;
    SpearScript projectile;
    public PhysicsMaterial2D freezeMaterial;
    public PhysicsMaterial2D usualMaterial;
    public List<GameObject> passableInRange;
    public List<GameObject> fireInRange;
    public List<GameObject> earthInRange;
    public List<GameObject> voidInRange;
    public InteractiveScript interactWith;
    public Sprite dialogSprite;
    public GameObject characterMenu;                                                                    // To be moved to InterfaceController
    GameObject characterMenuInstance;                                                                   // To be moved to InterfaceController
    public float currentHealth;
    public float currentMana;
    public float fallMultiplier;
    public float lowFallMultiplier;
    public float maxHealth;
    public float maxMana;
    public float maxSpeed;
    public bool canClimb;
    public bool canControl;
    public bool canInteract;
    public bool characterScreenOn;
    public bool goingDown;
    public bool heavyWeaponSelected;
    public bool isInvulnerable;
    public int[] skillLevels;
    public int auraSelected;
    public int currentZone;
    public int heavyDamage;
    public int jumpsAvailable;
    public int lightDamage;
    public int maxJumpsAvailable;
    public AudioClip jumpSound;
    public AudioClip lightattackSound;
    public AudioClip heavyattackSound;

    Transform flash;
    float vectX;
    float move = 0;
    GameManagerScript gmngr;
    SkillScript skillEarth;
    SkillScript skillWater;
    SkillScript skillFire;
    SkillScript skillAir;
    SkillScript skillVoid;
    SuordScript blade;
    HealthScript healthDisplay;
    ManaScript manaDisplay;
    public GameObject feetsies;
    public Rigidbody2D rig;
    float heavyAttackRecoil;
    float heavyAttackTime;
    float lightAttackRecoil;
    float lightAttackTime;
    float iTime;
    bool alreadyAttacking;
    bool canJumpDown;
    bool directionChanged;
    bool facingRight;
    bool isClimbing;
    bool isGrounded;
    public bool isFalling;
    bool isJumping;
    bool startedClimbing;
    bool spearRecoil;
    float spearRecoilTime;
    public int currentExperience;
    int currentLevel;
    int projectileType;
    public int currentSkillPoints;
    int experienceToNextLevel;
    int jumpSpeed;
    int force;
    char lastKeyPressed;

    private void Awake()
    {
        instance = this;
        skillLevels = new int[5] { 3, 0, 0, 0, 0 };
    }

    void Start()
    {
        Debug.Log("Samurai Enabled");
        sound = gameObject.GetComponent<AudioSource>();
        sound.volume = 0.1f;
        auraSelected = 0;
        projectile = SpearScript.instance;
        lastKeyPressed = ' ';
        canControl = true;
        feetsies = transform.GetChild(3).gameObject;
        rig = gameObject.GetComponent<Rigidbody2D>();
        canClimb = false;
        canJumpDown = false;
        currentExperience = 0;
        currentLevel = 1;
        currentSkillPoints = 20;                                                        // <-----------------------Skill Points
        spearRecoil = false;
        spearRecoilTime = 1f;
        experienceToNextLevel = 10;
        facingRight = true;
        fallMultiplier = 5f;
        directionChanged = false;
        lowFallMultiplier = 7.5f;
        goingDown = false;
        maxSpeed = 10f;
        alreadyAttacking = false;
        startedClimbing = false;
        canInteract = false;
        characterScreenOn = false;
        heavyAttackRecoil = 0.25f;
        heavyAttackTime = 0.25f;
        isClimbing = false;
        lightAttackRecoil = 0.05f;
        lightAttackTime = 0.05f;
        heavyWeaponSelected = false;
        gmngr = GameManagerScript.instance;
        isInvulnerable = false;
        maxJumpsAvailable = 2;
        jumpsAvailable = 0;
        jumpSpeed = 700;
        force = 1000;
        lightDamage = 10;
        heavyDamage = 20;
        maxMana = 100;
        iTime = 2;
        currentMana = maxMana;
        maxHealth = 100;
        currentHealth = maxHealth;
        flash = transform.GetChild(4);
        vectX = flash.transform.GetComponent<SpriteRenderer>().size.x*flash.transform.localScale.x;
        blade = SuordScript.instance;
        manaDisplay = ManaScript.manaScript;
        healthDisplay = HealthScript.healthScript;
        skillEarth = EarthSkillScript.instance;
        skillWater = WaterSkillScript.instance;
        skillFire = FireSkillScript.instance;
        skillAir = AirSkillScript.instance;
        skillVoid = VoidSkillScript.instance;
    }

    private void OnDisable()
    {
        Debug.Log("Samurai Disabled");
    }

    private void FixedUpdate()
    {
        if (rig.velocity.x < maxSpeed)
        {
            rig.velocity = new Vector2(move * maxSpeed, rig.velocity.y);
        }

        if (isFalling)
        {
            if (rig.velocity.y < 0)
            {
                rig.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime * (fallMultiplier - 1);
            }
            else if (rig.velocity.y > 0 && !Input.GetKey(KeyCode.Space) && !goingDown)
            {
                rig.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime * (lowFallMultiplier - 1);
            }
        }
        else if (!isClimbing)
        {
            rig.velocity = new Vector2(rig.velocity.x, 0);
        }
        //Debug.Log(rig.velocity.y);
    }

    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(feetsies.transform.position, 0.1f, 1 << 8);

        if(!isGrounded){
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, 1 << 8);

            if (hit.collider != null)
            {
                isGrounded = true;
                rig.velocity = new Vector2(rig.velocity.x - (hit.normal.x * 0.9f), rig.velocity.y);
            }
        }

        canJumpDown = Physics2D.OverlapCircle(feetsies.transform.position, 0.1f, 1 << 10) && !isGrounded;

        if ((isGrounded || canJumpDown) && jumpsAvailable < maxJumpsAvailable)
        {
            jumpsAvailable = maxJumpsAvailable;
        }

        if (currentMana != maxMana)
        {
            StartCoroutine(RegainManaOverTime());
        }

        if (canControl)
        {
            if (!spearRecoil)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Debug.Log("Aiming...");
                    projectile.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    int randInt = UnityEngine.Random.Range(1, 11);
                    projectileType = Mathf.RoundToInt(Mathf.Floor(randInt / 10) * (auraSelected + 1));
                    Debug.Log("Projectile Type IS: " + randInt);
                    switch (projectileType)
                    {
                        case 1:
                            Debug.Log(auraSelected);
                            projectile.gameObject.GetComponent<SpriteRenderer>().sprite = projectile.eSpear;
                            break;

                        case 2:
                            Debug.Log(auraSelected);
                            projectile.gameObject.GetComponent<SpriteRenderer>().sprite = projectile.wSpear;
                            break;

                        case 3:
                            Debug.Log(auraSelected);
                            projectile.gameObject.GetComponent<SpriteRenderer>().sprite = projectile.fSpear;
                            break;

                        case 4:
                            Debug.Log(auraSelected);
                            projectile.gameObject.GetComponent<SpriteRenderer>().sprite = projectile.aSpear;
                            break;
                        case 5:
                            Debug.Log(auraSelected);
                            projectile.gameObject.GetComponent<SpriteRenderer>().sprite = projectile.vSpear;
                            break;
                        default:
                            Debug.Log(auraSelected);
                            projectile.gameObject.GetComponent<SpriteRenderer>().sprite = projectile.basicSpear;
                            break;
                    }
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                projectile.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                spawnedProjectile = Instantiate(projectilePrefab, projectile.transform.position, projectile.transform.rotation);
                spawnedProjectile.GetComponent<SpearProjectileScript>().type = projectileType;

            }
            
            if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && canClimb)
            {
                Climb(true);
                startedClimbing = true;
            }

            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && canClimb)
            {
                Climb(false);
                startedClimbing = true;
            }

            if (Input.GetKeyDown("space") && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
            {
                Jump();
            }
            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))) {
                if (Input.GetKeyDown(KeyCode.Space) && canJumpDown)
                {
                    Debug.Log(canJumpDown);
                    goingDown = true;
                    CollisionDirection();
                    rig.velocity = new Vector2(rig.velocity.x, -10);
                } else if (Input.GetKeyDown(KeyCode.Space) && !canJumpDown)
                {
                    Jump();
                } else if(!canClimb)
                {
                    goingDown = false;
                    CollisionDirection();
                }
            }

            if((Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)))
            {
                isClimbing = false;
                goingDown = false;
                CollisionDirection();
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                isClimbing = false;
                CollisionDirection();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                gmngr.currentStageProgress++;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && !gmngr.isGamePaused)
            {
                if (!alreadyAttacking)
                {
                    blade.blade_edge.enabled = true;
                    blade.visibility.color = new Color(1f * Convert.ToInt32(heavyWeaponSelected), 1f * Convert.ToInt32(!heavyWeaponSelected), 0f, 1f);
                    StartCoroutine(WeaponTime());
                }
            }

            if (Input.GetKeyDown(KeyCode.E) && canInteract)
            {
                interactWith.Interact();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                heavyWeaponSelected = !heavyWeaponSelected;
            }

            if (Input.GetKeyDown("1"))
            {
                skillEarth.Activate();
            }

            if (Input.GetKeyDown("2"))
            {
                skillWater.Activate();
            }

            if (Input.GetKeyDown("3"))
            {
                skillFire.Activate();
            }

            if (Input.GetKeyDown("4"))
            {
                skillAir.Activate();
            }

            if (Input.GetKeyDown("5"))
            {
                skillVoid.Activate();
            }

            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
            {
                lastKeyPressed = 'D';
            }

            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
            {

                lastKeyPressed = 'A';
            }

            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
            {
                if (!directionChanged)
                {
                    if (lastKeyPressed == 'A')
                    {
                        lastKeyPressed = 'D';
                    }
                    else if (lastKeyPressed == 'D')
                    {
                        lastKeyPressed = 'A';
                    }
                    directionChanged = true;
                }
            }

            if((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) || (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)))
            {
                directionChanged = false;
            }

            if(!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
            {
                lastKeyPressed = ' ';
            }

            if (lastKeyPressed=='A')
            {
                move = -1;
            } else if (lastKeyPressed == 'D')
            {
                move = 1;
            } else
            {
                move = 0;
            }
            

            
            if (move < 0 && facingRight == true) { Flip(true); }
            if (move > 0 && facingRight == false) { Flip(false); }

            if (move == 0)
            {
                if (!isClimbing && startedClimbing)
                {
                    rig.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
                else
                {
                    rig.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                }
            }
            else
            {
                if (!isClimbing && startedClimbing)
                {
                    rig.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
                else
                {
                    rig.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }

            if (!canClimb)
            {
                startedClimbing = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!characterScreenOn)
            {
                Time.timeScale = 1 * System.Convert.ToInt32(gmngr.isGamePaused);
                gmngr.isGamePaused = !gmngr.isGamePaused;
                canControl = false;
                characterMenuInstance = Instantiate(characterMenu, gmngr.transform);
                characterScreenOn = true;
            } else
            {
                Time.timeScale = 1 * System.Convert.ToInt32(gmngr.isGamePaused);
                gmngr.isGamePaused = !gmngr.isGamePaused;
                canControl = true;
                Destroy(characterMenuInstance);
                characterScreenOn = false;
            }
        }
    }


    public void AddEXP(int experienceToAdd)
    {
        currentExperience += experienceToAdd;
        if(currentExperience >= experienceToNextLevel){
            currentLevel++;
            currentSkillPoints++;
            experienceToNextLevel += 20;                                                                                                                                //do wymiany
        }
    }

    public void CollisionDirection()
    {
        foreach (GameObject platform in passableInRange)
        {
            if (platform.transform.GetComponent<Collider2D>().bounds.max.y > feetsies.gameObject.GetComponent<Collider2D>().bounds.min.y || goingDown)
            {
                Physics2D.IgnoreCollision(platform.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
                Physics2D.IgnoreCollision(platform.GetComponent<Collider2D>(), feetsies.gameObject.GetComponent<Collider2D>(), true);
            } else
            {
                Physics2D.IgnoreCollision(platform.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), false);
                Physics2D.IgnoreCollision(platform.GetComponent<Collider2D>(), feetsies.gameObject.GetComponent<Collider2D>(), false);
            }
        }
    }

    void Flip(bool i)
    {
        int flip = Convert.ToInt32(i);
        transform.GetComponent<SpriteRenderer>().flipX = i;
        projectile.GetComponent<SpriteRenderer>().flipX = i;                                                                                                                            //CHECK AFTER ROTATION IMPLEMENTED
        flash.GetComponent<SpriteRenderer>().flipX = i;
        flash.transform.localPosition = new Vector3(flash.transform.localPosition.x + vectX - (flip * 2 * vectX), flash.transform.localPosition.y, flash.transform.localPosition.z);
        facingRight = !i;
    }

    public void getDamaged(int damage, Collider2D instigator)
    {
        StartCoroutine(Invulnerability(instigator));
        bool isLeft = instigator.bounds.center.x > GetComponent<Collider2D>().bounds.center.x;
        StartCoroutine(Knockback(0.1f, 15f, transform.position, isLeft));
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Climb(bool up)
    {
        isClimbing = true;
        jumpsAvailable = maxJumpsAvailable;
        rig.gravityScale = 0;
        rig.drag = 100;

        isFalling = false;
        goingDown = true;
        rig.velocity = new Vector2(0, maxSpeed - 2*Convert.ToInt32(!up)*maxSpeed);
        CollisionDirection();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Jump()
    {
        startedClimbing = false;
        isFalling = true;
        isJumping = true;
        if (jumpsAvailable > 0)
        {
            sound.volume = 0.05f;
            sound.clip = jumpSound;
            sound.PlayOneShot(jumpSound);
            sound.volume = 0.1f;
            rig.gravityScale = 2;
            rig.drag = 2;
            rig.velocity = new Vector2(rig.velocity.x, 0);
            rig.AddForce(new Vector2(0, jumpSpeed));
            jumpsAvailable--;
        }
    }

    #region Enumeratory

    private IEnumerator Invulnerability(Collider2D instigator)
    {
        int i = 0;
        isInvulnerable = true;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), instigator, true);
        Physics2D.IgnoreCollision(feetsies.GetComponent<Collider2D>(), instigator, true);
        while (i < iTime * 60)
        {
                i++;
                yield return new WaitForSeconds(0);

        }
        isInvulnerable = false;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        if (instigator != null)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), instigator, false);
            Physics2D.IgnoreCollision(feetsies.GetComponent<Collider2D>(), instigator, false);
        }
    }

    private IEnumerator Knockback(float knockbackTime, float knockbackForce, Vector2 knockbackDirection, bool rightOrLeft)
    {
        float timer = 0;
        canControl = false;
        rig.sharedMaterial = freezeMaterial;
        rig.velocity = Vector2.zero;
        while (timer < knockbackTime)
        {
        timer += Time.deltaTime;
            rig.AddForce(new Vector2(knockbackDirection.x * knockbackForce * 0.08f * (1 - (2 * System.Convert.ToInt32(rightOrLeft))), knockbackDirection.y * 2 * knockbackForce));
            
        yield return knockbackTime;

        }
        rig.sharedMaterial = usualMaterial;
        while (!canControl)
        {
            if (isGrounded || canJumpDown)
            {
                canControl = true;
            }
            yield return 0;
        }
    }

    private IEnumerator RegainManaOverTime()
    {
        new WaitForSeconds(1);

        while (currentMana < maxMana)
        {
            if (!gmngr.isGamePaused)
            {
                currentMana += 0.01f;
            }
            yield return new WaitForSeconds(3);
        }
    }

    private IEnumerator WeaponTime()
    {

        int i = 0;
        if (heavyWeaponSelected)
        {
            sound.clip = heavyattackSound;
            sound.PlayOneShot(heavyattackSound);
            alreadyAttacking = true;
            while (i < heavyAttackTime * 60)
            {
                if (!gmngr.isGamePaused) { 
                    i++;
                    yield return new WaitForSeconds(0);
                } else {
                    yield return null;
                }
            }
            i = 0;
            blade.visibility.color = new Color(1f, 1f, 1f, 0f);
            blade.blade_edge.enabled = false;
            while (i < heavyAttackRecoil * 60)
            {
                if (!gmngr.isGamePaused)
                {
                    i++;
                    yield return new WaitForSeconds(0);
                }
                else
                {
                    yield return null;
                }
            }
        }
        else
        {
            sound.clip = lightattackSound;
            sound.PlayOneShot(lightattackSound);
            alreadyAttacking = true;
            while (i < lightAttackTime * 60)
            {
                if (!gmngr.isGamePaused)
                {
                    i++;
                    yield return new WaitForSeconds(0);
                }
                else
                {
                    yield return null;
                }
            }
            i = 0;
            blade.visibility.color = new Color(1f, 1f, 1f, 0f);
            blade.blade_edge.enabled = false;
            while (i < lightAttackRecoil * 60)
            {
                if (!gmngr.isGamePaused)
                {
                    i++;
                    yield return new WaitForSeconds(0);
                }
                else
                {
                    yield return null;
                }
            }
        }
        alreadyAttacking = false;
    }
    #endregion
}
