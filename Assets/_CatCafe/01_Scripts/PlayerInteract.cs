using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interactions;
using System.Linq;

public class PlayerInteract : MonoBehaviour
{
    private GameObject currentInteractable = null;

    public void Interact()
    {
        // Checks to see if an interactable was detected
        if (currentInteractable != null)
        {
            IInteractable interactable = currentInteractable.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // Calls interact on that object
                interactable.Interact();
            }
            else
            {
                // For error handling
                Debug.Log("Object not interactable");
            }
        }
        else
        {
            // For error handling
            Debug.Log("No object to interact with");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            currentInteractable = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentInteractable)
        {
            currentInteractable = null;
        }
    }
}
