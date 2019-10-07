using UnityEngine;
using System.Collections;

public class CollisionScript : MonoBehaviour {

	public string otherObjectTag;
	private GameObject scoreObject;
	private Score score;
	private GameObject comboBar;
	private ComboBarScript comboBarScript;

	void Start()
	{
		scoreObject = GameObject.Find(Constants.STRING_SCORE);
		score = scoreObject.GetComponent<Score> ();		
		GameObject canvas = GameObject.Find ("Canvas");
		comboBar = canvas.transform.Find("Combo bar").gameObject;
		comboBarScript = comboBar.GetComponent<ComboBarScript>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.tag == otherObjectTag)
		{	
			Humanoid thisObject = this.gameObject.transform.GetComponent<Humanoid> ();	
			if (!thisObject.alreadyHit)
			{
				Humanoid otherObject = other.transform.parent.gameObject.GetComponent<Humanoid> ();	
				int attackStrength = otherObject.GetComponent<Humanoid>().attackStrength;
				thisObject.alreadyHit = true;
				thisObject.takeHit(attackStrength);	
				if (hasPlayerHitEnemy(other))
				{
					score.addToScore(Constants.POINTS_FOR_ENEMY_KILL);
					comboBarScript.AddToComboBar();
				}
				else
				{
					comboBarScript.ResetComboBar();
				}
			}
		}	
	}

	bool doesColliderBelongToPlayer(Collider2D coll)
	{
		bool isPlayer = false;

		if (coll.transform.parent.tag == Constants.STRING_PLAYER) {
			isPlayer = true;
		}

		return isPlayer;
	}

	bool hasPlayerHitEnemy(Collider2D other)
	{
		return other.gameObject.tag == Constants.STRING_PLAYERHITCOLLISION;
	}

	int checkEnemyState(Collider2D other)
	{
		Enemy enemyScript = other.gameObject.GetComponent<Enemy> ();
		int state = enemyScript.getState ();

		return state;
	}

	bool isPlayingFallenAnimation(Collider2D other)
	{
		bool fallingAnimationPlaying = false;
		Enemy enemyScript = other.gameObject.GetComponent<Enemy> ();
		Animation anim = enemyScript.GetComponent<Animation> ();

		if (anim.IsPlaying (Constants.STRING_FALLING)) 
		{
			fallingAnimationPlaying = true;
		}

		return fallingAnimationPlaying;
	}
}
