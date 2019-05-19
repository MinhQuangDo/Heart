using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winning : MonoBehaviour
{
    public void playAgain()
    {
        SceneManager.LoadScene("Opening_Hard");
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
