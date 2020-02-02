using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameSettings), menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
	public float GameTime = 0.0f;
	public float CountdownTime = 3.0f;

	public bool ShowTutorialScreen = true;

    [Header("Items")]
    public Mesh         ItemMeshNoStageLeft;
    public List<Mesh>   ItemMeshMinigames;
    public Color        ItemColorNoStageLeft;
    public List<Color>  ItemColorMinigames;
    public int          ItemScorePerMinigameDone;

    [Header("Minigame: Sequence")]
    public Texture2D[] RuneTextures;
    public GameObject RuneSpritePrefab;
    public Color BadColorBase;
    public Color BadColorGlow;
    public Color GoodColorBase;
    public Color GoodColorGlow;

	[Header("Minigame: Morse")]
	public GameObject MorseSoundPrefab;
	public Color MorseColorPlayback;
	public Color MorseColorInput;
	public float ShortPlaybackTime;
	public float ShortInputTolerance;
	public float LongPlaybackTime;
	public float LongInputTolerance;
	public float InputDelayTolerance;
	public int AmountSoundsPerSequence;
	public int MaxAmountLongSoundsPerSequence;

    [Header("Minigame: Screwdriver")]
    public GameObject ScrewDriverTargetPrefab;
    public GameObject ScrewDriverPinPrefab;

    [Header("Minigame: Matcher")]
    public List<Texture2D> VirusGraphics;
}
