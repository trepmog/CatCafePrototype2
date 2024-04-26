using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatProfile : MonoBehaviour
{
    public string catName;
    public string gender;
    public string bodyType;
    public string fur;
    public string breed;
    public string[] characteristics;
    public CatManager catManager;
    public GameManager gameManager;

    void Start()
    {
        catManager = GameManager.Instance.GetComponent<CatManager>();
        LoadCatData();
    }


    private void LoadCatData()
    {
        // Load cat data from CatManager which is persistent
        Cat[] cats = catManager.catDatabase.cats;

        // Extract the number part from the GameObject's name, e.g., "Cat1" -> "1"
        string catId = gameObject.name.Substring(3); // Assuming name is formatted as "Cat1", "Cat2", etc.

        foreach (Cat cat in cats)
        {
            if (cat.id == "Cat" + catId) // Match "Cat1", "Cat2", etc., with ids
            {
                catName = cat.name;
                gender = cat.gender;
                bodyType = cat.bodyType;
                fur = cat.fur;
                breed = cat.breed;
                characteristics = cat.character;

                // Test to see if data loaded properly
                UpdateCatProfileDisplay();

                break;
            }
        }
    }

    private void UpdateCatProfileDisplay()
    {
        Debug.Log($"Loaded {catName}'s profile: {breed}");
    }
}
