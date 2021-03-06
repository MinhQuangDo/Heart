using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string CurrentSceneName;
    public string NextSceneName;
    // public string MainMenuName = "Main Menu";
    [SerializeField] private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
      Player = GameObject.FindGameObjectWithTag("Player");
        // GameObject findScene = GameObject.FindGameObjectWithTag("LoadScene");
        // if(CurrentSceneName == "")
        // {
        //     if(findScene != null)
        //     {
        //         CurrentSceneName = findScene.GetComponent<SceneLoader>().CurrentSceneName;
        //     }
        //     else
        //     {
        //         Debug.Log(this.gameObject + " has no Current Scene Name variable");
        //     }
        // }
        // if (NextSceneName == "")
        // {
        //     if (findScene != null)
        //     {
        //         NextSceneName = findScene.GetComponent<SceneLoader>().NextSceneName;
        //     }
        //     else
        //     {
        //         Debug.Log(this.gameObject + " has no Next Scene Name variable");
        //     }
        // }
        // if (MainMenuName == "")
        // {
        //     Debug.Log(this.gameObject + " has no Main Menu Name variable");
        // }
    }

    // public void restartTimer()
    // {
    //     GameObject obj = GameObject.FindGameObjectWithTag("Music");
    //     if (obj != null)
    //     {
    //         obj.GetComponent<MusicBox>().startNewTimer();
    //     }
    // }

    // UI and buttons
    // public void MainMenu()
    // {
    //     Time.timeScale = 1;
    //     GameObject obj = GameObject.FindGameObjectWithTag("Music");
    //     if(obj != null)
    //     {
    //         obj.GetComponent<MusicBox>().playTitle();
    //     }
    //     SceneManager.LoadScene(MainMenuName);
    // }

    // // UI and buttons
    // public void NextLevel()
    // {
    //     Time.timeScale = 1;
    //     SceneManager.LoadScene(NextSceneName);
    // }
    //
    // // UI and buttons
    // public void RestartLevel()
    // {
    //     Time.timeScale = 1;
    //     SceneManager.LoadScene(CurrentSceneName);
    // }

    // REMEBER TO SET TO TRIGGER
    //Door for player and other colliders
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 1;
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(NextSceneName);
        }
    }

    // Update is called once per frame
    void Update()
    {
      // if(col.gameObject.tag == "Player")
      // {
      //     PlayerMovement playerAvatar;
      //     playerAvatar = Player.GetComponent<PlayerMovement>();
      //     if(playerAvatar.alive) {
      //         playerAvatar.Die();
      //     }
      //     // Item box should destroy itself post collision
      //     // LevelManager.SendMessage("Restart");
      //     SceneManager.LoadScene(CurrentSceneName);
      // }
    }
}
