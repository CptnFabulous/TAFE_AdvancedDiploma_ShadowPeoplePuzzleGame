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

    public HeadsUpDisplay headsUpDisplay;
    public Menu pauseMenu;
    public LevelCompleteScreen winMenu;
    public LevelFailedScreen gameOverMenu;
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

    void SwitchWindow(GameObject g)
    {
        headsUpDisplay.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        winMenu.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(false);
        g.SetActive(true);
    }

    public void PauseGame()
    {
        Debug.Log("Pausing game");
        CurrentState = GameState.Paused;
        Time.timeScale = 0;
        SwitchWindow(pauseMenu.gameObject);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game");
        CurrentState = GameState.Playing;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SwitchWindow(headsUpDisplay.gameObject);
        Time.timeScale = 1;
    }

    public void WinGame()
    {
        CurrentState = GameState.HasWon;
        Time.timeScale = 0;
        SwitchWindow(winMenu.gameObject);
        winMenu.Populate(player);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void FailGame()
    {
        CurrentState = GameState.HasLost;
        Time.timeScale = 0;
        SwitchWindow(gameOverMenu.gameObject);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
