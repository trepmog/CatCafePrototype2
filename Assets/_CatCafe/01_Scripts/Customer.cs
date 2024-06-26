using UnityEngine;
using UnityEngine.AI;
using Game.Interactions;
using System;

// Script for handling Customer interaction behaviors
public class Customer : MonoBehaviour, IInteractable
{
    private bool playerLeft = false;
    private bool hasCat = false;
    private NavMeshAgent agent;
    public TraitGenie traitGenie; 
    private CustomerIcons customerIcons;
    private bool keyHeld = false;
    public event Action<string, CustomerProfile> OnInteract;
    private CustomerSpawner spawner;
	private CustomerProfile customerProfile;
	private const float AUTO_ICON_DURATION = 2.0f;
	private float m_autoIconShowUntil = 0;
	public MeshRenderer m_spriteRenderer;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        customerIcons = GetComponent<CustomerIcons>();
        GameObject traitGenieObj = GameObject.FindWithTag("TraitGenie");
        traitGenie = traitGenieObj.GetComponent<TraitGenie>();
        traitGenie.OnHold += FindKeyHolding;
        GameObject spawnerObj = GameObject.FindWithTag("Persistent");
        spawner = spawnerObj.GetComponent<CustomerSpawner>();
		customerProfile = GetComponent<CustomerProfile>();
		customerProfile.OnLoaded += Initialize;
    }

	private void OnDestroy()
	{
		customerProfile.OnLoaded -= Initialize;
	}

	private void Initialize()
	{
		SetSpriteNormal();
	}

	public void SetSpriteNormal()
	{
		m_spriteRenderer.material.mainTexture = customerProfile.spriteNormal;
	}

	public void SetSpriteHappy()
	{
		m_spriteRenderer.material.mainTexture = customerProfile.spriteHappy;
	}

	public void SetSpriteSad()
	{
		m_spriteRenderer.material.mainTexture = customerProfile.spriteSad;
	}

	void Update()
    {
		if ( keyHeld && m_autoIconShowUntil != 0 && Time.time > m_autoIconShowUntil )
		{
			m_autoIconShowUntil = 0;
			keyHeld = false;
		}

        // Shows traits if true, hides if false
        ToggleShowTraits(keyHeld);
    }

    public void Interact(GameObject interactor)
    {
        // Stop wandering
        agent.isStopped = true;
        // Trigger showing convo event
        // First customer is hard coded, this will change when we cycle through customers and load data
        OnInteract( customerProfile.customerId, customerProfile );
    }

    public void Resume()
    {
        // Resume wandering
        agent.isStopped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cat"))
        {
            hasCat = true;
            Debug.Log("Cat entered the customer");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerLeft = true;
            Debug.Log("Player left the customer");
        }
    }

    private void FindKeyHolding(bool isHeld)
    {
        // Sets local boolean to event given boolean
        keyHeld = isHeld;
		m_autoIconShowUntil = 0;
	}

	public void IconShowForSeconds()
	{
		if ( !keyHeld )
		{
			keyHeld = true;
			m_autoIconShowUntil = Time.time + AUTO_ICON_DURATION;
		}
	}

    public void ToggleShowTraits(bool toggle)
    {
        customerIcons.ToggleShowIcons(toggle);
    }

    public void SetPickupCat(bool matchResult, GameObject cat)
    {
        //pick up the cat and try to leave if it is a match
        if (matchResult)
        {
            cat.transform.SetParent(this.transform);
            cat.transform.localPosition = new Vector3(0, 0, -0.5f);

            hasCat = true;
            DespawnSelf();
        }

    }

    private void DespawnSelf()
    {
        agent.isStopped = false;
        spawner.MoveToExit(this);
        //Debug.Log("Customer.cs has tried to DespawnSelf");
    }

    public bool GetHasCat()
    {
        return hasCat;
    }
}
