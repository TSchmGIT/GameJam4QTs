using UnityEngine;

public class TutorialScreenStartButton : MonoBehaviour
{
	[SerializeField]
	private GameObject m_TutorialScreenGO = null;

	private void Awake()
	{
		GameManager.Instance.TutorialScreenGO = m_TutorialScreenGO;
	}

	private void OnDestroy()
	{
		if (GameManager.Instance.TutorialScreenGO == m_TutorialScreenGO)
		{
			GameManager.Instance.TutorialScreenGO = null;
		}
	}

	public void StartGameAfterTutorial()
	{
		GameManager.Instance.StartGame();

		m_TutorialScreenGO.SetActive(false);
	}
}
