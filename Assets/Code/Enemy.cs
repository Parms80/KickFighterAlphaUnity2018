using UnityEngine;
using System.Collections;

public class Enemy : Humanoid {

	public float attackDistance = 1.2f;
//	public int levelToIntroduce = 0;
//	public float spacingAdjustment = 0.0f;
	private bool attacked;
	private Player playerScript;
	private GameLogic gameLogic;

	public override void Start () {
		base.Start ();
		GameObject player = GameObject.Find(Constants.STRING_PLAYER);
		playerScript = player.transform.GetComponent<Player>();
		gameLogic = GameObject.FindObjectOfType<GameLogic>();
	}

	public override void reset(){
		switchToState(Constants.RUNNING);
		attacked = false;
		anim = GetComponent<Animator>();
		anim.enabled = true;
		alreadyHit = false;
		BoxCollider2D box = GetComponent<BoxCollider2D>();
		box.enabled = true;
		energy = startingEnergy;
		moveSpeed = initialSpeed;
		numTimesHitGround = 0;
	}

	public override void checkAndRunState () {
		base.checkAndRunState ();
		
		switch (state) 
		{
			case Constants.RUNNING:
				if (playerScript.getState() == Constants.FALLING ||
					playerScript.getState() == Constants.KNOCKED_BACK ||
				    playerScript.getState() == Constants.GET_UP)
			    {
					state = Constants.IDLE;
					anim.Play(Constants.STRING_IDLE);
					moveSpeed = 0;
			    }
			    else
			    {
	
					if (!attacked)
					{
						checkAttack();
					}
				}	
				moveForward();
				break;

			case Constants.ATTACKING:
			
				moveForward();
				if (hasAnimationFinished())
				{
					switchToState(Constants.RUNNING);
					attacked = true;
					
					anim.StopPlayback();
					anim.Play(Constants.STRING_RUN);
				}
				
				break;
				
			
			case Constants.GET_UP:
				moveForward();
				break;
				
			case Constants.IDLE:
				if (playerScript.getState() == Constants.RUNNING)
				{
					state = Constants.RUNNING;
					anim.Play(Constants.STRING_RUN);
					moveSpeed = initialSpeed;
				}
				moveForward();
				break;
		}
		
	}
	
	private void moveForward()
	{
		float moveDistance = (moveSpeed+playerScript.moveSpeed) * Time.deltaTime;
		transform.Translate(-Vector3.right * moveDistance);
	}

	private void checkAttack()
	{
		GameObject player = GameObject.Find(Constants.STRING_PLAYER);
		float xDistance = (this.transform.position.x - player.transform.position.x);
		float yDistance = Mathf.Abs(this.transform.position.y - player.transform.position.y);

		if (yDistance < Constants.INITIATE_PUNCH_DISTANCE)
		{
			if (xDistance > 0.0 && xDistance < attackDistance)
			{
				anim.StopPlayback();
				anim.Play(Constants.STRING_ATTACK);
				switchToState(Constants.ATTACKING);
			}
		}
	}
	
	protected void HandleObjectOffScreen()
	{
		this.gameObject.SetActive (false);
		gameLogic.checkNumberOfEnemiesRemaining ();
		
	}	
}
