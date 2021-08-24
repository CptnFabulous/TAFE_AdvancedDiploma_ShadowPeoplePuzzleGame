using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public class Interactable : MonoBehaviour
{
    [System.Serializable]
    public class InteractEvent : UnityEvent<Player>
    {
        public InteractEvent() { }
    }

    public bool enabled = true;
    public string prompt = "Interact";
    public string deniedMessage = "Disabled";
    public InteractEvent onInteract = new InteractEvent();

    public virtual bool IsInteractable
    {
        get
        {
            return enabled;
        }
    }

    public void Interact(Player user)
    {
        onInteract.Invoke(user);
    }
}
