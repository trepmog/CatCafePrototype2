using UnityEngine;
using UnityEngine.AI;
using Game.Interactions;
using System;

// Script for handling Customer interaction behaviors
public class Customer : MonoBehaviour, IInteractable
{
    private bool playerLeft = false;
    private bool hasCat = false;
    private NavMeshAgent agent; 
    public event Action<string> OnInteract;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Interact(GameObject interactor)
    {
        // Stop wandering
        agent.isStopped = true;
        Debug.Log("Player interacted with Customer");
        // Trigger showing convo event
        OnInteract("Customer1");
    }

    public void Resume()
    {
        // Resume wandering
        agent.isStopped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cat"))
        {
            hasCat = true;
            Debug.Log("Cat entered the customer");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerLeft = true;
            Debug.Log("Player left the customer");
        }
    }
}
