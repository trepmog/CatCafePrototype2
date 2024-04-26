using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MatchMaker : MonoBehaviour
{
    private GameObject cat;
    private CatProfile catProfile;
    private GameObject customer;
    private CustomerProfile customerProfile;
    private CatTreeInteract catTreeInteract;
    private string[] customerTraits;
    private string[] catTraits;
    private bool isMatchMade = false;
    public event Action<bool> OnMatchResult;

    // Start is called before the first frame update
    void Start()
    {
        catTreeInteract = GetComponent<CatTreeInteract>();
        catTreeInteract.OnCatPlacement += MakeMatch;
    }

    // Update is called once per frame
    void Update() { 
        if(catTreeInteract.isOccupied) 
        {
            cat = catTreeInteract.residingCat;
            catProfile = cat.GetComponent<CatProfile>();
            catTraits = catProfile.traits;
            //Debug.Log("Cat traits are: " + catTraits[0] + " and " + catTraits[1]);
        }
        if(catTreeInteract.isCustomerNear) 
        {
            customer = catTreeInteract.detectedCustomer;
            customerProfile = customer.GetComponent<CustomerProfile>();
            customerTraits = customerProfile.desiredTraits;
            //Debug.Log("Customer traits are: " + customerTraits[0] + " and " + customerTraits[1]);
        }
        if(catTreeInteract.isOccupied && catTreeInteract.isCustomerNear && !isMatchMade)
        {
            MakeMatch(customer);
            isMatchMade = true;
        }

    }

    void SetTraits()
    {
        customerTraits = customerProfile.desiredTraits;
        catTraits = catProfile.traits;
    }

    void MakeMatch(GameObject interactor)
    {
        bool matchResult = false;
        int matchCount = 0;
        
        // Compare customer traits and cat traits
        foreach (string customerTrait in customerTraits)
        {
            foreach (string catTrait in catTraits)
            {
                // If a match is found, return true immediately
                if (customerTrait == catTrait)
                {
                    matchCount++;
                }
            }
        }

        // Here's where you can determine the quality of the outcome.
        // matchCount % customerTraits.length will yield a value, then compare that to a set of reward tables
        if(matchCount > 0) matchResult = true;
        Debug.Log("Matching was " + matchResult);
        OnMatchResult(matchResult);
    }
}
