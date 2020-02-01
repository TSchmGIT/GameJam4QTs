using System.Collections.Generic;
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

	#region Unity References

	public float m_ShortSequenceMinRange = 0.15f;
	public float m_ShortSequenceMaxRange = 0.25f;
	public float m_ShortSequencePlaybackRange = 0.25f;

	public float m_LongSequenceMinRange = 0.4f;
	public float m_LongSequenceMaxRange = 0.5f;
	public float m_LongSequencePlaybackRange = 0.5f;

	public int m_MaxAmountLongSequences = 2;
	public int m_AmountOfSoundsPerSequence = 3;

	#endregion

	#region Private Variables

	private List<MorseType> m_MorseTypeSequence = new List<MorseType>();
	private State m_State = State.Playback;
	private int m_SequenceIndex = 0;
	private float m_LastPlaybackSoundTimestamp = 0.0f;

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
	}

	public override MinigameTickResult Tick()
	{
		MinigameTickResult result = MinigameTickResult.ContinueTick;

		switch (m_State)
		{
			case State.Playback:
			{
				bool isPlaybackOver = TickPlayback();

				if (isPlaybackOver)
				{
				m_State = State.Input;
				}
				break;
			}
			case State.Input:
			{
				result = TickInput();
				break;
			}
		}

		return result;
	}

	public override void Finish()
	{
		throw new System.NotImplementedException();
	}

	#endregion

	#region Private Methods

	private void GenerateMorseCodeSequence()
	{
		for (int i = 0; i < m_AmountOfSoundsPerSequence; i++)
		{
			MorseType randomMorseType = (MorseType)Random.Range(0, 2);
			m_MorseTypeSequence.Add(randomMorseType);
		}
	}

	private bool TickPlayback()
	{
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
				// Playback has ended
				return true;
			}
			else
			{
				// Play next playback sequence
				MorseType nextMorseType = m_MorseTypeSequence[m_SequenceIndex];

				PlayPlaybackSound(nextMorseType);

				m_LastPlaybackSoundTimestamp = Time.time;
			}
		}

		return false;

	}

	private MinigameTickResult TickInput()
	{
		return MinigameTickResult.ContinueTick;
	}

	private void PlayPlaybackSound(MorseType morse)
	{
		GameObject spawnGO = new GameObject("MorseGame");
		spawnGO.transform.position = new Vector3(100, 0, 0);

	}

	private void PlayInputSound()
	{

	}

	private float GetPlaybackLengthForMorseType(MorseType type)
	{
		switch (type)
		{
			case MorseType.Short:
				return m_ShortSequencePlaybackRange;
			case MorseType.Long:
				return m_LongSequencePlaybackRange;
		}

		return 0.0f;
	}

	#endregion
}
