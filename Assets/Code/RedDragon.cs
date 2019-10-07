using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Enemy))]
public class RedDragon : MonoBehaviour {

	public Enemy enemyScript;
	
	 void jumpAttack() {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(Constants.HORIZONTAL_JUMP_STRENGTH, enemyScript.jumpStrength));
	}
}
