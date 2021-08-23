using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractFunction : MonoBehaviour
{
    public Player user;
    
    public float interactRange = 3;
    public LayerMask interactMask = ~0;

    Interactable currentlyLookingAt;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Interactable interactableThisFrame = null;
        RaycastHit interactCheck;
        if (Physics.Raycast(transform.position, transform.forward, out interactCheck, interactRange, interactMask))
        {
            interactableThisFrame = interactCheck.collider.GetComponent<Interactable>();
        }

        if (interactableThisFrame != currentlyLookingAt)
        {
            currentlyLookingAt = interactableThisFrame;
            
            user.hud.RefreshInteractionWindow(currentlyLookingAt);
        }

        if (currentlyLookingAt != null && currentlyLookingAt.IsInteractable == true && Input.GetButtonDown("Interact"))
        {
            currentlyLookingAt.Interact(user);
            user.hud.RefreshInteractionWindow(currentlyLookingAt);
        }
    }
}
