using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class Samurai : Damageable
{

    public static Samurai instance;

    private Crouching crouching;
    private SpriteRenderer bodySprite;


    Checkpoint activeCheckpoint;
    Transform startMarker;
    Transform endMarker; 
    public Transform bodyTransform; 
    float reviveSpeed = 1.0f;
    float startTime;
    float reviveDistance;





    private int stillInWater = 0;
    public bool touchingLava = false;
    Coroutine breathMeter;
    public Coroutine burningBoots;
    public Coroutine repairBoots;

    AudioSource sound;
    public GameObject projectilePrefab;
    GameObject spawnedProjectile;
    Spear projectile;
    public bool touchingWater;
    public PhysicsMaterial2D freezeMaterial;
    public PhysicsMaterial2D slideMaterial;
    public PhysicsMaterial2D usualMaterial;
    public List<GameObject> passableInRange;
    public List<GameObject> fireInRange;
    public List<GameObject> earthInRange;
    public List<GameObject> voidInRange;
    public Interactive interactWith;
    public Sprite dialogSprite;
    public GameObject characterMenu;                                                                    // To be moved to InterfaceController
    GameObject characterMenuInstance;                                                                   // To be moved to InterfaceController
    public float currentHealth;
    public float currentMana;

    bool canCrouch;
    bool canUncrouch;
    bool isCrouched;
    float crouchedSpeed = 5f;

    public float fallMultiplier;
    public float lowFallMultiplier;
    public float maxHealth;
    public float maxMana;
    public bool isClimbing;
    public bool canClimb;

    public bool canInteract;
    public bool characterScreenOn;
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
    bool isReviving = false;

    Transform flash;

    bool canSwim;

    GameManager gmngr;
    
    SpriteRenderer flashSprite;
    Skill skillEarth;
    Skill skillWater;
    Skill skillFire;
    Skill skillAir;
    Skill skillVoid;
    Suord blade;
    Health healthDisplay;
    float fracJourney;

    public Coroutine BurningThingie;
    Mana manaDisplay;
    public Collider2D bodyCollider;
    public Rigidbody2D rig;
    float heavyAttackRecoil;
    float heavyAttackTime;
    float lightAttackRecoil;
    float lightAttackTime;
    float iTime;
    bool alreadyAttacking;
    bool canJumpDown;

    public bool goingDown;
    public bool canControl;
    bool isGrounded;
    public bool isUnderwater;
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
    Vector2 leftOffset;
    Vector2 rightOffset;

    SpriteRenderer spearSprite;
    public float breathBase;
    public float breathCurrent;
    public float bootsBase;

    public float bootsCurrent;
    public bool lavaVulnerable;



    private void Awake()
    {
        instance = this;
        skillLevels = new int[5] { 1, 0, 0, 0, 0 };
    }

    /*
     * K8's notes:
     * 
     * Sorry, but I have to say that:
     * 
     * This methods looks horrible!... (o.o)
     * It has 71 lines of initializing instructions! (>.<)
     * And that means that you have at least 71 variables in this class! (x.x)
     * EDIT: I roughly counted about  1 1 3  variables ;p
     * 
     * I think that it is time for You to learn SOLID principles ;o
     */
    void Start()
    {
        crouching = Crouching.instance;

        bodySprite = gameObject.GetComponent<SpriteRenderer>();
        canCrouch = true;
        isCrouched = false;
        lavaVulnerable = false;
        canSwim = false;
        touchingWater = false; goingDown = false;
        Time.timeScale = 1;
        breathBase = 6f;
        breathCurrent = breathBase;
        bootsBase = 1f;
        bootsCurrent = bootsBase;
        sound = gameObject.GetComponent<AudioSource>();
        leftOffset = new Vector2(-bodySprite.size.x / 2, 0);
        rightOffset = -leftOffset;
        sound.volume = 0.1f;
        auraSelected = 0;
        projectile = Spear.instance;
        spearSprite = projectile.gameObject.GetComponent<SpriteRenderer>();
        //fallMultiplier = 5f;
        //lowFallMultiplier = 7.5f;
        bodyCollider = gameObject.GetComponent<Collider2D>();
        rig = gameObject.GetComponent<Rigidbody2D>();
        canClimb = false;
        canJumpDown = false;
        currentExperience = 0;
        currentLevel = 1;
        currentSkillPoints = 20;                                                        // <-----------------------Skill Points
        spearRecoil = false;
        spearRecoilTime = 1f;
        experienceToNextLevel = 10;
        
        alreadyAttacking = false;
        bodyTransform = this.transform;
        startedClimbing = false;
        canInteract = false;
        characterScreenOn = false;
        heavyAttackRecoil = 0.25f;
        heavyAttackTime = 0.25f;
        
        lightAttackRecoil = 0.05f;
        lightAttackTime = 0.05f;
        heavyWeaponSelected = false;
        gmngr = GameManager.instance;
        isInvulnerable = false;
        maxJumpsAvailable = 1;
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
        flashSprite = flash.transform.GetComponent<SpriteRenderer>();

        blade = Suord.instance;
        manaDisplay = Mana.manaScript;
        healthDisplay = Health.healthScript;
        skillEarth = EarthSkill.instance;
        skillWater = WaterSkill.instance;
        skillFire = FireSkill.instance;
        skillAir = AirSkill.instance;
        skillVoid = VoidSkill.instance;
    }

    private void FixedUpdate()
    {
        /*if (isFalling)
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
}*/
    }



    void Update()
    {

        if (!Input.GetKey(KeyCode.DownArrow) && canUncrouch)
        {
            //Uncrouch();
        }

        if (gmngr.currentStageProgress == 0 && !touchingWater)
        {
            jumpSpeed = 400;
        }
        else if (!touchingWater)
        {
            jumpSpeed = 700;
        }

        if (gmngr.currentStageProgress > 1)
        {
            maxJumpsAvailable = 2;
        }
        else
        {
            maxJumpsAvailable = 1;
        }

        if (gmngr.currentStageProgress > 2)
        {
            canSwim = true;
        }

        if (gmngr.currentStageProgress > 3)
        {

        }


        LayerMask groundWater = 1 << 8 | 1 << 4;
        LayerMask layers = 1 << 8 | 1 << 10;



        isGrounded = Physics2D.OverlapBox((Vector2)transform.position, new Vector2(bodySprite.size.x - 0.1f, 0.02f), 0, groundWater);
        canJumpDown = Physics2D.OverlapBox((Vector2)transform.position, new Vector2(bodySprite.size.x - 0.1f, 0.02f), 0, 1 << 10) && !isGrounded;

        bool sideTouch = (Physics2D.OverlapBox((Vector2)transform.position + leftOffset, new Vector2(0.1f, bodySprite.size.y - 0.2f), 0, layers) || Physics2D.OverlapBox((Vector2)transform.position + rightOffset, new Vector2(0.1f, bodySprite.size.y - 0.2f), 0, layers) && !isGrounded && !canJumpDown);

        if (sideTouch && !isUnderwater)
        {
            rig.sharedMaterial = slideMaterial;
            rig.drag = 0;
            isFalling = true;
        }
        if (sideTouch && isUnderwater)
        {
            isFalling = true;
        }

        if (!sideTouch && rig.sharedMaterial == slideMaterial && !isUnderwater)
        {
            rig.sharedMaterial = usualMaterial;
            rig.drag = 2;
        }

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
                    spearSprite.color = new Color(1, 1, 1, 1);
                    int randInt = UnityEngine.Random.Range(1, 11);
                    projectileType = Mathf.RoundToInt(Mathf.Floor(randInt / 10) * (auraSelected + 1));
                    switch (projectileType)
                    {
                        case 1:
                            spearSprite.sprite = projectile.eSpear;
                            break;
                        case 2:
                            spearSprite.sprite = projectile.wSpear;
                            break;
                        case 3:
                            spearSprite.sprite = projectile.fSpear;
                            break;
                        case 4:
                            spearSprite.sprite = projectile.aSpear;
                            break;
                        case 5:
                            spearSprite.sprite = projectile.vSpear;
                            break;
                        default:
                            spearSprite.sprite = projectile.basicSpear;
                            break;
                    }
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                spearSprite.color = new Color(1, 1, 1, 0);
                spawnedProjectile = Instantiate(projectilePrefab, projectile.transform.position, projectile.transform.rotation);
                spawnedProjectile.GetComponent<SpearProjectile>().type = projectileType;
            }

            /*if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && canClimb)
            {
                Climb(true);
                startedClimbing = true;
            }

            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && canClimb)
            {

                Climb(false);
                startedClimbing = true;
            }*/

            if (Input.GetKeyDown("space") && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
            {
                Jump();
            }

            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
            {

                //Crouch();

                if (canSwim)
                {
                    if (touchingWater && !isUnderwater)
                    {
                        if (stillInWater == 0)
                        {
                            if (gmngr.currentStageProgress < 4)
                            {
                                breathMeter = StartCoroutine(Breathage());
                            }
                            rig.drag = 18f;
                            jumpSpeed = 1600;
                            rig.sharedMaterial = slideMaterial;
                            isFalling = true;
                        }
                        isUnderwater = true;
                        stillInWater++;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Space) && canJumpDown)
                {
                    goingDown = true;
                    CollisionDirection();
                    rig.velocity = new Vector2(rig.velocity.x, -10);

                }
                else if (Input.GetKeyDown(KeyCode.Space) && !canJumpDown) { }
                else if (!canClimb)
                {
                    goingDown = false;
                    CollisionDirection();
                }
                else if (isUnderwater)
                {
                    rig.drag = 18f;
                }

                if (isUnderwater)
                {
                    rig.drag = 10f;
                }
            }

            if ((Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)))
            {
                isClimbing = false;
                goingDown = false;
                CollisionDirection();

                //Uncrouch();

                if (isUnderwater)
                {
                    rig.drag = 18f;
                }
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

            if (isClimbing || isUnderwater || isJumping)
            {
                canCrouch = false;
            }
            else
            {
                canCrouch = true;
            }


            if ((isGrounded || canJumpDown) && !isUnderwater)
            {
                canCrouch = true;
            }

            if (!canCrouch)
            {
                //Uncrouch();
            }

            /*if (move == 0)
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
            }*/

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
            }
            else
            {
                Time.timeScale = 1 * System.Convert.ToInt32(gmngr.isGamePaused);
                gmngr.isGamePaused = !gmngr.isGamePaused;
                canControl = true;
                Destroy(characterMenuInstance);
                characterScreenOn = false;
            }
        }

        if (isReviving)
        {
            if (fracJourney < 1)
            {
                reviveSpeed = reviveDistance / iTime;
                float distCovered = (Time.time - startTime) * reviveSpeed * 1.5f;
                fracJourney = distCovered / reviveDistance;
                transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
            }
            else
            {
                currentHealth = maxHealth;
                canControl = true;
                isReviving = false;
            }
        }
    }

    public void AddEXP(int experienceToAdd)
    {
        currentExperience += experienceToAdd;
        if (currentExperience >= experienceToNextLevel)
        {
            currentLevel++;
            currentSkillPoints++;
            experienceToNextLevel += 20;                                                                                                                                //do wymiany
        }
    }

    public void CollisionDirection()
    {
        foreach (GameObject platform in passableInRange)
        {

            Collider2D platformCollider = platform.transform.GetComponent<Collider2D>();

            if (platformCollider.bounds.max.y > bodyCollider.bounds.min.y || goingDown)
            {
               // Debug.Log("Beep");
                Physics2D.IgnoreCollision(platformCollider, bodyCollider, true);
            }
            else
            {
                Physics2D.IgnoreCollision(platformCollider, bodyCollider, false);
               // Debug.Log("Boop");
            }
        }
    }



    override public void GetDamaged(float damage, Collider2D instigator)
    {
        if (instigator != null && !isInvulnerable)
        {
            //Uncrouch();
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
            bool isLeft = instigator.bounds.center.x < bodyCollider.bounds.center.x;
            StartCoroutine(Knockback(3f, 30f, transform.position, isLeft));
            StartCoroutine(Invulnerability(instigator, instigator.gameObject.tag != "Ground"));
        }
        else
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }


    }



    private void Die()
    {
        fracJourney = 0f;
        activeCheckpoint = Checkpoint.thisOne;
        startMarker = this.transform;
        endMarker = activeCheckpoint.transform;
        reviveDistance = Vector3.Distance(startMarker.position, endMarker.position);
        canControl = false;
        //bodySprite.color = new Color(0,0,0,0);
        startTime = Time.time;
        isReviving = true;

        //Destroy(gameObject);
    }

    private void Jump()
    {
        if (!isUnderwater)
        {
            startedClimbing = false;
            isFalling = true;
            isJumping = true;
            if (jumpsAvailable > 0)
            {
                jumpsAvailable--;
                sound.volume = 0.05f;
                sound.clip = jumpSound;
                sound.PlayOneShot(jumpSound);
                sound.volume = 0.1f;
                rig.gravityScale = 2;
                rig.drag = 2;
                rig.velocity = new Vector2(rig.velocity.x, 0);
                rig.AddForce(new Vector2(0, jumpSpeed));
            }
        }
        else
        {
            sound.volume = 0.05f;
            sound.clip = jumpSound;
            sound.PlayOneShot(jumpSound);
            sound.volume = 0.1f;
            rig.velocity = new Vector2(rig.velocity.x, 0);
            rig.AddForce(new Vector2(0, jumpSpeed));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isFalling = false;
        }
        if (collision.gameObject.layer == 4)
        {
            touchingWater = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isFalling = true;
        }
        if (collision.gameObject.layer == 4)
        {
            touchingWater = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4)
        {
            if (stillInWater == 0)
            {
                if (gmngr.currentStageProgress < 4)
                {
                    breathMeter = StartCoroutine(Breathage());
                }
                rig.drag = 18f;
                jumpSpeed = 1600;
                rig.sharedMaterial = slideMaterial;
            }
            stillInWater++;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4)
        {
            stillInWater--;
            if (stillInWater == 0)
            {
                rig.drag = 2f;
                rig.sharedMaterial = usualMaterial;
                breathCurrent = breathBase;
                if (breathMeter != null)
                {
                    StopCoroutine(breathMeter);
                }

                touchingWater = false;
                isUnderwater = false;
            }
        }
    }

    private void Crouch()
    {
        if (canCrouch)
        {

            isCrouched = true;
            bodyTransform.localScale = new Vector3(1f, 0.5f, 1f);
            //maxSpeed = crouchedSpeed;
        }
    }

    private void Uncrouch()
    {
        if (isCrouched && canUncrouch)
        {
           // maxSpeed = walkSpeed;
            isCrouched = false;
            bodyTransform.localScale = Vector3.one;
        }
    }

    #region Enumeratory

    public IEnumerator BurnOff()
    {
        float timer = 0;
        while (bootsCurrent > 0)
        {
            timer += Time.deltaTime;
            if (bootsCurrent > 0)
            {
                bootsCurrent = bootsCurrent - Time.deltaTime;
            }
            yield return null;
        }
        if (bootsCurrent < 0)
        {
            bootsCurrent = 0;
        }
        lavaVulnerable = true;
    }

    public IEnumerator BootsRestore()
    {
        int timer = 0;
        lavaVulnerable = false;
        yield return null;
        if (!touchingLava)
        {
            while (bootsCurrent < bootsBase)
            {
                timer++;
                if (timer % 10 == 0)
                {
                    bootsCurrent = bootsCurrent + Time.deltaTime;
                }
                if (bootsCurrent > bootsBase)
                {
                    bootsCurrent = bootsBase;
                }
                yield return null;
            }
        }
        else
        {
            yield break;
        }

    }

    private IEnumerator Breathage()
    {
        float timer = 0;
        while (currentHealth > 0)
        {
            timer += Time.deltaTime;
            if (breathCurrent > 0)
            {
                breathCurrent = breathCurrent - Time.deltaTime;
            }
            else
            {
                GetDamaged(10 * Time.deltaTime, null);
                //currentHealth = currentHealth - 10 * Time.deltaTime;
            }

            yield return null;
        }
    }

    private IEnumerator Invulnerability(Collider2D instigator, bool ignore)
    {
        int i = 0;
        isInvulnerable = true;
        bodySprite.color = new Color(1f, 1f, 1f, 0.3f);
        Physics2D.IgnoreCollision(bodyCollider, instigator, ignore);
        while (i < iTime * 60)
        {
            i++;
            yield return new WaitForSeconds(0);

        }
        isInvulnerable = false;
        if (!isReviving)
        {
            canControl = true;
            bodySprite.color = new Color(1f, 1f, 1f, 1f);
        }
        if (instigator != null)
        {
            Physics2D.IgnoreCollision(bodyCollider, instigator, false);
        }
    }

    private IEnumerator Knockback(float knockbackTime, float knockbackForce, Vector2 knockbackDirection, bool rightOrLeft)
    {
        float timer = 0;
        canControl = false;
        rig.sharedMaterial = freezeMaterial;
        //move = 0;
        rig.velocity = Vector2.zero;
        rig.drag = 25;
        rig.AddForce(new Vector2(30 * knockbackForce * 1f * (1 - (2 * System.Convert.ToInt32(!rightOrLeft))), 20 * 3f * knockbackForce));
        rig.drag = 2;
        while (timer < knockbackTime)
        {
            timer += Time.deltaTime;
            yield return knockbackTime;
        }
        isFalling = true;
        while (!canControl)
        {
            if (isGrounded || canJumpDown || canClimb)
            {
                rig.sharedMaterial = usualMaterial;
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
