using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

	#region Unity References

	public GameObject m_TimerWrapper = null;
	public Text m_Text = null;

	#endregion

	#region Unity Callbacks

	private void Update()
	{
		if (GameManager.Instance.State != GameManager.GameState.Game)
		{
			m_TimerWrapper.SetActive(false);
			return;
		}

		m_TimerWrapper.SetActive(true);

		System.TimeSpan timeLeft = System.TimeSpan.FromSeconds(GameManager.Instance.TickManager.timeLeft);
		m_Text.text = timeLeft.ToString(@"mm\:ss");
	}

	#endregion
}
