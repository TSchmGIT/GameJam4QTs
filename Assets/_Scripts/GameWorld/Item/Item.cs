using System.Collections.Generic;
using UnityEngine;

public struct ItemRuntimeData
{
	public List<int> MachineOrderList;
}

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
	#region Private Members
	private List<ConveyorBelt> m_ConveyorBeltList = new List<ConveyorBelt>();
	private Rigidbody m_Rigidbody = null;

	private ItemRuntimeData m_RuntimeData;
	#endregion

	#region Unity Callback

	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
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
		foreach (ConveyorBelt conveyorBelt in m_ConveyorBeltList)
		{
			transform.position += conveyorBelt.transform.forward * conveyorBelt.speed * Time.deltaTime;
		}
	}

	public void SetRuntimeData(ItemRuntimeData runtimeData)
	{
		m_RuntimeData = runtimeData;
	}

	#endregion
}
