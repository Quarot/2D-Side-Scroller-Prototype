using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour {
    [SerializeField]
    private float accelerationForce = 5;
    [SerializeField]
    private float maxSpeed = 5;
    [SerializeField]
    private float jumpForce = 10;
    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private Collider2D playerGroundCollider;

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    [SerializeField]
    private Collider2D groundDetectTrigger;

    [SerializeField]
    private ContactFilter2D groundContactFilter;
    [SerializeField]
    AudioClip jumpSound;
    [SerializeField]
    AudioSource soundSource;

    private float horizontalInput;
    private bool isOnGround;
    private Collider2D[] groundHitDetectionResults = new Collider2D[16];
    private Checkpoint currentCheckpoint;
    private AudioSource audioSource;
    
    bool facingRight = true;
    float groundRadius = 0.2f;
    bool playerDead = false;
    
    Animator anim;

    

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateIsOnGround();
        UpdateHorizontalInput();
        HandleJumpInput();
    }

    private void UpdateHorizontalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        UpdatePhysicsMaterial();
        if (playerDead == false)
        {
            Move();
        }
        

        

        if (horizontalInput > 0 && !facingRight)
            Flip();
        else if (horizontalInput < 0 && facingRight)
            Flip();

        
        anim.SetBool("Ground", isOnGround);

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Dead", playerDead);
    }

    private void UpdatePhysicsMaterial()
    {
        if (Mathf.Abs(horizontalInput) > 0)
        {
            playerGroundCollider.sharedMaterial = playerMovingPhysicsMaterial;
        }
        else
        {
            playerGroundCollider.sharedMaterial = playerStoppingPhysicsMaterial;
        }
    }

    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundHitDetectionResults) > 0;
        //Debug.Log("IsOnGround?: " + isOnGround);

    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //Debug.Log("Player is jumping");
            SoundManagerScript.PlaySound("doJump");
        }
    }

    private void Move()
    {
        rb2d.AddForce(Vector2.right * horizontalInput * accelerationForce);
        Vector2 clampedVelocity = rb2d.velocity;
        clampedVelocity.x = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = clampedVelocity;
    }

    public void KillPlayer()
    {
        playerDead = true;
        SoundManagerScript.PlaySound("playerDies");
    }

    private void UpdateRespawn()
    {
        if (Input.GetButtonDown("Respawn") && playerDead == true)
        {
            Respawn();
        }
        
    }

    public void Respawn()
    {
        //SoundManagerScript.PlaySound("playerDies");
        //TO DO: Don't reload the scene until done with death animation, and sound effect
        if (currentCheckpoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //SoundManagerScript.PlaySound("playerDies");
            playerDead = false;
        }
        else
        {
            rb2d.velocity = Vector2.zero;
            transform.position = currentCheckpoint.transform.position;
            //SoundManagerScript.PlaySound("playerDies");
            playerDead = false;
        }
        audioSource.Play();


    }

    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        if(currentCheckpoint != null)
        {
            currentCheckpoint.SetIsActivated(false);

        }

        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
