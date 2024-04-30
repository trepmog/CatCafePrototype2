using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MatchMaker : MonoBehaviour
{
	private static MatchMaker s_instance;

    private GameObject cat;
    private CatProfile catProfile;
    private GameObject customer;
    private CustomerProfile customerProfile;
    private CatTreeInteract catTreeInteract;
    private string[] customerTraits;
    private string[] catTraits;
    public event Action<bool> OnMatchResult;

	private void Awake()
	{
		s_instance = this;
	}

    // Start is called before the first frame update
    void Start()
    {
        catTreeInteract = GetComponent<CatTreeInteract>();
    }

    // Update is called once per frame
    void Update() { 
        if(catTreeInteract.isCustomerNear) 
        {
            customer = catTreeInteract.detectedCustomer;
            customerProfile = customer.GetComponent<CustomerProfile>();
            customerTraits = customerProfile.desiredTraits;
            //Debug.Log("Customer traits are: " + customerTraits[0] + " and " + customerTraits[1]);
        }
    }

    void SetTraits()
    {
        customerTraits = customerProfile.desiredTraits;
        catTraits = catProfile.traits;
    }

    private void MakeMatchInternal( GameObject _cat )
    {
		if ( cat != _cat )
		{ 
			cat = _cat;
			catProfile = cat.GetComponent<CatProfile>();
			catTraits = catProfile.traits;
			//Debug.Log("Cat traits are: " + catTraits[0] + " and " + catTraits[1]);
		}

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

	public static void MakeMatch( GameObject _cat )
	{
		s_instance.MakeMatchInternal( _cat );
	}
}
