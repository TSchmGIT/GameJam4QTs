using System;
using UnityEngine;
using UnityEngine.UI;

public class EndscreenScript : MonoBehaviour
{
	#region Unity References

	[SerializeField]
	private GameObject m_EndScreenWrapper = null;

	[SerializeField]
	private Text m_ScoreText = null;

	[SerializeField]
	private Button m_MainMenuButton = null;
	[SerializeField]
	private Button m_PlayAgainButon = null;
	#endregion

	#region Unity Callbacks


	private void Start()
	{
		m_MainMenuButton.onClick.AddListener(GameManager.Instance.LoadMainMenu);
		m_PlayAgainButon.onClick.AddListener(GameManager.Instance.LoadGame);

		GameManager.Instance.EndScreenGO = gameObject;

		GameManager.Instance.TickManager.OnGameEnded += TickManager_OnGameEnded;
	}

	private void OnDestroy()
	{
		if (GameManager.Instance.EndScreenGO == this)
		{
			GameManager.Instance.EndScreenGO = null;
		}

		GameManager.Instance.TickManager.OnGameEnded -= TickManager_OnGameEnded;
	}

	private void Update()
	{
		if (GameManager.Instance.State != GameManager.GameState.PostGame)
		{
			m_EndScreenWrapper.SetActive(false);
			return;
		}

		m_EndScreenWrapper.SetActive(true);
	}

	#endregion

	#region Private Methods

	private void TickManager_OnGameEnded()
	{
		FillHighscoreList();
	}

	private void FillHighscoreList()
	{
		m_ScoreText.text = GameManager.Instance.TickManager.Points.ToString();
	}

	#endregion
}
