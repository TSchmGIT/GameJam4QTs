using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MorseSound))]
public class MorseSoundEditor : Editor
{
	public MorseSound morseSound => (MorseSound)target;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Test Effect"))
		{
			morseSound.SetMorseDuration(1.0f);
		}
	}
}
