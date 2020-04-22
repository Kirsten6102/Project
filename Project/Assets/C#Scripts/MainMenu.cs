using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string firstLevel;
    public GameObject helpMenu;

    public int startLives;

    public GameObject changeDifficulty;

    //loads new game
    public void NewGame() 
    {
        changeDifficulty.gameObject.SetActive(true);
    }

    //displays help menu
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
