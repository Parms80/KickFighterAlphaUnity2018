using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	int score;
	public Text topScoreText;
	public Text scoreText;

	void Start () {
		resetScore();
	}
	
	public void resetScore()
	{
		score = 0;
		setScoreText();
		setTopScoreText();
	}

	void setScoreText()
	{
		scoreText.text = "" + score.ToString ();
	}
	
	void setTopScoreText(){
		int topScore = PlayerPrefsManager.GetBestScore();
		topScoreText.text = "Best " + topScore.ToString();
	}
	
	public void saveNewTopScore(){
		PlayerPrefsManager.SetBestScore(score);
	}
	
	public void addToScore(int points)
	{
		score += points;
		setScoreText ();
	}
	
	public int getScore()
	{
		return score;
	}
	
	public bool isNewTopScore()
	{
		return score > PlayerPrefsManager.GetBestScore();
	}
}
