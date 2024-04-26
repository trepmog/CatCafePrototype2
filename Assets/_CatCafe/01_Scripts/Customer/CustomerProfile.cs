using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomerProfile : MonoBehaviour
{
    public string name;
    public string occupation;
    public string[] desiredTraits;
    public ConversationText[] conversationTexts;
    public GameManager gameManager;
    private DataLoader dataManager;
    public event Action OnLoaded;

    void Start()
    {
        dataManager = GameManager.Instance.GetComponent<DataLoader>();
        LoadCustomerData();
    }

    // Update is called once per frame
    void Update() { }

    void LoadCustomerData()
    {
        // Since customer object name will have clone in it, use the temporary limit to create the CustomerID
        // Only 1 customer will be loaded. Normally this would be dynamically created like array.length
        int limit = 1;
        // For each loop is here for when a list of customers is loaded
        CustomerEntry[] customers = dataManager.customerDatabase.customers;

        foreach (CustomerEntry customer in customers)
        {
            if (customer.id == "Customer" + limit.ToString() && limit < 2) 
            {
                name = customer.name;
                occupation = customer.occupation;
                conversationTexts = customer.conversationTexts;
                desiredTraits = customer.desiredTraits;

                limit++;
                break;
            }
        }
        OnLoaded();
    }

    public string[] GetTraits()
    {
        return desiredTraits;
    }
}
