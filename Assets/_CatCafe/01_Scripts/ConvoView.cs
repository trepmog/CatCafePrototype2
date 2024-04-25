using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvoView : MonoBehaviour
{
    public Customer customer;
    private GameObject convoBG;

    void Start()
    {
        customer.OnInteract += ShowConvoUI;
        convoBG = transform.Find("ConvoBG").gameObject;
    }

    private void OnDisable()
    {
        customer.OnInteract -= ShowConvoUI;
    }


    public void ShowConvoUI(string message)
    {
        // Display conversation UI
        Debug.Log("Convo UI shown: " + message);
        convoBG.SetActive(!convoBG.activeSelf);

    }
}
