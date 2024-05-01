using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CatTraitDiscoveryPrototype : MonoBehaviour
{
	public TextMeshProUGUI m_text;

	const float MINIGAME_DURATION = 3.0f;
	enum State
	{
		PLAYING_MINIGAME,
		DISPLAY_RESULT,
	}
	private State m_state;
	private float m_stateStartTime;

	void OnEnable()
	{
		m_state = State.PLAYING_MINIGAME;
		m_stateStartTime = Time.time;
		m_text.text = "Playing minigame.";
	}

	// Update is called once per frame
	void Update()
	{
		if ( m_state == State.PLAYING_MINIGAME )
		{
			float elapsedTime = Time.time - m_stateStartTime;
			if ( elapsedTime > MINIGAME_DURATION )
			{
				m_state = State.DISPLAY_RESULT;
				DoResult();
				return;
			}

			string message = "Playing minigame.";
			int secondsElapsed = Mathf.FloorToInt( elapsedTime );
			for ( int i = 0; i < secondsElapsed; i++ )
				message += '.';
			m_text.text = message;
		}
		else if ( m_state == State.DISPLAY_RESULT )
		{
			if ( Input.GetKeyDown( KeyCode.E ) )
				this.gameObject.SetActive( false );
		}
	}

	private void DoResult()
	{
		bool success = UnityEngine.Random.Range( 0, 2 ) == 1;

		if ( success )
		{
			m_text.text = "You learn the cat is ______";
		}
		else
		{
			m_text.text = "No trait was learned. Try again.";
		}
	}
}
