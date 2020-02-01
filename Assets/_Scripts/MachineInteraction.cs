using UnityEngine;

public class MachineInteraction : MonoBehaviour
{
	#region Unity References

	public MinigameType m_MinigameType = MinigameType.Screwdriver;

	#endregion

	#region Public Properties

	public bool canStartMinigame => false;

	#endregion

	#region Private Variables

	#endregion

	#region Public Methods

	public void InitiateMinigame(int playerID)
	{
		Debug.Log("InitiateMinigame");
		GameManager.Instance.MinigameManager.StartMinigame(GetComponent<MinigameDisplayComponent>(), m_MinigameType, playerID, OnMachineInteractionFinished);
	}

    void OnMachineInteractionFinished(MinigameTickResult tickResult)
    {
        Debug.Log("I am a machine, my name is " + gameObject.name + " and I finished a minigame with result " + tickResult.ToString());
    }

	#endregion
}
