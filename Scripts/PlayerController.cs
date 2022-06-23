using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    
    public HealthBar healthBar;
    public BulletRechargeBar bulletBar;
    public LifeManager lifeCount;
    private UnhideStar unhideStar;
    private UnhideCross unhideCross;

    [Header("Player Movements")]
    public float moveSpeed;
    public float jumpForce = 100;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Animator animator;

    [Header("Bullet Properties")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject MuzzleFlash;
    public int bulletCount = 2;
    public int muzzleFlashCount = 5;

    private Rigidbody2D rb;
    private GameObject player;
    private bool isGrounded;
    private float moveDirection;
    private bool facingLeft = true;
    private bool isGravityNormal = true;
    private bool playerIsDead = false;
    public static bool controlsDisabled = false;
    private bool top;
    private bool jumping;
    private GameMaster gm;
    [Header("Other Properties")]
    public UnityEvent landingEvent;
    public AudioClip GunSound;
    public AudioClip JumpSound;
    public AudioClip HitSound;

    // Awake is called after all objects are initialised. Called in a random order.
    private void Awake()
    {
      
       

        player = GameObject.FindGameObjectWithTag("Player");

        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckpointPosition;

        rb = GetComponent<Rigidbody2D>();
        if (landingEvent == null)
            landingEvent = new UnityEvent();

    }


    // Start is called before the first frame update
    void FixedUpdate()
    {
        
        bool wasGrounded = isGrounded;
        isGrounded = false;

        // Casts a circle at the players empty ground object which will set grounded to true if touching the layer associated with ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, checkRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                    landingEvent.Invoke();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //get inputs
        Inputs();

        // weapon firing
        ShootWeapon();

        //Animation
        Animate();

        //move
        Movement();

        //swap gravity
        GravitySwap();

        //jumping based on conditionals
        GravityDetectJumping();

        //enable and disable gravity icons
        GravityIcons();

        
       

       


    }


    //allows the player to jump based on the gravity of the level
    private void GravityDetectJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGravityNormal == true && isGrounded == true && !jumping)
        {
           
            rb.velocity = Vector2.up * jumpForce;
            jumping = true;
            animator.SetBool("Jumping", true);
            Invoke("ResetJumping", .5f);
            AudioSource.PlayClipAtPoint(JumpSound, transform.position);

        } 
        else if (Input.GetKeyDown(KeyCode.Space) && isGravityNormal == false && isGrounded == true && !jumping)
        {
            
            rb.velocity = Vector2.down * jumpForce;
            jumping = true;
            animator.SetBool("Jumping", true);
            Invoke("ResetJumping", .5f);
            AudioSource.PlayClipAtPoint(JumpSound, transform.position);

        }
    }

    //when the player lands set the jumping animation to be false
    public void OnLanding()
    {
        animator.SetBool("Jumping", false);
    }


    //allows the switch of gravity as long as the player is not moving adn applies a force based on current gravity
    public void GravitySwap()
    {
        if (Input.GetKeyDown(KeyCode.Q) && PauseMenu.GamePaused == false && moveDirection == 0 && isGravityNormal == true)
        {
            controlsDisabled = true;
            facingLeft = true;
            rb.gravityScale = -1f;
            Invoke("Rotation", 0.2f);
            Invoke("EnableInputs", 0.2f);
            isGravityNormal = false;
            rb.velocity = Vector2.up * 5f;
        }else if (Input.GetKeyDown(KeyCode.Q) && PauseMenu.GamePaused == false && moveDirection == 0 && isGravityNormal == false)
        {
            controlsDisabled = true;
            facingLeft = true;
            rb.gravityScale = 1f;
            Invoke("Rotation", 0.2f);
            Invoke("EnableInputs", 0.2f);
            isGravityNormal = true;
            rb.velocity = Vector2.down * 5f;
        }
    }

    //hide and display correct gravity icons
    private void GravityIcons()
    {
        if(moveDirection == 0)
        {
            
            UnhideStar.instance.ShowStarIcon();
            UnhideCross.instance.HideCrossIcon();
            
        }
        else
        {
           
            UnhideStar.instance.HideStarIcon();
            UnhideCross.instance.ShowCrossIcon();
            
        }
    }

    private void ResetJumping()
    {
        jumping = false;
    }

    //rotates the player sprite to account for a switch in gravity
    void Rotation()
    {
        if (top == false)
        {
            transform.eulerAngles = new Vector3(0, 180f, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
        top = !top;
    }

    //applies a velocity based on the move direction of the player
    private void Movement()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveDirection));
    }

    //Allows the player to move if controls are not disabled
    private void Inputs()
    {
        if (controlsDisabled == false)
        {
            moveDirection = Input.GetAxis("Horizontal"); // Scale of -1 -> 1
        }
        
    }
    
    //enables the inputs of the player
    public void EnableInputs()
    {
        controlsDisabled = false; ;
    }

    private void ShootWeapon()
    {
        // shoots a bullet from the player and removes a bullet from the bullet count
        if (Input.GetButtonDown("Fire1") && BulletRechargeBar.instance.currentRecharge >= 25  && PauseMenu.GamePaused == false)
        {
            Shoot();
            MuzzleFlash.SetActive(true);
            bulletCount--;
            Invoke("hideMuzzleFlash", 0.2f);
            BulletRechargeBar.instance.UseRecharge(25);
            AudioSource.PlayClipAtPoint(GunSound, transform.position);
        }
    }
    
    //hide the muzzle of the weapon
    void hideMuzzleFlash()
    {
        MuzzleFlash.SetActive(false);
    }

    void Shoot()
    {
        // creates a bullet from the players firing position
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    }

    //take damage based on an int
    public void TakeDamage(int damage)
    {
        
        HealthBar.instance.HealthAmount(damage);
        animator.SetBool("Hit", true);
        Invoke("PlayerHitReset", 0.6f);
        AudioSource.PlayClipAtPoint(HitSound, transform.position);

        if (HealthBar.instance.currentHealth <= 0 && LifeManager.instance.currentLives >= 0 && playerIsDead == false)
        {
            Die();
            playerIsDead = true;
            
            Invoke("ReloadPlayer", 0.4f);
        }else if(HealthBar.instance.currentHealth <= 0 && LifeManager.instance.currentLives < 0)
        {
            player.SetActive(false);
           
        }
    }

    void Die()
    {
        player.SetActive(false);
        LifeManager.instance.ChangeLives(-1);
        
    }
    

    void PlayerHitReset()
    {
        animator.SetBool("Hit", false);
    }

    //reloads the player to the most current checkpoint located within the checkpoint master
    void ReloadPlayer()
    {
        player.transform.position = gm.lastCheckpointPosition;
        player.SetActive(true);
        playerIsDead = false;
        if(isGravityNormal == false)
        {
            rb.gravityScale = 1;
            Rotation();
            isGravityNormal = true;
            facingLeft = true;
        }
        HealthBar.instance.MaxHealth(HealthBar.instance.maxHealth);
        HealthBar.instance.currentHealth = HealthBar.instance.maxHealth;
    }

    //saves data from the player
   public void SavePlayer()
    {
        Saving.SavePlayer(this);
    } 

   

    //flip the player based on direction
    private void Animate()
    {
        if (facingLeft == false && moveDirection < 0)
        {
            FlipPlayer();
        }
        else if (facingLeft == true && moveDirection > 0)
        {
            FlipPlayer();
        }
    }

    private void FlipPlayer()
    {
        facingLeft = !facingLeft;
        transform.Rotate(0f, 180f, 0f);
    }
}
