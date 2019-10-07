using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		GameLogic gameLogicScript = GameObject.FindObjectOfType<GameLogic>();
		if (other.tag == Constants.STRING_PLAYER) {
			Score score = GameObject.FindObjectOfType<Score>();
			if (score.isNewTopScore()) {
				gameLogicScript.gameState = Constants.GAMESTATE_NEW_TOP_SCORE;
			}
			else {
				gameLogicScript.gameState = Constants.GAMESTATE_GAMEOVER;
			}
			other.gameObject.SetActive(false);

			return;
		}
		else if (other.tag == Constants.STRING_ENEMY)
		{
			other.gameObject.SetActive (false);
			gameLogicScript.checkNumberOfEnemiesRemaining ();
		}
	}
}
