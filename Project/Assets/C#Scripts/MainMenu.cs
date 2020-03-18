using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //public string firstLevel;
    //public string levelSelect;

    //loads new game
    public void NewGame() 
    {
        SceneManager.LoadScene("Level_1");
    }
    
    //loads saved game
    public void Continue()
    {
        
    }

    //quits game
    public void Quit()
    {
        Application.Quit();
    }

}
