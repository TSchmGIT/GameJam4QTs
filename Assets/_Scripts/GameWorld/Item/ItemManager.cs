using System.Collections.Generic;
using UnityEngine;

public enum ItemTier
{
	Tier1,
	Tier2,
	Tier3,
	Tier4
}

public class ItemManager : MonoBehaviour
{
	#region Unity References

	public ItemSettings m_ItemTier1;
	public ItemSettings m_ItemTier2;
	public ItemSettings m_ItemTier3;
	public ItemSettings m_ItemTier4;

	[SerializeField]
	private Transform[] m_ItemSpawnPoints = new Transform[0];

	#endregion

	#region Unity Callbacks

	private void Awake()
	{
		m_ItemTierSettings.Add(ItemTier.Tier1, m_ItemTier1);
		m_ItemTierSettings.Add(ItemTier.Tier2, m_ItemTier2);
		m_ItemTierSettings.Add(ItemTier.Tier3, m_ItemTier3);
		m_ItemTierSettings.Add(ItemTier.Tier4, m_ItemTier4);
	}

	#endregion

	#region Public Methods

	public void SpawnItem(ItemTier tier)
	{
		ItemSettings settings = m_ItemTierSettings[tier];

		int amountOfMachinesRequired = Random.Range(settings.MinAmountMachines,settings.MaxAmountMachines);


	}

	#endregion

	#region Private Variables

	private Dictionary<ItemTier, ItemSettings> m_ItemTierSettings = new Dictionary<ItemTier, ItemSettings>();

	#endregion
}
