using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	float scrollSpeed;
	public GameLogic gameLogicObject;

	void Start () {
		setScrollSpeed();
	}

	void Update () {
		if (gameLogicObject.gameState == Constants.GAMESTATE_PLAY) {
			moveCameraRight ();
		}
	}

	void setScrollSpeed()
	{
		GameObject playerObject = GameObject.Find (Constants.STRING_PLAYER);
		Player playerScript = (Player)playerObject.GetComponent (Constants.STRING_PLAYER);
		scrollSpeed = playerScript.moveSpeed;
	}

	void moveCameraRight()
	{
		float moveDistance = scrollSpeed * Time.deltaTime;
		transform.Translate(Vector3.right * moveDistance);
	}
}
