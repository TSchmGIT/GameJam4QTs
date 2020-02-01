using System;
using System.Collections.Generic;
using UnityEngine;

public class GameTickManager
{
	#region Properties
	private bool isInCooldown => m_StartCountdownTimer > 0.0f;
	#endregion

	#region Private Variables
	public float m_StartCountdownTimer = 0.0f;
	private float m_GameTimeLeft = 0.0f;
	private int m_Points = 0;

	private List<GameObject> m_PlayerList = new List<GameObject>();
	#endregion
	
	#region Public Methods

	public void ResetGame()
	{
		m_GameTimeLeft = GameManager.Instance.settings.GameTime;
		m_StartCountdownTimer = GameManager.Instance.settings.CountdownTime;
	}

	public void Tick()
	{
		if (isInCooldown)
		{
			bool cooldownHasPassed = UpdateCooldown();
		}
		else
		{
			bool gameHasEnded = UpdateTime();
		}
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

			//GameManager.Instance.EndGame();
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
