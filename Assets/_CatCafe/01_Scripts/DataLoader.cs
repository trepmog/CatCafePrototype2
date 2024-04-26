using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public CatDatabase catDatabase;
    public CustomerDatabase customerDatabase;

    void Awake()
    {
        LoadCatData();
    }

    void LoadCatData()
    {
        // Load cat profile data
        TextAsset catData = Resources.Load<TextAsset>("CatProfile");
        catDatabase = JsonUtility.FromJson<CatDatabase>(catData.text);
        // Load customer profile data
        TextAsset customerData = Resources.Load<TextAsset>("CustomerText");
        customerDatabase = JsonUtility.FromJson<CustomerDatabase>(customerData.text);
        Debug.Log("Data loaded");
    }

}
