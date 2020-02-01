using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameItemConfig), menuName = "Game Item Config")]
public class GameItemConfig : ScriptableObject
{
	public AnimationCurve ItemSpawnIntervals;
}
