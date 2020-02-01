using System.Collections.Generic;
using UnityEngine;

public class GameTickManager : MonoBehaviour
{
	#region Properties
	public float timeLeft => m_GameTimeLeft;
	public float timePassed => GameManager.Instance.settings.GameTime - m_GameTimeLeft;

	public bool isInCountdown => m_StartCountdownTimer > 0.0f;
	public float countdownTime => m_StartCountdownTimer;

	public event System.Action OnCountdownStarted = null;
	#endregion

	#region Private Variables
	private float m_StartCountdownTimer = 0.0f;
	private float m_GameTimeLeft = 0.0f;
	private int m_Points = 0;

	private List<GameObject> m_PlayerList = new List<GameObject>();
	#endregion

	#region Unity Callbacks
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	public void Update()
	{
		if (GameManager.Instance.State != GameManager.GameState.Game)
		{
			return;
		}

		if (isInCountdown)
		{
			bool cooldownHasPassed = UpdateCooldown();
		}
		else
		{
			bool gameHasEnded = UpdateTime();
		}
	}
	#endregion

	#region Public Methods

	public void ResetGame()
	{
		m_GameTimeLeft = GameManager.Instance.settings.GameTime;
		m_StartCountdownTimer = GameManager.Instance.settings.CountdownTime;

		OnCountdownStarted?.Invoke();
	}

	private bool UpdateCooldown()
	{
		if (m_StartCountdownTimer >= 0.0f)
		{
			m_StartCountdownTimer -= Time.deltaTime;
			return false;
		}
		else
		{
			m_StartCountdownTimer = 0.0f;
			return true;
		}
	}

	public void AddPoints(int points)
	{
		m_Points += points;
	}

	public void Init()
	{

	}
	#endregion

	#region Private Methods
	private bool UpdateTime()
	{
		// Check if the game time has run out
		if (m_GameTimeLeft >= 0)
		{
			m_GameTimeLeft -= Time.deltaTime;
			return false;
		}
		else
		{
			m_GameTimeLeft = 0.0f;

			GameManager.Instance.EndGame();
			return true;
		}
	}

	private void StartGame()
	{
		ActivatePlayerList();
	}

	private void ActivatePlayerList()
	{

	}
	#endregion
}
