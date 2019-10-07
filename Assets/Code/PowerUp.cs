using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	
	private Player playerScript;
	private Animator anim;
	public float moveSpeed;
	
	void Start () {
		anim = GetComponent <Animator>();
		GameObject player = GameObject.Find(Constants.STRING_PLAYER);
		playerScript = player.transform.GetComponent<Player>();
	}
	
	void Update () {
		float moveDistance = (playerScript.moveSpeed + moveSpeed) * Time.deltaTime;
//		transform.Translate(-Vector3.right * moveDistance);
		transform.localPosition += new Vector3(moveDistance,0,0);
	}
	
	public void InitialisePowerup(Vector3 position) {
		transform.position = position;
		gameObject.SetActive(true);
		anim.Play("Knocked back");
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == Constants.STRING_PLAYERHITCOLLISION)
		{	
//			gameObject.SetActive(false);
			playerScript.increaseEnergy(1);
		}	
	}
}
