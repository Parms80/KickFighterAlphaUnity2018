using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level : MonoBehaviour {

	int level;
	public Text levelText;
	
	void Start () {
		resetLevel();
	}
	
	public void resetLevel()
	{
		level = 0;
		setLevelText();
	}
	
	void setLevelText()
	{
		levelText.text = "Level " + (level+1).ToString ();
	}
	
	public void updateLevel()
	{
		level++;
		setLevelText();
	}	
	
	public int getLevel() {
		return level;
	}
}
