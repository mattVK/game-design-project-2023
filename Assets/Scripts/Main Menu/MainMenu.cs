using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
   public void playGame()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void settings()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
