using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Paused,
        InMenus,
        HasWon,
        HasLost
    }

    Player player;

    public Canvas headsUpDisplay;
    public Canvas pauseMenu;
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        ResumeGame();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Debug.Log("Pause button pressed");
            if (CurrentState == GameState.Playing)
            {
                PauseGame();
            }
            else if (CurrentState == GameState.Paused)
            {
                ResumeGame();
            }
        }
    }

    void SwitchWindow(Canvas c)
    {
        headsUpDisplay.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        c.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        Debug.Log("Pausing game");
        CurrentState = GameState.Paused;
        Time.timeScale = 0;
        SwitchWindow(pauseMenu);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game");
        CurrentState = GameState.Playing;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SwitchWindow(headsUpDisplay);
        Time.timeScale = 1;
    }
}
