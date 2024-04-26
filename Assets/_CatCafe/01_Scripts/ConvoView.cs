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
    public TextMeshProUGUI nameText;
    private CustomerDatabase customerDB;
    private int currentTextIndex = 0;
    private string customerId;
    private bool isConvoActive = false;
    private bool isFirstInteract = true;

    void Start()
    {
        LoadCustomerData();
        customer.OnInteract += ShowConvoUI;
        convoBG = transform.Find("ConvoBG").gameObject;
        convoBG.SetActive(false);
    }

    void Update()
    {
        // Handle key press to cycle through the conversation
        if (isConvoActive && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("Tried to show next piece of dialogue");
            NextConvoText();
        }
    }

    private void OnDisable()
    {
        customer.OnInteract -= ShowConvoUI;
    }

    public void ShowConvoUI(string givenId)
    {
        customerId = givenId;
        // Shows convo BG
        SetActiveConvo(true);
        DisplayCustomerDemands();
    }

    private void SetActiveConvo(bool isActive)
    {
        isConvoActive = isActive;
        convoBG.SetActive(isActive);
        // Resets if setting convo to inactive
        if (!isActive) {
            currentTextIndex = 0;
            isFirstInteract = true;
        }
    }

    void LoadCustomerData()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("CustomerText");
        customerDB = JsonUtility.FromJson<CustomerDatabase>(jsonData.text);
    }

    public void DisplayCustomerDemands()
    {
        foreach (CustomerEntry customerEntry in customerDB.customers)
        {
            if (customerEntry.id == customerId)
            {
                if (currentTextIndex < customerEntry.conversationTexts.Length)
                {
                    // Cycle to the next piece of dialogue
                    var convoEntry = customerEntry.conversationTexts[currentTextIndex];
                    nameText.text = convoEntry.speaker + ":";
                    conversationText.text = convoEntry.text;
                }
                else
                {
                    SetActiveConvo(false); // End conversation if we run out of texts
                }
            }
        }
    }

    public void NextConvoText()
    {
        if (isConvoActive)
        {
                // Displays the first piece of dialogue on first interaction
            if (isFirstInteract) {
                DisplayCustomerDemands();
                isFirstInteract = false;
            }
            else
            {
                // Cycles to next piece of dialogue if not the first interaction
                currentTextIndex++;
                DisplayCustomerDemands();
            }
            
            
        }
    }
}
