using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemManager))]
public class ItemManagerEditor : Editor
{
	public ItemManager itemManager => (ItemManager)target;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Spawn Item"))
		{
			itemManager.SpawnItem(ItemTier.Tier1);
		}
	}
}
