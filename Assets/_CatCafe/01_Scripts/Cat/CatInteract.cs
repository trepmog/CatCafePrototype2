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
    public GameObject catTreeObj;
    public CatTreeInteract catTree;
    public bool isOnTree = false;
    private bool keyHeld;
    private bool isNearTree = false;
	private GameObject nearPlayArea = null;
    private bool successfulMatch = false;

    void Start()
    {
        catIcons = GetComponent<CatIcons>();
        catTreeObj = GameObject.FindWithTag("CatTree");
        // Subscribe to Trait Genie's event
        traitGenie.OnHold += FindKeyHolding;
    }

    void Update()
    {
        // Shows traits if true, hides if false
        ToggleShowTraits(keyHeld);
    }

    void OnDisable()
    {
        // Unsubscribe to events if this object is ever disabled
        traitGenie.OnHold -= FindKeyHolding;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CatTree"))
        {
            isNearTree = true;
        }
		else if ( other.CompareTag("PlayArea"))
		{
			//Debug.Log("Entered Play Area " + other.gameObject.name );
			nearPlayArea = other.gameObject;
		}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CatTree"))
        {
            isNearTree = false;
        }
		else if ( other.CompareTag( "PlayArea" ) )
		{
			//Debug.Log( "Exited Play Area " + other.gameObject.name );
			nearPlayArea = null;
		}
	}

    public void Interact(GameObject interactor)
    {
        player = interactor;

        //Stop the player from interacting with a cat that's already been matched to a customer
        if (!successfulMatch)
        {
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
    }

    private void FindKeyHolding(bool isHeld)
    {
        // Sets local boolean to event given boolean
        keyHeld = isHeld;
    }

    private void PickUp(GameObject player)
    {
        Debug.Log("Cat picked up!");

        this.transform.SetParent(player.transform);
		this.transform.localPosition = new Vector3( 0, 0, -0.5f );

        isCarried = true;
        // If this cat is on the cat tree,
        // Set cat state to not on tree + set cat tree state to not occupied
        if(isOnTree) 
        {
            catTree.SetIsOccupied(false);
            catTree.residingCat = null;
            isOnTree = false;
        }
    }

    private void PutDown(GameObject player)
    {
		// Checks if Player is trying to put the cat on the Cat Tree first
		if ( isNearTree )
		{
			PlaceOnTree( player );
			isCarried = false;
		}
		else if ( nearPlayArea )
		{
			PlaceOnPlayArea( player );
			isCarried = false;
		}
		else
		{
			Debug.Log( "Cat put down!" );
			this.transform.SetParent( null ); // Remove the cat from being a child of player
											  // creates a position that is offset from the player to appear over the hand and assigns it to the cat
			this.transform.position = new Vector3(
				player.transform.position.x + 0.5f,
				catY,
				player.transform.position.z - 1.5f
			);
			isCarried = false;

		}
	}

    public void ToggleShowTraits(bool toggle)
    {
        catIcons.ToggleShowIcons(toggle);
    }

    public void PlaceOnTree(GameObject player)
    {
        if (isCarried)
        {
            var treePos = catTreeObj.transform.position;
            this.transform.SetParent(null);

            this.transform.position = new Vector3(treePos.x, treePos.y + 2.0f, treePos.z + 2.5f);
            isCarried = false;
            isOnTree = true;
            catTree.SetIsOccupied(true);
            catTree.residingCat = this.gameObject;

			MatchMaker.MakeMatch( this.gameObject );
        }
        else
        {
            Debug.Log("Cannot place on tree, cat is not being carried");
        }
    }
	
	public void PlaceOnPlayArea( GameObject player )
	{
		if ( isCarried )
		{
			var playAreaPos = nearPlayArea.transform.position;
			this.transform.SetParent( null );

			this.transform.position = new Vector3( playAreaPos.x, playAreaPos.y + 2.0f, playAreaPos.z + 0.0f );
			isCarried = false;

			ConvoView.CatDiscoveryStart();
		}
		else
		{
			Debug.Log( "Cannot place cat; cat is not being carried" );
		}
	}
    public void SetSuccessfulMatch(bool isMatch)
    {
        successfulMatch = isMatch;
    }
}
