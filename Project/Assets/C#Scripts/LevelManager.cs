using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelManager : MonoBehaviour 
{
    public float respawnDelay;
    public PlayerMovement thePlayer;

    public GameObject deathParticles;
    public AudioSource playerDeathSound;

    public int coinsCollected;
    public int bounsLifeForCoins;
    public Text coinText;
    public AudioSource coinPickup;
    
    public Image heartImage1;
    public Image heartImage2;
    public Image heartImage3;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite noHeart;

    public int startLives;
    public int lives;
    public Text livesText;

    public int maxHealth;
    public int health;
    public AudioSource itemPickup;

    private bool isRespawning;
    private ResetRespawn[] resetObjects;

    public int experience;
    public int experienceForLvl;
    public int expLevel;
    public Text expText;
    public Text expLevelText;

    public GameObject bulletPrefab;

    public bool canBeHurt;

    public GameObject gameOverScreen;
    public AudioSource gameOverMusic;
    public AudioSource levelMusic;
    public AudioSource completeLevelSound;

    public bool respawnCoroActive;

    

    // Use this for initialization
    void Start() 
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
        bulletPrefab.GetComponent<BulletMovement>();
        levelMusic.Play();

        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            health = PlayerPrefs.GetInt("PlayerHealth");
        } else
        {
            health = maxHealth;
        }
        

        if (PlayerPrefs.HasKey("PlayerLives"))
        {
            lives = PlayerPrefs.GetInt("PlayerLives");
            UpdateHealth();
        } else
        {
            lives = startLives;
        }
        
        livesText.text = "Lives x " + lives;


        if (PlayerPrefs.HasKey("CoinsCollected"))
        {
            coinsCollected = PlayerPrefs.GetInt("CoinsCollected");
        }

        coinText.text = "Coins: " + coinsCollected;


        if (PlayerPrefs.HasKey("Experience"))
        {
            experience = PlayerPrefs.GetInt("Experience");
        }

        expText.text = "XP: " + experience;


        if (PlayerPrefs.HasKey("ExperienceForLvl"))
        {
            experienceForLvl = PlayerPrefs.GetInt("ExperienceForLvl");
        }


        if (PlayerPrefs.HasKey("PlayerLevel"))
        {
            expLevel = PlayerPrefs.GetInt("PlayerLevel");
        }

        expLevelText.text = "Lvl: " + expLevel;
        
        resetObjects = FindObjectsOfType<ResetRespawn>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && !isRespawning)
        {
            Respawn();
            isRespawning = true;
        }

        expText.text = "XP: " + experience;

        if (experienceForLvl >= 100)
        {
            expLevel += 1;
            expLevelText.text = "Lvl: " + expLevel;
            bulletPrefab.GetComponent<BulletMovement>().amountOfDamage += 5;
            experienceForLvl -= 100;
        }
 

        if (bounsLifeForCoins >= 20)
        {
            lives += 1;
            livesText.text = "Lives x " + lives;
            bounsLifeForCoins -= 20;
            itemPickup.Play();
        }
    }


    public void Respawn()
    {
        lives -= 1;
        livesText.text = "Lives x " + lives;

        if(lives > 0)
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
        respawnCoroActive = true;
        
        //Create death pariticals at the same position as the player
        Instantiate(deathParticles, thePlayer.transform.position, thePlayer.transform.rotation);
        playerDeathSound.Play();
        //Creates a delay in the respawn
        yield return new WaitForSeconds(respawnDelay);

        respawnCoroActive = false;
        health = maxHealth;
        isRespawning = false;
        UpdateHealth();

        coinsCollected = 0;
        coinText.text = "Coins: " + coinsCollected;
        bounsLifeForCoins = 0;
        experience = 0;
        expText.text = "XP: " + experience;
        experienceForLvl = 0;

        for (int i = 0; i < resetObjects.Length; i++)
        {
            resetObjects[i].gameObject.SetActive(true);
            resetObjects[i].ResetLevel();
        }

        thePlayer.transform.position = thePlayer.respawnPoint;
        thePlayer.gameObject.SetActive(true);
    }


    public void AddCoins(int coinsToAdd)
    {
        coinsCollected += coinsToAdd;
        bounsLifeForCoins += coinsToAdd;
        coinText.text = "Coins: " + coinsCollected;
        coinPickup.Play();
    }
    

    public void HurtPlayer(int damageCausedByEnemy)
    {
        //player cannot be hurt while invincible (are being knocked back)
        if (!canBeHurt)
        {
            health -= damageCausedByEnemy;
            UpdateHealth();

            thePlayer.KnockBackFromEnemy();
            thePlayer.playerDamage.Play();
        }
        
    }

    //controls the player's health shown on screen
    public void UpdateHealth()
    {
        switch(health)
        {
            case 6:
                heartImage1.sprite = fullHeart;
                heartImage2.sprite = fullHeart;
                heartImage3.sprite = fullHeart;
                return;

            case 5:
                heartImage1.sprite = fullHeart;
                heartImage2.sprite = fullHeart;
                heartImage3.sprite = halfHeart;
                return;

            case 4:
                heartImage1.sprite = fullHeart;
                heartImage2.sprite = fullHeart;
                heartImage3.sprite = noHeart;
                return;

            case 3:
                heartImage1.sprite = fullHeart;
                heartImage2.sprite = halfHeart;
                heartImage3.sprite = noHeart;
                return;

            case 2:
                heartImage1.sprite = fullHeart;
                heartImage2.sprite = noHeart;
                heartImage3.sprite = noHeart;
                return;

            case 1:
                heartImage1.sprite = halfHeart;
                heartImage2.sprite = noHeart;
                heartImage3.sprite = noHeart;
                return;

            case 0:
                heartImage1.sprite = noHeart;
                heartImage2.sprite = noHeart;
                heartImage3.sprite = noHeart;
                return;

            default:
                heartImage1.sprite = fullHeart;
                heartImage2.sprite = fullHeart;
                heartImage3.sprite = fullHeart;
                return;
        }
    }

    public void AddLife(int livesToAdd)
    {
        itemPickup.Play();
        lives += livesToAdd;
        livesText.text = "Lives x " + lives;
    }

    public void AddHealth(int healthToAdd)
    {
        health += healthToAdd;

        //health cannot go over max health
        if(health > maxHealth)
        {
            health = maxHealth;
        }

        itemPickup.Play();
        UpdateHealth();
    }
    
}
