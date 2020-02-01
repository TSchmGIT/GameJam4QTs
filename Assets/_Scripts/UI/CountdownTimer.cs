using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CountdownTimer : MonoBehaviour
{
	#region Unity Callbacks
	[SerializeField]
	private int m_MaximumFontSize = 75;

	private float m_CountdownResetTimestamp = 0.0f;

	#endregion

	#region Unity Callbacks

	private void Awake()
	{
		m_Text = GetComponent<Text>();
	}

	private void Start()
	{
		GameManager.Instance.TickManager.OnCountdownStarted += TickManager_OnCountdownStarted;
	}

	private void OnDestroy()
	{
		GameManager.Instance.TickManager.OnCountdownStarted -= TickManager_OnCountdownStarted;
	}

	private void Update()
	{
		float fontFactor = Mathf.PingPong((Time.time - m_CountdownResetTimestamp) * 2.0f, 1.0f);

		m_Text.fontSize = (int)(m_MaximumFontSize * fontFactor);
		m_Text.text = Mathf.Ceil(GameManager.Instance.TickManager.countdownTime).ToString();

		if (!GameManager.Instance.TickManager.isInCountdown)
		{
			gameObject.SetActive(false);
		}
	}
	#endregion

	#region Private Methods

	private void TickManager_OnCountdownStarted()
	{
		ResetCountdown();
	}

	private void ResetCountdown()
	{
		m_Text.fontSize = 0;
		m_CountdownResetTimestamp = Time.time;

		gameObject.SetActive(true);
	}

	#endregion

	#region Private Members
	private Text m_Text = null;
	#endregion
}
