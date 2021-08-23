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
    public Canvas winScreen;
    public Canvas failScreen;
    public string levelNameForRespawning;

    public void WinLevel()
    {

    }

    public void FailLevel()
    {

    }

    
}
