using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreenStartButton : MonoBehaviour
{
	[SerializeField]
	private GameObject m_TutorialScreenGO = null;

	private void Awake()
	{
		GameManager.Instance.TutorialScreenGO = m_TutorialScreenGO;
	}

	public void StartGameAfterTutorial()
	{
		GameManager.Instance.StartGame();

		m_TutorialScreenGO.SetActive(false);
	}
}
