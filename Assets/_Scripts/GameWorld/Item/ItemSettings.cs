using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ItemSettings), menuName = "Item Settings")]
public class ItemSettings : ScriptableObject
{
	public int MinAmountMachines;
	public int MaxAmountMachines;
}
