using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interactions;
using System.Linq;

public class PlayerInteract : MonoBehaviour
{
    private GameObject currentInteractable = null;
    public float interactionRadius = 2.0f;
    private float lastInteractionTime = 0f;
    private float interactionCooldown = 2f;

    void Update()
    {
        FindNearestInteractable();
    }

    public void Interact()
    {
        // Checks to see if an interactable was detected
        if (currentInteractable != null)
        {
            IInteractable interactable = currentInteractable.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // Calls interact on that object
                interactable.Interact(this.gameObject);
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

    private void FindNearestInteractable()
    {
        // Reset current interactable
        currentInteractable = null;
        float closestDistance = float.MaxValue;

        // Find all interactable objects within interactionRadius
        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRadius);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.GetComponent<IInteractable>() != null)
            {
                float distance = Vector3.Distance(hit.transform.position, transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    currentInteractable = hit.gameObject;
                }
            }
        }
    }



    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.GetComponent<IInteractable>() != null)
    //     {
    //         currentInteractable = other.gameObject;
    //     }
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject == currentInteractable)
    //     {
    //         currentInteractable = null;
    //     }
    // }
}
