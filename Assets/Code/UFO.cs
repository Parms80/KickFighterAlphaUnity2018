using UnityEngine;
using System.Collections;

public class UFO : MonoBehaviour {
	
	public float moveSpeed;
	public float amplitudeY = 1.0f;
	public float omegaY = 5.0f;
	private float index;
	private Player playerScript;
	private bool movingLeft;
	
	void Start () {
		GameObject player = GameObject.Find(Constants.STRING_PLAYER);
		playerScript = player.transform.GetComponent<Player>();
		movingLeft = true;
	}
	
	
	void reset(){
	}
	
	void Update () {
//		moveForward();
		if (reachedEdgeOfScreen()) {
			reverseDirection();
		}
		bobUpAndDown ();
	}
	
	void moveForward() {
		float moveDistance = (moveSpeed+playerScript.moveSpeed) * Time.deltaTime;
		transform.Translate(-Vector3.right * moveDistance);
	}
	
	bool reachedEdgeOfScreen() {
		if ((movingLeft && Camera.main.WorldToScreenPoint(transform.position).x < 0) ||
		    (!movingLeft && Camera.main.WorldToScreenPoint(transform.position).x > Screen.width)) {
			return true;
		} 
		return false;
	}
	
	void reverseDirection() {
		moveSpeed *= -1;
		movingLeft = !movingLeft;	
	}
	
	void bobUpAndDown()
	{
		index += Time.deltaTime;
		float y = amplitudeY*Mathf.Sin (omegaY*index);
		transform.localPosition= new Vector3(transform.localPosition.x,y,0);
		
	}
}
