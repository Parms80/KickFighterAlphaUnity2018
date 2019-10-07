using UnityEngine;
using System.Collections;

public class LevelObjectScript : MonoBehaviour {

	void reset()
	{
	}

	void HandleObjectOffScreen()
	{
		this.gameObject.SetActive (false);
	}
}
