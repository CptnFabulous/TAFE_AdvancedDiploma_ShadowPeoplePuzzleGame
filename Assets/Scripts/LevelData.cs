using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData internalReferenceForCurrent;
    public static LevelData Current
    {
        get
        {
            if (internalReferenceForCurrent == null)
            {
                internalReferenceForCurrent = FindObjectOfType<LevelData>();
            }

            return internalReferenceForCurrent;
        }
    }

    public int startingLives = 1;
    public int optimalLivesLeft = 0;
    public string currentLevelNameForRespawning;
    public string nextLevel;


    private void Start()
    {
        // Set up level
        SpawnPoint.Current.SpawnPlayer();
        Player.Current.Health.lives = startingLives;
    }

    public void WinLevel()
    {
        Player[] playersInScene = FindObjectsOfType<Player>();
        foreach(Player p in playersInScene)
        {
            p.PauseHandler.WinGame();
        }
    }

    public void FailLevel()
    {
        Player[] playersInScene = FindObjectsOfType<Player>();
        foreach (Player p in playersInScene)
        {
            p.PauseHandler.FailGame();
        }
    }

    
}
