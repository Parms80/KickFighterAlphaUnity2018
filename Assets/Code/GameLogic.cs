using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameLogic : MonoBehaviour {

	public Score score;
	public Level level;
	public Text levelText;
	public int gameState;
	public int gameStateBeforePaused;
	public Player playerObject;
	public GameObject topScoreText;
	public GameObject scoreText;
	public GameObject pauseButton;
	public Camera mainCamera;
	public GameObject backgroundContainer;
	public bool isPaused = false;
	public MenuManager menuManager;
	public GameObject comboBar;
	private int enemyWaveLastUpdate;
	private int scoreLastUpdate;

	void Start () {
		gameState = Constants.GAMESTATE_MENU;
	}

	void ResetGame() {
		level.resetLevel();
		scoreLastUpdate = 0;
		enemyWaveLastUpdate = 0;
		playerObject.reset();
		resetBackgrounds();
		EnemyWave.enemyWaveGenerator.DeactivateAllEnemies();
		EnemyWave.enemyWaveGenerator.setMaxEnemies(2);
		score.resetScore();
		pauseButton.SetActive(true);
		isPaused = false;
		if (!comboBar.gameObject.activeSelf) {
			comboBar.gameObject.SetActive(true);
		}
		comboBar.GetComponent<ComboBarScript>().Reset();
	}

	void Update () {
		
		switch (gameState) 
		{
		case Constants.GAMESTATE_TITLE:
			menuManager.showMenu();
			menuManager.hideGameOver();
			menuManager.hidePauseMenu();
			EnemyWave.enemyWaveGenerator.DeactivateAllEnemies();
			playerObject.gameObject.SetActive(false);
			playerObject.disableShield();
			break;
		
		case Constants.GAMESTATE_STARTGAME:
			ResetGame();
			gameState = Constants.GAMESTATE_GETREADY;
			menuManager.hideMenu ();
			menuManager.hideGameOver();
			menuManager.hidePauseMenu();
			topScoreText.SetActive(true);
			scoreText.SetActive(true);
			break;

		case Constants.GAMESTATE_GETREADY:
			if (playerObject.transform.position.x >= Constants.READY_X_POS)
			{
				gameState = Constants.GAMESTATE_PLAY;
				playerObject.switchToState(Constants.RUNNING);
				EnemyWave.enemyWaveGenerator.createNewWave();
			}
			break;

		case Constants.GAMESTATE_PLAY:
			
			scoreLastUpdate = score.getScore ();
			enemyWaveLastUpdate = EnemyWave.enemyWaveGenerator.getCurrentWave();
			break;
			
		case Constants.GAMESTATE_NEW_TOP_SCORE:
			menuManager.showNewTopScore();
			break;
		
		case Constants.GAMESTATE_GAMEOVER:
			menuManager.showGameOver();
			break;
			
		case Constants.GAMESTATE_PAUSED:
			menuManager.showPauseMenu();
			break;
			
		case Constants.GAMESTATE_RESUME:
			menuManager.hidePauseMenu();
			gameState = gameStateBeforePaused;
			pauseButton.SetActive(true);
			topScoreText.SetActive(true);
			scoreText.SetActive(true);
			Time.timeScale = 1;
			break;
		}			
	}
	
	void resetBackgrounds()
	{
		int numChildren = backgroundContainer.transform.childCount;
		
		for (int i = 0; i < numChildren; i++)
		{
			GameObject paneContainer = backgroundContainer.transform.GetChild(i).gameObject;
			paneContainer.GetComponent<BackgroundScript>().resetBackgrounds();
		}
	}

	bool qualifiedForNextLevel()
	{
		bool qualified = false;
		int currentWave = EnemyWave.enemyWaveGenerator.getCurrentWave();
		
		if (currentWave >= (level.getLevel()+1)*Constants.WAVES_PER_LEVEL)
		{
			qualified = true;
		}

		return qualified;
	}

	void setNextLevel()
	{
		int maxEnemies;
		maxEnemies = EnemyWave.enemyWaveGenerator.getMaxEnemies();
		level.updateLevel();
		if (maxEnemies < NewObjectPoolScript.current.pooledAmount)
		{
			EnemyWave.enemyWaveGenerator.setMaxEnemies(maxEnemies+1);
		}
	}
	
	public void checkNumberOfEnemiesRemaining()
	{		
		int numEnemies = NewObjectPoolScript.current.countAllActiveObjects();
		if (numEnemies == 0)
		{
			if (qualifiedForNextLevel ()) {
				setNextLevel ();
			}
			EnemyWave.enemyWaveGenerator.createNewWave();
		}
	}
}
