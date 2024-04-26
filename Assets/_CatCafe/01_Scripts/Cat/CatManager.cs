using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    public CatDatabase catDatabase;

    void Awake()
    {
        LoadCatData();
    }

    void LoadCatData()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("CatProfile");
        catDatabase = JsonUtility.FromJson<CatDatabase>(jsonData.text);
        Debug.Log("Cat data loaded");
    }

}
