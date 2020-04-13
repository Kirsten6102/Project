using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerDataaa {

    //getting and store data from the level manager

    public int health;
    public int coinCount;
    public int coinBounsLife;
    public int currentLives;
    public int activeScene;
    //public float[] respawnPoint;
    

    public PlayerDataaa (LevelManager LvlMnger)
    {
        health = LvlMnger.healthCount;
        coinCount = LvlMnger.coinCount;
        coinBounsLife = LvlMnger.coinBounsLife;
        currentLives = LvlMnger.currentLives;

        //respawnPoint = new float[3];
        //respawnPoint[0] = player.transform.position.x;
        //respawnPoint[1] = player.transform.position.y;
        //respawnPoint[2] = player.transform.position.z;

        activeScene = SceneManager.GetActiveScene().buildIndex;

    }

}
