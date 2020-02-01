using System.Collections.Generic;
using UnityEngine;

public struct ItemRuntimeData
{

}

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
	#region Private Members
	private List<ConveyorBelt> m_ConveyorBeltList = new List<ConveyorBelt>();
	private Rigidbody m_Rigidbody = null;

	//private ItemData m_ItemData;
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

		m_ConveyorBeltList.Add(conveyorBelt);
	}

	private void OnCollisionExit(Collision collision)
	{
		ConveyorBelt conveyorBelt = collision.gameObject.GetComponent<ConveyorBelt>();

		m_ConveyorBeltList.Remove(conveyorBelt);
	}

	private void OnGUI()
	{

	}

	#endregion

	#region Public Methods
	
	public void SpawnItem()
	{

	}

	#endregion

	#region Private Methods

	private void UpdateConveyorBelt()
	{
		/*foreach (ConveyorBelt conveyorBelt in m_ConveyorBeltList)
		{
			transform.position += conveyorBelt.transform.forward * conveyorBelt.speed * Time.deltaTime;
		}*/
	}

	#endregion
}
