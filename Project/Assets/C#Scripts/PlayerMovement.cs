using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    //Movement and jump values
    public float movementSpeed;
    public float sprintSpeed = 1.5f;
    public Rigidbody2D playerRigidbody;
    public float jumpSpeed;
    public bool canMove;

    //Values used to check if player is on the ground
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;

    private Animator playerAnim;

    public Vector3 respawnPoint;

    public LevelManager levelManager;

    public float knockForce;
    public float knockbackLength;
    private float knockback;

    public float invincibilityLength;
    private float invincibility;

    public AudioSource jumpSound;
    public AudioSource playerDamage;

    public GameObject firePoint;


    void Start () {

        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();

        respawnPoint = transform.position;

        levelManager = FindObjectOfType<LevelManager>();

        canMove = true;
    }
	
	void Update () {

        //Checks if player is on ground or not
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (knockback <= 0 && canMove)
        {
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                playerRigidbody.velocity = new Vector3(movementSpeed, playerRigidbody.velocity.y, 0f);
                //eulerAngles stores the rotation of a game object 
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                if (Input.GetButton("Sprint"))
                {
                    movementSpeed = 30f;
                } else
                {
                    movementSpeed = 15f;
                }
                
            } else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                playerRigidbody.velocity = new Vector3(-movementSpeed, playerRigidbody.velocity.y, 0f);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                if (Input.GetButton("Sprint"))
                {
                    movementSpeed = 30f;
                }
                else
                {
                    movementSpeed = 15f;
                }
            } else
            {   //if no input velocity is 0 - stops the player sliding
                playerRigidbody.velocity = new Vector3(0f, playerRigidbody.velocity.y, 0f);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpSpeed, 0f);
                jumpSound.Play();
            }
        } else if (knockback > 0)
        {
            knockback -= Time.deltaTime;
            if(transform.localScale.x > 0)
            {
                playerRigidbody.velocity = new Vector3(-knockForce, knockForce, 0f);
            } else
            {
                playerRigidbody.velocity = new Vector3(knockForce, knockForce, 0f);
            }
        }

        
        if(invincibility > 0)
        {
            invincibility -= Time.deltaTime;
        } else if(invincibility <= 0)
        {
            levelManager.canBeHurt = false;
        }
        
        //Sets speed and velocity of rigidbody for the player animation
        /*Mathf.Abs = math function that turns a negitive vaule into a positive so animation doesn't stop
        when player is walking left.*/
        playerAnim.SetFloat("Speed", Mathf.Abs(playerRigidbody.velocity.x));
        playerAnim.SetBool("Grounded", isGrounded);

    }
    

    //Trigger death zone and checkpoints
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DeathZone")
        {
            levelManager.Respawn();
        }

        if (other.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
        }

    }


    public void KnockBackFromEnemy()
    {
        knockback = knockbackLength;
        invincibility = invincibilityLength;
        levelManager.canBeHurt = true;

    }

}
