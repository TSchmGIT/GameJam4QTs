using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MorseSound : MonoBehaviour
{
	#region Unity References

	[SerializeField]
	private MeshRenderer m_MeshRenderer = null;

	public float m_ScaleSpeed = 1.0f;

	private float m_MorseDuration = 0.0f;
	private float? m_MorseStartpoint = 0.0f;
	#endregion

	#region Unity Callback

	private void Awake()
	{
		m_MorseStartpoint = null;
	}

	private void Update()
	{
		if (!m_MorseStartpoint.HasValue)
		{
			return;
		}

		if (m_MorseStartpoint.Value + m_MorseDuration <= Time.time)
		{
			Destroy(gameObject);
			return;
		}

		// Update scale
		transform.localScale += Vector3.one * m_ScaleSpeed * Time.deltaTime;

		// Update fade
		float fadefactor = 1.0f - Mathf.Clamp01((Time.time - m_MorseStartpoint.Value) / m_MorseDuration);
		Color currentColor = m_MeshRenderer.material.GetColor("_Color");
		currentColor.a = fadefactor;
		m_MeshRenderer.material.SetColor("_Color", currentColor);
	}
	
	#endregion

	#region Public Methods

	public void SetMorseDuration(float duration)
	{
		m_MorseStartpoint = Time.time;
		m_MorseDuration = duration;
	}

	#endregion
}
