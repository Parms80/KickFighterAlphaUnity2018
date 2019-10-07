using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {
	
	public static void SetBestScore (int newScore) {	
		int currentHighScore = GetBestScore();
		if (newScore > currentHighScore) {
			PlayerPrefs.SetInt (Constants.PLAYERPREFS_BESTSCORE_KEY, newScore);
		}
	}
	
	public static int GetBestScore () {
		int bestScore = PlayerPrefs.GetInt (Constants.PLAYERPREFS_BESTSCORE_KEY);
		if (bestScore != null) {
			return bestScore;
		} else {
			return 0;
		}
	}
	
	public static void DeleteScore() {
		PlayerPrefs.DeleteKey (Constants.PLAYERPREFS_BESTSCORE_KEY);
	}
	
}
