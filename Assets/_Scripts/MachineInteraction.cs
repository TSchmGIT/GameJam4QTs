using UnityEngine;

public class MachineInteraction : MonoBehaviour
{
	#region Unity References

	public MinigameType m_MinigameType = MinigameType.Screwdriver;

	#endregion

	#region Public Properties

	public bool canStartMinigame => m_CurrentMinigame == null;

	#endregion

	#region Private Variables

	private BaseMinigame m_CurrentMinigame = null;

	#endregion

	#region Public Methods

	public void InitiateMinigame()
	{
		Debug.Log("InitiateMinigame");
		m_CurrentMinigame = GameManager.Instance.MinigameManager.StartMinigame(m_MinigameType);
	}

	#endregion
}
