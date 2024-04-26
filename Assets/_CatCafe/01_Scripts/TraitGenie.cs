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
        if (!interactionStarted)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnHold(true);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            OnHold(false);
            interactionStarted = false;
        }
    }

    public void Interact(GameObject interactor)
    {
        //OnHold();
        interactionStarted = true;
    }
}
