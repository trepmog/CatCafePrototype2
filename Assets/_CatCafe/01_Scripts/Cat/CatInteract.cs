using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interactions;
using System;

public class CatInteract : MonoBehaviour, IInteractable
{
    private bool isCarried = false;
    private float catY = 1.43f;
    private GameObject player;
    private CatIcons catIcons;
    public TraitGenie traitGenie;
    private GameObject traitGenieObj;
    private bool keyHeld;

    // Start is called before the first frame update
    void Start() {
        catIcons = GetComponent<CatIcons>();
        // Subscribe to Trait Genie's event
        traitGenie.OnHold += FindKeyHolding;
     }

    void Update() {
        // Shows traits if true, hides if false
        ToggleShowTraits(keyHeld);
        
    }

    void OnDisable()
    {
        traitGenie.OnHold -= FindKeyHolding;
    }

    public void Interact(GameObject interactor)
    {
        player = interactor;
        if (isCarried)
        {
            // Put down the cat
            PutDown(player);
        }
        else
        {
            // Pick up the cat
            PickUp(player);
        }
    }

    private void FindKeyHolding(bool isHeld)
    {
        // Sets local boolean to event given boolean
        keyHeld = isHeld;
    }

    private void PickUp(GameObject player)
    {
        Debug.Log("Cat picked up!");
        // Creates a position that is "vertically" higher than it currently is (actually an X value since the game is 2.5D)
        var playerHand = new Vector3(
            gameObject.transform.position.x + 0.5f,
            gameObject.transform.position.y + 1f,
            gameObject.transform.position.z - 1.5f
        );
        // Moves the cat to the vertically higher position
        this.transform.position = playerHand;
        // Make the cat move with the player;
        this.transform.SetParent(player.transform);

        isCarried = true;
    }

    private void PutDown(GameObject player)
    {
        Debug.Log("Cat put down!");
        this.transform.SetParent(null); // Remove the cat from being a child of player
        // creates a position that is offset from the player to appear over the hand and assigns it to the cat
        this.transform.position = new Vector3(
            player.transform.position.x + 0.5f,
            catY,
            player.transform.position.z - 1.5f
        );
        isCarried = false;
    }


    public void ToggleShowTraits(bool toggle)
    {
        catIcons.ToggleShowIcons(toggle);
    }
}
