using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
	private GameLogic gameLogicScript;

	void Start()
	{
		gameLogicScript = GameObject.FindObjectOfType<GameLogic>();
	}
	
	public void StartGame()
	{
		gameLogicScript.gameState = Constants.GAMESTATE_STARTGAME;
		Time.timeScale = 1;
	}
	
	public void ReturnToTitle()
	{
		gameLogicScript.gameState = Constants.GAMESTATE_TITLE;
	}
	
	public void QuitRequest(){
		Application.Quit ();
	}
	
	public void PauseGame()
	{
		gameLogicScript.isPaused = true;
		gameLogicScript.gameStateBeforePaused = gameLogicScript.gameState;
		gameLogicScript.gameState = Constants.GAMESTATE_PAUSED;
		Time.timeScale = 0;
	}
	
	public void ResumeGame()
	{
		gameLogicScript.isPaused = false;
		gameLogicScript.gameState = Constants.GAMESTATE_RESUME;
		Time.timeScale = 1;
	}
	
	private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
	private const string TWEET_LANGUAGE = "en"; 
	
	public void ShareToTwitter (string textToDisplay)
	{
		Application.OpenURL(TWITTER_ADDRESS +
		                    "?text=" + WWW.EscapeURL(textToDisplay) +
		                    "&amp;lang=" + WWW.EscapeURL(TWEET_LANGUAGE));
	}
}