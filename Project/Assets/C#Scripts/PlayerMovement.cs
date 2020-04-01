using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    //Movement and jump values
    public float movementSpeed;
    private Rigidbody2D playerRigidbody;
    public float jumpSpeed;

    //Values used to check if player is on the ground
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public bool isGrounded;

    private Animator playerAnim;

    public Vector3 respawnPoint;

    public LevelManager theLevelManager;

    // Use this for initialization
    void Start () {

        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();

        respawnPoint = transform.position;

        theLevelManager = FindObjectOfType<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {

        //Checks if player is on ground or not
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            playerRigidbody.velocity = new Vector3(movementSpeed, playerRigidbody.velocity.y, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);

        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            playerRigidbody.velocity = new Vector3(-movementSpeed, playerRigidbody.velocity.y, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {   //if no input velocity is 0 - stops the player sliding
            playerRigidbody.velocity = new Vector3(0f, playerRigidbody.velocity.y, 0f);
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpSpeed, 0f);
        }

        //Sets speed and velocity of rigidbody for the player animation
        //Mathf.Abs = math function that turns a negitive vaule into a positive so animation doesn't stop
        //when player is walking left.
        playerAnim.SetFloat("Speed", Mathf.Abs(playerRigidbody.velocity.x));
        playerAnim.SetBool("Grounded", isGrounded);

    }

    //Trigger death zone and checkpoints
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DeathZone")
        {
            theLevelManager.Respawn();
        }

        if (other.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
        }

    }

}
