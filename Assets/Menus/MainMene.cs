using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMene : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("Opening");
    }

    public void playGameHard()
    {
        SceneManager.LoadScene("Opening_Hard");
    }

    public void playGameMedium()
    {
        SceneManager.LoadScene("Opening_Medium");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
