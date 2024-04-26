using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interactions;
using System;

public class TraitGenie : MonoBehaviour, IInteractable
{
    public event Action<bool> OnHold;
    private bool interactionStarted = false;

    void Start()
    {
        
    }


    void Update()
    {
        // If Interact was called, Update may check for key hold
        if (!interactionStarted)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnHold(true);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            // If the key is not being held, reset interaction status
            OnHold(false);
            interactionStarted = false;
        }
    }

    public void Interact(GameObject interactor)
    {
        // Set a flag to allow Update to check for Key press
        interactionStarted = true;
    }
}
