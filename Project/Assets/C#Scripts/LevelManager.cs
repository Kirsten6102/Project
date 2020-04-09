using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour 
{
    public float respawnDelay;
    public PlayerMovement thePlayer;

    public GameObject deathParticles;
    public AudioSource deathSound;

    public int coinCount;
    public Text coinText;
    public AudioSource coinPickup;

    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;

    public int startLives;
    public int currentLives;
    public Text livesText;

    public int maxHealth;
    public int healthCount;
    public AudioSource itemPickup;

    private bool respawning;
    private ResetRespawn[] objectsToReset;

    public bool invincible;

    public GameObject gameOverScreen;
    public AudioSource gameOverMusic;
    public AudioSource levelMusic;
    public AudioSource CompleteLevelSound;

    // Use this for initialization
    void Start() 
    {
        thePlayer = FindObjectOfType<PlayerMovement>();

        healthCount = maxHealth;

        if(PlayerPrefs.HasKey("PlayerLives"))
        {
            currentLives = PlayerPrefs.GetInt("PlayerLives");
        } else
        {
            currentLives = startLives;
        }
        
        livesText.text = "Lives x " + currentLives;

        if (PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount = PlayerPrefs.GetInt("CoinCount");
        }

        coinText.text = "Coins: " + coinCount;
        
        objectsToReset = FindObjectsOfType<ResetRespawn>();
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
        currentLives -= 1;
        livesText.text = "Lives x " + currentLives;

        if(currentLives > 0)
        {
            thePlayer.gameObject.SetActive(false);
            StartCoroutine("RespawnCoroutine");
        } else
        {
            thePlayer.gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
            levelMusic.Stop();
            gameOverMusic.Play();
        }
        
    }
    
    public IEnumerator RespawnCoroutine()
    {
        //Create death pariticals at the same position as the player
        Instantiate(deathParticles, thePlayer.transform.position, thePlayer.transform.rotation);
        deathSound.Play();
        //Creates a delay in the respawn
        yield return new WaitForSeconds(respawnDelay);

        healthCount = maxHealth;
        respawning = false;
        UpdateHealth();

        coinCount = 0;
        coinText.text = "Coins: " + coinCount;

        for (int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].gameObject.SetActive(true);
            objectsToReset[i].ResetWorld();
        }

        thePlayer.transform.position = thePlayer.respawnPoint;
        thePlayer.gameObject.SetActive(true);
    }


    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinText.text = "Coins: " + coinCount;
        coinPickup.Play();
    }

    public void HurtPlayer(int damageTaken)
    {
        //player cannot be hurt while invincible (are being knocked back)
        if (!invincible)
        {
            healthCount -= damageTaken;
            UpdateHealth();

            thePlayer.KnockBack();
            thePlayer.playerDamage.Play();
        }
        
    }

    //controls the player's health shown on screen
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

    public void AddLife(int livesToAdd)
    {
        itemPickup.Play();
        currentLives += livesToAdd;
        livesText.text = "Lives x " + currentLives;
    }

    public void AddHealth(int healthToAdd)
    {
        healthCount += healthToAdd;

        //health cannot go over max health
        if(healthCount > maxHealth)
        {
            healthCount = maxHealth;
        }

        itemPickup.Play();
        UpdateHealth();
    }
}
