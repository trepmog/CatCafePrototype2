using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomerProfile : MonoBehaviour
{
	public string customerId;

	[Header( "Fields Loaded at runtime" )]
    public string customerName;
    public string occupation;
    public string[] desiredTraits;
    public Conversation[] conversations;

	[Header( "Sprites" )]
	public Material spriteNormal;
	public Material spriteHappy;
	public Material spriteSad;

    //public GameManager gameManager;
    private DataLoader dataManager;
    public event Action OnLoaded;

	private BitArray m_traitsDiscovered;

    void Start()
    {
        dataManager = GameManager.Instance.GetComponent<DataLoader>();
        LoadCustomerData();
    }

    // Update is called once per frame
    void Update() { }

    void LoadCustomerData()
    {
        // Since customer object name will have clone in it, use the temporary limit to create the CustomerID
        // Only 1 customer will be loaded. Normally this would be dynamically created like array.length
        int limit = 1;
        // For each loop is here for when a list of customers is loaded
        CustomerEntry[] customers = dataManager.customerDatabase.customers;

        foreach (CustomerEntry customer in customers)
        {
            if (customer.id == customerId && limit < 2) 
            {
                customerName = customer.name;
                occupation = customer.occupation;
                conversations = customer.conversations;
                desiredTraits = customer.desiredTraits;

				m_traitsDiscovered = new BitArray( desiredTraits.Length, false );

                limit++;
                break;
            }
        }
        OnLoaded();
    }

    public string[] GetTraits()
    {
        return desiredTraits;
    }

	public bool IsDesireDiscovered( int index )
	{
		return m_traitsDiscovered[index];
	}

	public void Desire_Discover( int index )
	{
		m_traitsDiscovered.Set( index, true );
	}

	public void Desire_Discover( string trait )
	{
		for ( int i=0; i<desiredTraits.Length; i++ )
		{
			if ( desiredTraits[i] == trait )
			{
				Desire_Discover( i );
				return;
			}
		}

		Debug.LogError( $"The customer does not have the desire \'{trait}\'" );
	}
}
