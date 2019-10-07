using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {
	
	public Score score;
	public GameObject topScoreText;
	public GameObject congratulationsText;
	public float congratulationsDuration;
	public GameObject scoreText;
	public GameObject levelText;
	public GameObject titleText;
	public GameObject playButton;
	public GameObject gameOverText;
	public GameObject pausedText;
	public GameObject retryButton;
	public GameObject returnToTitleButton;
	public GameObject pauseButton;
	public GameObject resumeButton;
	public GameObject quitButton;
	public GameObject energyBar;
	public GameObject comboBar;
	public ParticleSystem particleSystem;
	private Animator anim;
	private float startTime;
	
	void Start () {
		anim = titleText.GetComponent <Animator>();
	}
	
	public void showMenu()
	{
		bool titleDisplayed = titleText.activeSelf;
		bool finishedAnimation = hasAnimationFinished();
		
		if (!titleDisplayed && finishedAnimation)
		{
			titleText.SetActive(true);			
			playButton.SetActive(true);	
			quitButton.SetActive(true);
			pauseButton.SetActive(false);
			topScoreText.SetActive(true);
			scoreText.SetActive(false);
//			levelText.SetActive(false);
			congratulationsText.SetActive(false);
			energyBar.SetActive(false);
			comboBar.SetActive(false);
		}
	}
	
	protected bool hasAnimationFinished()
	{
		return true;
	}
	
	public void hideMenu()
	{
		bool titleDisplayed = titleText.activeSelf;
		
		if (titleDisplayed)
		{			
			titleText.SetActive(false);
			playButton.SetActive(false);	
			quitButton.SetActive(false);
			pauseButton.SetActive(true);
			energyBar.SetActive(true);
			comboBar.SetActive(true);
			//			levelText.SetActive(true);
//			GameObject canvas = GameObject.Find ("Canvas");
//			GameObject admobObject = canvas.transform.Find("admobObject").gameObject;
//			AdMob admobScript = admobObject.GetComponent<AdMob>();
//			admobScript.ShowBanner();
		}
	}
	
	public void showGameOver()
	{
		bool gameOverDisplayed = gameOverText.activeSelf;		
		
		if (!gameOverDisplayed)
		{
			congratulationsText.SetActive(false);
			score.saveNewTopScore();
			topScoreText.SetActive(false);
			gameOverText.SetActive(true);	
			retryButton.SetActive(true);
			returnToTitleButton.SetActive(true);
			pauseButton.SetActive(false);
		}
	}
	
	public void hideGameOver()
	{
		bool gameOverDisplayed = gameOverText.activeSelf;		
		
		if (gameOverDisplayed)
		{
//			levelText.SetActive(false);
			gameOverText.SetActive(false);	
			retryButton.SetActive(false);
			returnToTitleButton.SetActive(false);
		}
	}
	
	public void showPauseMenu()
	{		
		bool pauseDisplayed = pausedText.activeSelf;	
		if (!pauseDisplayed)
		{
			topScoreText.SetActive(false);
			scoreText.SetActive(false);	
			pausedText.SetActive(true);
			resumeButton.SetActive(true);
			retryButton.SetActive(true);
			returnToTitleButton.SetActive(true);
			pauseButton.SetActive(false);
		}
	}
	
	public void hidePauseMenu()
	{
		bool pauseDisplayed = pausedText.activeSelf;	
		if (pauseDisplayed)
		{
			pausedText.SetActive(false);
			resumeButton.SetActive(false);
			retryButton.SetActive(false);
			returnToTitleButton.SetActive(false);
		}
	}
	
	public void showNewTopScore()
	{
		bool newTopScoreDisplayed = congratulationsText.activeSelf;
		
		if (!newTopScoreDisplayed)
		{
			congratulationsText.SetActive(true);
			pauseButton.SetActive(false);
			startTime = Time.time;
			particleSystem.Play();
		}
		else if (Time.time - startTime > congratulationsDuration)
		{
			GameLogic gameLogicScript;
			gameLogicScript = GameObject.FindObjectOfType<GameLogic>();
			gameLogicScript.gameState = Constants.GAMESTATE_GAMEOVER;
			particleSystem.Stop ();
		}
	}
}
