using System.Collections.Generic;
using UnityEngine;

public struct ItemRuntimeData
{
	public List<MinigameType>   MachineOrderList;
    public int                  MachinesNeededTotal;
}

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
	#region Private Members
	private List<ConveyorBelt> m_ConveyorBeltList   = new List<ConveyorBelt>();
	private Rigidbody m_Rigidbody                   = null;
    private MeshRenderer m_MeshRenderer             = null;
    private MeshFilter m_MeshFilter;
    private ItemTier m_ItemTier;

	private ItemRuntimeData m_RuntimeData;
	
	private MinigameType m_MinigameType;
	#endregion

	#region Unity Callback

	private void Awake()
	{
		m_Rigidbody     = GetComponent<Rigidbody>();
        
        m_MeshRenderer  = GetComponent<MeshRenderer>();
        m_MeshFilter    = GetComponent<MeshFilter>();
	}

	private void Update()
	{
		UpdateConveyorBelt();
	}

	private void OnCollisionEnter(Collision collision)
	{
		ConveyorBelt conveyorBelt = collision.gameObject.GetComponent<ConveyorBelt>();
		if (conveyorBelt != null)
		{
			m_ConveyorBeltList.Add(conveyorBelt);
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		ConveyorBelt conveyorBelt = collision.gameObject.GetComponent<ConveyorBelt>();

		if (conveyorBelt != null)
		{
			m_ConveyorBeltList.Remove(conveyorBelt);
		}
	}

	private void OnGUI()
	{

	}

	#endregion

	#region Private Methods

	private void UpdateConveyorBelt()
	{
        Vector3 oldPosition = transform.position;
		foreach (ConveyorBelt conveyorBelt in m_ConveyorBeltList)
		{
            
			transform.position += conveyorBelt.transform.forward * conveyorBelt.speed * Time.deltaTime;
		}

        if (transform.position != oldPosition)
        {
            transform.rotation = Quaternion.LookRotation(-transform.position + oldPosition, Vector3.up);

        }

	}

	public void SetRuntimeData(ItemRuntimeData runtimeData)
	{
		m_RuntimeData = runtimeData;

        UpdateVisualization();
	}

    public ItemRuntimeData GetRuntimeData()
    {
        return m_RuntimeData;
    }

    public void SetTier(ItemTier itemTier)
    {
        m_ItemTier = itemTier;
    }

    public bool IsEligbleForMachine(MachineInteraction machine)
    {
        if (m_RuntimeData.MachineOrderList.Count == 0)
        {
            return false;
        }

        return m_RuntimeData.MachineOrderList[0] == machine.m_MinigameType;
    }

    public void OnSuccessFullMachineInteraction()
    {
        m_RuntimeData.MachineOrderList.RemoveAt(0);

        UpdateVisualization();
    }


    void UpdateVisualization()
    {
        if (m_RuntimeData.MachineOrderList.Count == 0)
        {
            m_MeshFilter.mesh               = GameManager.Instance.settings.ItemMeshNoStageLeft;
            m_MeshRenderer.material.color   = GameManager.Instance.settings.ItemColorNoStageLeft;

            return;
        }

        m_MeshFilter.mesh                   = GameManager.Instance.settings.ItemMeshMinigames[(int) m_RuntimeData.MachineOrderList[0]];
        m_MeshRenderer.material.color       = GameManager.Instance.settings.ItemColorMinigames[(int) m_RuntimeData.MachineOrderList[0]];
    }

    public ItemTier GetTier()
    {
        return m_ItemTier;
    }   
	#endregion
}
