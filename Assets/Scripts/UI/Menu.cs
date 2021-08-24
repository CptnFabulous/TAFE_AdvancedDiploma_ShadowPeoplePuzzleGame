using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    


    public virtual void Populate()
    {

    }

    public void LoadNextLevel()
    {
        string name = LevelData.Current.nextLevel;
        if (name == null || name == "")
        {
            name = "Game Completed";
        }
        SceneManager.LoadScene(name);
    }

    public void RestartLevel()
    {
        string name = LevelData.Current.currentLevelNameForRespawning;
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
