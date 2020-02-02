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
		Game = 1,
		Credits = 2
	}

	public static GameManager Instance { get; private set; }

	#region Unity References
	[SerializeField]
	private GameSettings m_Settings = null;

	[SerializeField]
	private GameTickManager m_GameTickManager;
	private ItemManager m_ItemManager;
	#endregion
	
	#region Public Properties
	public GameSettings settings => m_Settings;
	public GameTickManager TickManager => m_GameTickManager;
    public ItemManager ItemManager => m_ItemManager;

	public GameState State = GameState.MainMenu;
   

	public GameObject TutorialScreenGO { get; set; } = null;
	public GameObject EndScreenGO { get; set; } = null;

	public MinigameManager MinigameManager { get; private set; } = new MinigameManager();

	//public event Action OnGameStarted = null;
	#endregion

	#region Unity Callbacks

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		Instance = this;

		MinigameManager.Init();
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
				MinigameManager.Tick();
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

	public void LoadMainMenu()
	{
		SceneManager.LoadScene((int)Scenes.MainMenu);
	}

	public void LoadCredits()
	{
		SceneManager.LoadScene((int)Scenes.Credits);
	}

	public void StartGame()
	{
		State = GameState.Game;

		m_GameTickManager.ResetGame();
	}

	public void EndGame()
	{
		State = GameState.PostGame;

		ShowEndScreen(true);
	}

	public void ShowTutorialScreen(bool isShown)
	{
		TutorialScreenGO.SetActive(isShown);
	}

	public void ShowEndScreen(bool isShown)
	{
		EndScreenGO.SetActive(isShown);
	}
	#endregion


	#region Private Methods

	private void SetupNewGame(object showTutorialScreen)
	{
		MinigameManager = new MinigameManager();
		MinigameManager.Init();

		State = m_Settings.ShowTutorialScreen ? GameState.PreGame : GameState.Game;

		ShowTutorialScreen(m_Settings.ShowTutorialScreen);
	}
	#endregion
}
