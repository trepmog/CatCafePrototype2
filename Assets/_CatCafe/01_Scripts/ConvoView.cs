using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConvoView : MonoBehaviour
{
    public Customer customer;
    private GameObject convoBG;
    public TextMeshProUGUI conversationText;
    private CustomerDatabase customerDB;

    void Start()
    {
        LoadCustomerData();
        customer.OnInteract += ShowConvoUI;
        convoBG = transform.Find("ConvoBG").gameObject;
        convoBG.SetActive(false);
        //conversationText = convoBG.transform.Find("TextObject").GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        customer.OnInteract -= ShowConvoUI;
    }

    public void ShowConvoUI(string customerId)
    {
        Debug.Log("Attempted to show conversation");
        // Get the demands text
        string demands = DisplayCustomerDemands(customerId);
        // Display conversation UI
        convoBG.SetActive(true);
        conversationText.text = demands;
    }

    public void HideConvoUI()
    {
        // Hide the conversation UI
        convoBG.SetActive(false);
    }

    void LoadCustomerData()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("CustomerText");
        customerDB = JsonUtility.FromJson<CustomerDatabase>(jsonData.text);
    }

    public string DisplayCustomerDemands(string customerId)
    {
        foreach (CustomerEntry customerEntry in customerDB.customers)
        {
            if (customerEntry.id == customerId)
            {
                // Assign the retrieved text to the UI element
                conversationText.text = customerEntry.demands; // Update text in the UI
                return customerEntry.demands;
            }
        }
        return "No demands found for the given customer ID."; // Return a default message if no customer is found
    }
}
