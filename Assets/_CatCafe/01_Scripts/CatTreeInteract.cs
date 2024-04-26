using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interactions;
using System;

public class CatTreeInteract : MonoBehaviour, IInteractable
{
    public event Action<GameObject> OnCatPlacement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(GameObject interactor)
    {
        Debug.Log("Calling event from Cat Tree");
        OnCatPlacement(interactor);
    }
}
