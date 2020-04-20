using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string firstLevel;
    public GameObject helpMenu;

    public int startLives;

    //loads new game
    public void NewGame() 
    {
        SceneManager.LoadScene(firstLevel);

        PlayerPrefs.SetInt("CoinsCollected", 0);
        PlayerPrefs.SetInt("PlayerLives", startLives);
        PlayerPrefs.SetInt("Experience", 0);
        PlayerPrefs.SetInt("ExperienceForLvl", 0);
        PlayerPrefs.SetInt("PlayerLevel", 1);
    }


    //loads saved game
    public void Continue()
    {
        
    }

    public void Help()
    {
        helpMenu.gameObject.SetActive(true);
    }

    public void Return()
    {
        helpMenu.gameObject.SetActive(false);
    }

    //quits game
    public void Quit()
    {
        Application.Quit();
    }

}
