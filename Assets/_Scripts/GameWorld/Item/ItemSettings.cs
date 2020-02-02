using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ItemSettings), menuName = "Item Settings")]
public class ItemSettings : ScriptableObject
{
	public int MinAmountMachines = 1;
	public int MaxAmountMachines = 3;

	public int ScorePerMachineDone = 200;

	public GameObject Prefab;
}
