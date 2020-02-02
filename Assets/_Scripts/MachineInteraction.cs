using UnityEngine;

public class MachineInteraction : MonoBehaviour
{
	#region Unity References

	public MinigameType m_MinigameType = MinigameType.Screwdriver;

	#endregion

	#region Public Properties
	private bool m_CurrentlyPlayingMinigame = false;
	public bool CanStartMinigame => !m_CurrentlyPlayingMinigame && (m_LastEndedTimestamp + m_MachineBlockedAfterGame) <= Time.time;

	public float m_MachineBlockedAfterGame = 0.1f;

	#endregion

	#region Private Variabless

	private float m_LastEndedTimestamp = 0.0f;

    private Item m_InitiatedWithItem;

	#endregion

	#region Public Methods
    
	public void InitiateMinigame(int playerID, Player1Controller controller, Item initiatedWithItem)
    {
		Debug.Log("InitiateMinigame");
		GameManager.Instance.MinigameManager.StartMinigame(GetComponent<MinigameDisplayComponent>(), m_MinigameType, playerID, (tickResult) => OnMachineInteractionFinished(tickResult, controller));
		m_CurrentlyPlayingMinigame = true;
        controller.DisableControls();
        m_InitiatedWithItem = initiatedWithItem;
	}

	private void OnMachineInteractionFinished(MinigameTickResult tickResult, Player1Controller controller)
	{
		if (gameObject == null)
		{
			return;
		}

		Debug.Log("I am a machine, my name is " + gameObject.name + " and I finished a minigame with result " + tickResult.ToString());
		m_CurrentlyPlayingMinigame = false;
		m_LastEndedTimestamp = Time.time;
        controller.EnableControls();
        
        if (tickResult == MinigameTickResult.EarlySuccess)
        {
            m_InitiatedWithItem.OnSuccessFullMachineInteraction();
        }
	}

	#endregion
}
