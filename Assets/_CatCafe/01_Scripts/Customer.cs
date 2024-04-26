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
    public TraitGenie traitGenie; 
    private CustomerIcons customerIcons;
    private bool keyHeld = false;
    public event Action<string> OnInteract;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        customerIcons = GetComponent<CustomerIcons>();
        GameObject traitGenieObj = GameObject.FindWithTag("TraitGenie");
        traitGenie = traitGenieObj.GetComponent<TraitGenie>();
        traitGenie.OnHold += FindKeyHolding;
    }

    void Update()
    {
        // Shows traits if true, hides if false
        ToggleShowTraits(keyHeld);
    }

    public void Interact(GameObject interactor)
    {
        // Stop wandering
        agent.isStopped = true;
        // Trigger showing convo event
        // First customer is hard coded, this will change when we cycle through customers and load data
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

    private void FindKeyHolding(bool isHeld)
    {
        // Sets local boolean to event given boolean
        keyHeld = isHeld;
    }

    public void ToggleShowTraits(bool toggle)
    {
        customerIcons.ToggleShowIcons(toggle);
    }
}
