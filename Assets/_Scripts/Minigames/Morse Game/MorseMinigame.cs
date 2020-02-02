using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MorseMinigame : BaseMinigame
{
	private enum MorseType
	{
		Short,
		Long
	}

	private enum State
	{
		Playback,
		Input
	}

	#region Private Variables

	private GameSettings settings => GameManager.Instance.settings;

	private List<MorseType> m_MorseTypeSequence = new List<MorseType>();
	private State m_State = State.Playback;
	private int m_SequenceIndex = 0;
	private float m_LastPlaybackSoundTimestamp = 0.0f;

	private float? m_LastUserInputTimestamp = null;
	private float m_InputFrameStartTimestamp = 0.0f;

	[SerializeField]
	private MorseSound m_MorseSoundPrefab = null;
	[SerializeField]
	private Transform[] m_SpawnTransform = new Transform[0];
	#endregion

	#region Minigame Methods

	public override void Start()
	{
		GenerateMorseCodeSequence();

		m_State = State.Playback;
		m_SequenceIndex = 0;

		PlayPlaybackSound(m_MorseTypeSequence[m_SequenceIndex]);
	}

	public override MinigameTickResult Tick()
	{
		switch (m_State)
		{
			case State.Playback:
			{
				bool isPlaybackOver = TickPlayback();

				if (isPlaybackOver)
				{
					m_State = State.Input;
					m_SequenceIndex = 0;
					m_InputFrameStartTimestamp = Time.time;
				}
				return MinigameTickResult.ContinueTick;
			}
			case State.Input:
			{
				return TickInput();
			}
			default:
				return MinigameTickResult.Failed;
		}
	}

	public override void Finish()
	{
		
	}

	#endregion

	#region Private Methods

	private void GenerateMorseCodeSequence()
	{
		for (int i = 0; i < GameManager.Instance.settings.AmountSoundsPerSequence; i++)
		{
			MorseType randomMorseType = m_MorseTypeSequence.Count(morseType => morseType == MorseType.Long) < settings.MaxAmountLongSoundsPerSequence ? (MorseType)Random.Range(0, 2) : MorseType.Short;
			m_MorseTypeSequence.Add(randomMorseType);
		}
	}

	private bool TickPlayback()
	{
		Debug.Log("TickPlayback()");
		if (m_SequenceIndex < 0 || m_SequenceIndex >= m_MorseTypeSequence.Count)
		{
			return true;
		}

		MorseType morseType = m_MorseTypeSequence[m_SequenceIndex];
		if (m_LastPlaybackSoundTimestamp + GetPlaybackLengthForMorseType(morseType) <= Time.time)
		{
			m_SequenceIndex++;

			if (m_SequenceIndex >= m_MorseTypeSequence.Count)
			{
				return true;
			}

			// Play next playback sequence
			MorseType nextMorseType = m_MorseTypeSequence[m_SequenceIndex];
			PlayPlaybackSound(nextMorseType);
		}

		return false;

	}

	private MinigameTickResult TickInput()
	{
		Debug.Log("TickInput()"); 
		MorseType morseType = m_MorseTypeSequence[m_SequenceIndex]; 

		KeyCode actionKeyCode = GetKeyCode(InputHelper.Keys.Action);

		if (!m_LastUserInputTimestamp.HasValue)
		{
			if (m_InputFrameStartTimestamp + settings.InputDelayTolerance <= Time.time)
			{
				// FAILED due to inactivity
				return MinigameTickResult.Failed;
			}

			if (Input.GetKeyDown(actionKeyCode))
			{
				m_LastUserInputTimestamp = Time.time;

				PlayInputSound(m_MorseTypeSequence[m_SequenceIndex]);
			}
		}
		else
		{
			if (!Input.GetKey(actionKeyCode))
			{
				float keyPressedDuration = Time.time - m_LastUserInputTimestamp.Value;

				float morseTypeDuration = GetPlaybackLengthForMorseType(morseType);
				float inputTolerance = GetInputToleranceForMorseType(morseType);

				if (keyPressedDuration >= morseTypeDuration - inputTolerance && keyPressedDuration <= morseTypeDuration + inputTolerance)
				{
					// Success
					m_SequenceIndex++;

					if (m_SequenceIndex >= m_MorseTypeSequence.Count)
					{
						return MinigameTickResult.EarlySuccess;
					}

					m_LastUserInputTimestamp = null;
				}
				else
				{
					// Fail
					return MinigameTickResult.Failed;
				}

			}
		}

		return MinigameTickResult.ContinueTick;
	}

	private void PlayPlaybackSound(MorseType morse)
	{
		GameObject spawnGO = new GameObject("MorseGame");
		spawnGO.transform.position = new Vector3(305, 0, 105);

		GameObject gameObject = Object.Instantiate(GameManager.Instance.settings.MorseSoundPrefab);
		gameObject.transform.position = spawnGO.transform.position;
		gameObject.transform.rotation = Quaternion.AngleAxis(90, Vector3.right);

		MorseSound morseSound = gameObject.GetComponent<MorseSound>();
		morseSound.SetMorseDuration(GetPlaybackLengthForMorseType(morse), settings.MorseColorPlayback);

		m_LastPlaybackSoundTimestamp = Time.time;
	}

	private void PlayInputSound(MorseType morseType)
	{
		GameObject spawnGO = new GameObject("MorseGame");
		spawnGO.transform.position = new Vector3(305, 0, 105);

		var gameObject = Object.Instantiate(GameManager.Instance.settings.MorseSoundPrefab);
		gameObject.transform.position = spawnGO.transform.position;
		gameObject.transform.rotation = Quaternion.AngleAxis(90, Vector3.right);

		MorseSound morseSound = gameObject.GetComponent<MorseSound>();
		morseSound.SetMorseDuration(GetPlaybackLengthForMorseType(morseType), settings.MorseColorInput);


	}

	private float GetPlaybackLengthForMorseType(MorseType type)
	{
		switch (type)
		{
			case MorseType.Short:
				return settings.ShortPlaybackTime;
			case MorseType.Long:
				return settings.LongPlaybackTime;
		}

		return 0.0f;
	}

	private float GetInputToleranceForMorseType(MorseType type)
	{
		switch (type)
		{
			default:
			case MorseType.Short:
				return settings.ShortInputTolerance;
			case MorseType.Long:
				return settings.LongInputTolerance;
		}
	}

	private Color GetColorForState(State state)
	{
		switch (state)
		{
			default:
			case State.Playback:
				return settings.MorseColorPlayback;
			case State.Input:
				return settings.MorseColorInput;
		}
	}

	#endregion
}
