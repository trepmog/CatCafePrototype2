using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConvoView : MonoBehaviour
{
	private static ConvoView s_instance;

	public Customer customer;
	private CustomerProfile customerProfile;
	private GameObject convoBG;
	private GameObject matchSuccess;
	private GameObject matchFail;
	private GameObject reward;
	private GameObject thankYou;
	private GameObject continueText;
	public TextMeshProUGUI conversationText;
	public TextMeshProUGUI nameText;
	private CustomerDatabase customerDB;
	public MatchMaker matchMaker;
	private int currentConversationIndex = 0;
	private int currentTextIndex = 0;
	private bool isConvoActive = false;
	private bool isFirstInteract = true;
	private const float delayBeforeHide = 2.0f;
	private float timer;
	public CatTraitDiscoveryPrototype catTraitDiscovery;

	private void Awake()
	{
		s_instance = this;
	}

	void Start()
	{
		LoadCustomerData();
		customer.OnInteract += ShowConvoUI;
		matchMaker.OnMatchResult += ShowMatchResult;
		convoBG = transform.Find( "ConvoBG" ).gameObject;
		matchSuccess = transform.Find( "MatchSuccess" ).gameObject;
		matchFail = transform.Find( "MatchFailText" ).gameObject;
		thankYou = transform.Find( "ThankYou" ).gameObject;
		reward = transform.Find( "Reward" ).gameObject;
		continueText = transform.Find( "ContinueText" ).gameObject;
		convoBG.SetActive( false );
	}

	void Update()
	{
		// Handle key press to cycle through the conversation
		if ( isConvoActive && Input.GetKeyDown( KeyCode.E ) )
		{
			//Debug.Log("Tried to show next piece of dialogue");
			NextConvoText();
		}

		if ( matchSuccess.activeSelf || matchFail.activeSelf )
		{
			if ( timer > 0 )
			{
				timer -= Time.deltaTime;
			}
			else
			{
				HideMatchResult();
			}
		}
	}

	private void OnDisable()
	{
		customer.OnInteract -= ShowConvoUI;
	}

	public void ShowConvoUI( string givenId, CustomerProfile _customerProfile )
	{
		customerProfile = _customerProfile;
		customer = _customerProfile.GetComponent<Customer>();
		// Shows convo BG
		SetActiveConvo( true );
		DisplayCustomerDemands();
	}

	public void ShowMatchResult( bool result, Customer _customer )
	{
		customer = _customer;

		//Debug.Log("Match Success is: " + matchSuccess.activeSelf);
		if ( result )
		{
			matchSuccess.SetActive( true );
			thankYou.SetActive( true );
			customer.SetSpriteHappy();
			//continueText.SetActive( true );
			// This is to prevent the Update from hiding the results right away
			timer = delayBeforeHide;
		}
		else
		{
			Debug.Log( "It thinks it's a fail" );
			matchFail.SetActive( true );
			customer.SetSpriteSad();
			//continueText.SetActive( true );
			// This is to prevent the Update from hiding the results right away
			timer = delayBeforeHide;
		}
	}

	public void HideMatchResult()
	{
		matchSuccess.SetActive( false );
		thankYou.SetActive( false );
		matchFail.SetActive( false );
		continueText.SetActive( false );
		customer.SetSpriteNormal();
	}

	private void SetActiveConvo( bool isActive )
	{
		isConvoActive = isActive;
		convoBG.SetActive( isActive );
		// Resets if setting convo to inactive
		if ( !isActive )
		{
			currentTextIndex = 0;
			isFirstInteract = true;
		}
	}

	void LoadCustomerData()
	{
		TextAsset jsonData = Resources.Load<TextAsset>( "CustomerText" );
		customerDB = JsonUtility.FromJson<CustomerDatabase>( jsonData.text );
	}

	public void DisplayCustomerDemands()
	{
		if ( currentTextIndex < customerProfile.conversations[currentConversationIndex].conversationTexts.Length )
		{
			// Cycle to the next piece of dialogue
			var convoEntry = customerProfile.conversations[currentConversationIndex].conversationTexts[currentTextIndex];
			nameText.text = convoEntry.speaker + ":";
			conversationText.text = convoEntry.text;
		}
		else
		{
					
			string desiredTrait = customerProfile.conversations[currentConversationIndex].desiredTrait;
			if ( !String.IsNullOrEmpty( desiredTrait ) )
			{
				customerProfile.Desire_Discover( desiredTrait );
				customer.IconShowForSeconds();
			}

			currentConversationIndex = Math.Min( currentConversationIndex + 1, customerProfile.conversations.Length-1 );
			SetActiveConvo( false ); // End conversation if we run out of texts
		}
	}

	public void NextConvoText()
	{
		if ( isConvoActive )
		{
			// Displays the first piece of dialogue on first interaction
			if ( isFirstInteract )
			{
				DisplayCustomerDemands();
				isFirstInteract = false;
			}
			else
			{
				// Cycles to next piece of dialogue if not the first interaction
				currentTextIndex++;
				DisplayCustomerDemands();
			}
		}
	}

	public static void CatDiscoveryStart( CatProfile catProfile )
	{
		s_instance.catTraitDiscovery.m_catProfile = catProfile;
		s_instance.catTraitDiscovery.gameObject.SetActive( true );
	}
}
