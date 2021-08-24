using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MovementController Movement { get; private set; }
    public Health Health { get; private set; }
    public GameStateHandler PauseHandler { get; private set; }

    public HeadsUpDisplay hud;
    public InteractFunction interaction;

    public static Player Current
    {
        get
        {
            return FindObjectOfType<Player>();
        }
    }

    public static Player[] AllCurrent
    {
        get
        {
            return FindObjectsOfType<Player>();
        }
    }

    private void Awake()
    {
        Movement = GetComponent<MovementController>();
        Health = GetComponent<Health>();
        PauseHandler = GetComponent<GameStateHandler>();

        interaction.user = this;
    }

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        Health.current = Health.max;
        hud.Refresh();
    }
}
