using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public enum GameState
	{
		MainMenu,
		PreGame,
		Game,
		PostGame,
		Highscore
	}

	public enum Scenes
	{
		MainMenu = 0,
		Game = 1
	}

	public static GameManager Instance { get; private set; }

	#region Unity References
	[SerializeField]
	private GameSettings m_Settings = null;

	[SerializeField]
	private GameTickManager m_GameTickManager   = new GameTickManager();
	#endregion

	#region Private Methods
    private MinigameManager m_MinigameManager   = new MinigameManager();

	#endregion

	#region Public Properties
	public GameSettings settings => m_Settings;
	public GameTickManager TickManager => m_GameTickManager;

	public GameState State { get; private set; } = GameState.MainMenu;

	public GameObject TutorialScreenGO { get; set; } = null;

	//public event Action OnGameStarted = null;
	#endregion

	#region Unity Callbacks

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		Instance = this;

		m_MinigameManager.Init();
	}

	private void OnLevelWasLoaded(int level)
	{
		Scenes sceneLoaded = (Scenes)level;

		switch (sceneLoaded)
		{
			case Scenes.MainMenu:
				break;
			case Scenes.Game:
				SetupNewGame(m_Settings.ShowTutorialScreen);
				break;
		}
	}

	private void Update()
	{
		switch (State)
		{
			case GameState.MainMenu:
				break;
			case GameState.PreGame:
				break;
			case GameState.Game:
				//m_GameTickManager.Tick();
                
                m_MinigameManager.Tick();
				break;
			case GameState.PostGame:
				break;
			case GameState.Highscore:
				break;
		}
	}

	#endregion

	#region Public Methods
	public void LoadGame()
	{
		SceneManager.LoadScene((int)Scenes.Game);
	}

	public void StartGame()
	{
		State = GameState.Game;

		m_GameTickManager.ResetGame();
	}

	public void EndGame()
	{
		State = GameState.PostGame;
	}

	public void ShowTutorialScreen(bool isShown)
	{
		TutorialScreenGO?.SetActive(isShown);
	}
	#endregion


	#region Private Methods

	private void SetupNewGame(object showTutorialScreen)
	{
		State = m_Settings.ShowTutorialScreen ? GameState.PreGame : GameState.Game;

		ShowTutorialScreen(m_Settings.ShowTutorialScreen);
	}
	#endregion
}
