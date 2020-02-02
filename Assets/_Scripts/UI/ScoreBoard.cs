using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
	#region Unity References

	public GameObject m_ScoreWrapper = null;
	public Text m_ScoreText = null;

	#endregion

	#region Private Variables

	private int m_LastPoints = -1;

	#endregion

	#region Unity Callbacks

	private void Update()
	{
		if (GameManager.Instance.State != GameManager.GameState.Game)
		{
			m_ScoreWrapper.SetActive(false);
			return;
		}

		m_ScoreWrapper.SetActive(true);

		int currentPoints = GameManager.Instance.TickManager.Points;
		if (m_LastPoints == currentPoints)
		{
			return;
		}

		m_LastPoints = currentPoints;

		// React to points update
		m_ScoreText.text = string.Format("{0:n0}", currentPoints);
	}

	#endregion
}
