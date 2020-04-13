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

    public int coinCount;
    public int coinBounsLife;
    public Text coinText;
    public AudioSource coinPickup;
    
    public Image heartImage1;
    public Image heartImage2;
    public Image heartImage3;
    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;

    public int startLives;
    public int currentLives;
    public Text livesText;

    public int maxHealth;
    public int healthCount;
    public AudioSource itemPickup;

    private bool isRespawning;
    private ResetRespawn[] resetObjects;

    public bool isInvincible;

    public GameObject gameOverScreen;
    public AudioSource gameOverMusic;
    public AudioSource mainLevelMusic;
    public AudioSource CompleteLevelSound;

    public bool respawnCoActive;

    // Use this for initialization
    void Start() 
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
        mainLevelMusic.Play();

        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            healthCount = PlayerPrefs.GetInt("PlayerHealth");
        } else
        {
            healthCount = maxHealth;
        }
        

        if (PlayerPrefs.HasKey("PlayerLives"))
        {
            currentLives = PlayerPrefs.GetInt("PlayerLives");
            UpdateHealth();
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
        
        resetObjects = FindObjectsOfType<ResetRespawn>();
        
	}

    // Update is called once per frame
    void Update()
    {
        if(healthCount <= 0 && !isRespawning)
        {
            Respawn();
            isRespawning = true;
        }

        if(coinBounsLife >= 20)
        {
            currentLives += 1;
            livesText.text = "Lives x " + currentLives;
            coinBounsLife -= 20;
            itemPickup.Play();
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
            mainLevelMusic.Stop();
            gameOverMusic.Play();
        }
        
    }
    
    public IEnumerator RespawnCoroutine()
    {
        respawnCoActive = true;
        
        //Create death pariticals at the same position as the player
        Instantiate(deathParticles, thePlayer.transform.position, thePlayer.transform.rotation);
        playerDeathSound.Play();
        //Creates a delay in the respawn
        yield return new WaitForSeconds(respawnDelay);

        respawnCoActive = false;
        healthCount = maxHealth;
        isRespawning = false;
        UpdateHealth();

        coinCount = 0;
        coinText.text = "Coins: " + coinCount;
        coinBounsLife = 0;

        for (int i = 0; i < resetObjects.Length; i++)
        {
            resetObjects[i].gameObject.SetActive(true);
            resetObjects[i].ResetWorld();
        }

        thePlayer.transform.position = thePlayer.respawnPoint;
        thePlayer.gameObject.SetActive(true);
    }


    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinBounsLife += coinsToAdd;
        coinText.text = "Coins: " + coinCount;
        coinPickup.Play();
    }
    

    public void HurtPlayer(int damageTaken)
    {
        //player cannot be hurt while invincible (are being knocked back)
        if (!isInvincible)
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
                heartImage1.sprite = heartFull;
                heartImage2.sprite = heartFull;
                heartImage3.sprite = heartFull;
                return;

            case 5:
                heartImage1.sprite = heartFull;
                heartImage2.sprite = heartFull;
                heartImage3.sprite = heartHalf;
                return;

            case 4:
                heartImage1.sprite = heartFull;
                heartImage2.sprite = heartFull;
                heartImage3.sprite = heartEmpty;
                return;

            case 3:
                heartImage1.sprite = heartFull;
                heartImage2.sprite = heartHalf;
                heartImage3.sprite = heartEmpty;
                return;

            case 2:
                heartImage1.sprite = heartFull;
                heartImage2.sprite = heartEmpty;
                heartImage3.sprite = heartEmpty;
                return;

            case 1:
                heartImage1.sprite = heartHalf;
                heartImage2.sprite = heartEmpty;
                heartImage3.sprite = heartEmpty;
                return;

            case 0:
                heartImage1.sprite = heartEmpty;
                heartImage2.sprite = heartEmpty;
                heartImage3.sprite = heartEmpty;
                return;

            default:
                heartImage1.sprite = heartEmpty;
                heartImage2.sprite = heartEmpty;
                heartImage3.sprite = heartEmpty;
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

    //public void Save()
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Open(Application.persistentDataPath + "/GameSave.dat", FileMode.Open);

    //    PlayerData data = new PlayerData();
    //    data.health = healthCount;
    //    data.coinCount = coinCount;
    //    data.coinBounsLife = coinBounsLife;
    //    data.currentLives = currentLives;

    //    bf.Serialize(file, data);
    //    file.Close();

    //}

    //public void SaveGame()
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();

    //    //Application.persistnetDataPath will find and use a system file path that is unlikely to change
    //    string path = Application.persistentDataPath + "/Gamesave.dat";
    //    FileStream file = new FileStream(path, FileMode.Create);

    //    PlayerData data = new PlayerData();
    //    ////data.health = healthCount;
    //    ////data.coinCount = coinCount;
    //    ////data.coinBounsLife = coinBounsLife;
    //    ////data.currentLives = currentLives;

    //    formatter.Serialize(file, data);
    //    file.Close();

    //}


    //public void LoadGame()
    //{
    //    string path = Application.persistentDataPath + "/Gamesave.dat";

    //    if (File.Exists(path))
    //    {
    //        BinaryFormatter formatter = new BinaryFormatter();
    //        FileStream file = new FileStream(path, FileMode.Open);

    //        PlayerData data = formatter.Deserialize(file) as PlayerData;
    //        file.Close();

    //        healthCount = data.health;
    //        coinCount = data.coinCount;
    //        coinBounsLife = data.coinBounsLife;
    //        currentLives = data.currentLives;
    //        //public int activeScene;

    //        //Vector3 respawnPoint;
    //        //respawnPoint.x = data.respawnPoint[0];
    //        //respawnPoint.y = data.respawnPoint[1];
    //        //respawnPoint.z = data.respawnPoint[2];
    //        //transform.position = respawnPoint;


    //        //return data;

    //    }
    //    else
    //    {
    //        Debug.LogError("Save file not found in " + path);
    //        //return null;
    //    }
    //}
    
}

//[Serializable]
//class PlayerData
//{
//    public int health;
//    public int coinCount;
//    public int coinBounsLife;
//    public int currentLives;
//    public int activeScene;
//}

