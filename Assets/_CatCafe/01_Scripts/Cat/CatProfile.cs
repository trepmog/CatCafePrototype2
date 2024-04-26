using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CatProfile : MonoBehaviour
{
    public string catName;
    public string gender;
    public string bodyType;
    public string fur;
    public string breed;
    public string[] traits;
    public CatManager catManager;
    public GameManager gameManager;
    public event Action OnLoaded;

    void Start()
    {
        
        catManager = GameManager.Instance.GetComponent<CatManager>();
        LoadCatData();
    }


    private void LoadCatData()
    {
        // Temporary variable because there are only 3 cats. Ordinarily you would get a list of all cats available
        int limit = 3;
        // Load cat data from CatManager which is persistent
        Cat[] cats = catManager.catDatabase.cats;

        // Extract the number part from the GameObject's name, e.g., "Cat1" -> "1"
        string catId = gameObject.name.Substring(3); // Assuming name is formatted as "Cat1", "Cat2", etc.

        foreach (Cat cat in cats)
        {
            if (cat.id == "Cat" + catId && limit > 0) // Match "Cat1", "Cat2", etc., with ids
            {
                catName = cat.name;
                gender = cat.gender;
                bodyType = cat.bodyType;
                fur = cat.fur;
                breed = cat.breed;
                traits = cat.character;

                limit--;
                break;
            }
        }
        OnLoaded();
    }

    public string[] GetTraits()
    {
        Debug.Log("Cat Profile says traits are: " + traits);
        return traits;
    }

}
