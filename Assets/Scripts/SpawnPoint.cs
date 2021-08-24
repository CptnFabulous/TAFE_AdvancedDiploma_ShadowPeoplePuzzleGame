using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Player playerPrefab;

    public void SpawnPlayer()
    {
        Player p = Player.Current;
        if (p == null)
        {
            p = Instantiate(playerPrefab);
        }
        Respawn(p);
    }

    public void Respawn(Player playerRespawning)
    {
        Debug.Log(playerRespawning);
        playerRespawning.transform.position = transform.position;
        playerRespawning.Movement.LookAt(transform.forward);
        playerRespawning.Refresh();
    }


    public static SpawnPoint internalReferenceForCurrent;
    public static SpawnPoint Current
    {
        get
        {
            if (internalReferenceForCurrent == null)
            {
                internalReferenceForCurrent = FindObjectOfType<SpawnPoint>();
            }

            return internalReferenceForCurrent;
        }
    }
}
