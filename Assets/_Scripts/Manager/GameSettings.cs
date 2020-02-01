using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameSettings), menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
	public float GameTime = 0.0f;
	public float CountdownTime = 3.0f;

	public bool ShowTutorialScreen = true;

    [Header("Minigame: Sequence")]
    public Texture2D[] RuneTextures;
    public GameObject RuneSpritePrefab;
    public Color BadColorBase;
    public Color BadColorGlow;
    public Color GoodColorBase;
    public Color GoodColorGlow;
}
