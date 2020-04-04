using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour 
{
    public float respawnDelay;
    public PlayerMovement thePlayer;

    public GameObject deathParticles;

    public int coinCount;

    public Text coinText;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;

    public int maxHealth;
    public int healthCount;

    private bool respawning;

    // Use this for initialization
    void Start() 
    {
        thePlayer = FindObjectOfType<PlayerMovement>();

        coinText.text = "Coins: " + coinCount;

        healthCount = maxHealth;
	}

    // Update is called once per frame
    void Update()
    {
        if(healthCount <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
        }
    }


    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }
    
    public IEnumerator RespawnCoroutine()
    {
        thePlayer.gameObject.SetActive(false);

        //Create death pariticals at the same position as the player
        Instantiate(deathParticles, thePlayer.transform.position, thePlayer.transform.rotation);
        
        //Creates a delay in the respawn
        yield return new WaitForSeconds(respawnDelay);

        healthCount = maxHealth;
        respawning = false;
        UpdateHealth();
        thePlayer.transform.position = thePlayer.respawnPoint;
        thePlayer.gameObject.SetActive(true);
    }


    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinText.text = "Coins: " + coinCount;
    }

    public void HurtPlayer(int damageTaken)
    {
        healthCount -= damageTaken;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        switch(healthCount)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                return;

            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                return;

            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                return;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
        }
    }

}
