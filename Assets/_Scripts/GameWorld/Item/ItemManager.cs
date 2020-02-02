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
	[SerializeField]
	public GameItemConfig m_ItemConfig = null;

	#endregion

	#region Unity Callbacks

	private void Awake()
	{
		m_ItemTierSettings.Add(ItemTier.Tier1, m_ItemTier1);
		m_ItemTierSettings.Add(ItemTier.Tier2, m_ItemTier2);
		m_ItemTierSettings.Add(ItemTier.Tier3, m_ItemTier3);
		m_ItemTierSettings.Add(ItemTier.Tier4, m_ItemTier4);
	}

	private void Start()
	{
		GameManager.Instance.TickManager.OnGameStarted += TickManager_OnGameStarted;

		m_LastItemSpawnedTimestamp = float.MaxValue * 0.5f;
	}

	private void OnDestroy()
	{
		GameManager.Instance.TickManager.OnGameStarted -= TickManager_OnGameStarted;
	}

	private void Update()
	{
		if (GameManager.Instance.TickManager.isInCountdown)
		{
			return;
		}

		if (GameManager.Instance.State != GameManager.GameState.Game)
		{
			return;
		}

		UpdateItemSpawning();
	}

	#endregion

	#region Public Methods

	public void SpawnItem(ItemTier tier)
	{
		ItemSettings settings = m_ItemTierSettings[tier];

		int amountOfMachinesRequired = Random.Range(settings.MinAmountMachines, settings.MaxAmountMachines + 1);

		List<int> machineIndexList = new List<int>{ 1, 2, 3, 4 };

		for (int i = 0; i < 4 - amountOfMachinesRequired; ++i)
		{
			int randIndex = Random.Range(0, machineIndexList.Count);

			int machineIndex = machineIndexList[randIndex];

			machineIndexList.RemoveAt(randIndex);
		}

		ItemRuntimeData runtimeData;
		runtimeData.MachineOrderList = machineIndexList;

		// Spawn the actual item
		Spawn(runtimeData, settings);
	}

	private void Spawn(ItemRuntimeData runtimeData, ItemSettings settings)
	{
		Transform randomSpawnTransform = m_ItemSpawnPoints[Random.Range(0, m_ItemSpawnPoints.Length)];

		Item spawnedItem = Instantiate(settings.Prefab, randomSpawnTransform.position, Quaternion.identity).GetComponent<Item>();

		spawnedItem.SetRuntimeData(runtimeData);
	}

	#endregion

	#region Private Methods

	private void UpdateItemSpawning()
	{
		float itemSpawnInterval = m_ItemConfig.ItemSpawnIntervals.Evaluate(GameManager.Instance.TickManager.timePassed);
		if (m_LastItemSpawnedTimestamp + itemSpawnInterval <= Time.time)
		{
			m_LastItemSpawnedTimestamp = Time.time;

			SpawnItem(ItemTier.Tier1);
		}
	}

	private void TickManager_OnGameStarted()
	{
		m_LastItemSpawnedTimestamp = Time.time;

		SpawnItem(ItemTier.Tier1);
	}

	#endregion

	#region Private Variables

	private Dictionary<ItemTier, ItemSettings> m_ItemTierSettings = new Dictionary<ItemTier, ItemSettings>();
	private float m_LastItemSpawnedTimestamp = 0.0f;

	#endregion
}
