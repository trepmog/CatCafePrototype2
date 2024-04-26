using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interactions;
using System;

public class CatTreeInteract : MonoBehaviour, IInteractable
{
    public bool isOccupied;
    public bool isCustomerNear = false;
    public GameObject residingCat;
    public float detectionRadius = 5.0f;  // Radius within which to detect customers
    public GameObject detectedCustomer = null;
    public event Action<GameObject> OnCatPlacement;


    // Beware, this script does not yet contain a way to clear the current customer when customer leaves
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(detectedCustomer == null)
        {
            DetectCustomer();
        }
        
    }
    public void SetIsOccupied(bool isCatPresent)
    {
        isOccupied = isCatPresent;
    }

    public bool GetIsOccupied()
    {
        return isOccupied;
    }

    public GameObject GetResidingCat()
    {
        return residingCat;
    }

    public GameObject GetDetectedCustomer()
    {
        return detectedCustomer;
    }

    public void Interact(GameObject interactor)
    {
        OnCatPlacement(interactor);
    }

    void DetectCustomer()
    {
        // Clear previous detected customer
        detectedCustomer = null;

        // Get all colliders within the detection radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Customer"))
            {
                detectedCustomer = hitCollider.gameObject;
                isCustomerNear = true;
                break;  // Optional: break if you only need one customer
            }
        }
    }
}
