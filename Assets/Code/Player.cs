using UnityEngine;
using System.Collections;

public class Player : Humanoid {

	public GameObject pauseButton;
	public GameObject shield;
	
	private	PlayerInput playerInput;
	private bool dashEnabled;
	private int normalAttackStrength;
 
	public override void Start () {
		base.Start ();
		anim = GetComponent <Animator>();
		playerInput = GetComponent <PlayerInput>();
		initialSpeed = moveSpeed;
		normalAttackStrength = attackStrength;
	}

	public override void reset(){
		switchToState(Constants.WALK_TO_START_POSITION);
		gameObject.SetActive(true);
		transform.position = new Vector3(-5.97f, -0.22f, -0.75f);
		anim.enabled = true;
		alreadyHit = false;
		moveSpeed = initialSpeed;
		energy = startingEnergy;
		dashEnabled = false;
		disableShield();
		attackStrength = normalAttackStrength;
	}

	public override void checkAndRunState () {
		
		bool kickPressed = playerInput.isKickPressed();
		base.checkAndRunState ();
		switch (state) 
		{

		case Constants.WALK_TO_START_POSITION:
			
			anim.Play(Constants.STRING_RUN);
			moveForward ();

			break;

		case Constants.RUNNING:
			
			anim.Play(Constants.STRING_RUN);
			if (kickPressed && !dashEnabled) {
				doKick ();
			}
			else if (kickPressed && dashEnabled) {
				doDash();
			}
		
			break;
			
		case Constants.DASHING:
		case Constants.ATTACKING:
			if (hasAnimationFinished())
			{
				switchToState(Constants.RUNNING);
				anim.StopPlayback();
				anim.Play(Constants.STRING_RUN);
				moveSpeed = initialSpeed;
				attackStrength = normalAttackStrength;
			}
			
			break;

		case Constants.JUMPING:
			
			if (grounded)
			{
				switchToState(Constants.RUNNING);
				anim.StopPlayback();
				anim.Play(Constants.STRING_RUN);
			}
			
			break;
			
		case Constants.FALLING:
			if (pauseButton.activeSelf) {
				pauseButton.SetActive(false);
			}
			Debug.Log("Player: FALLING");
			break;
		}
	}

	private void moveForward()
	{
		float moveDistance = moveSpeed * Time.deltaTime;
		transform.Translate(Vector3.right * moveDistance);
	}

	private void doKick()
	{
		moveSpeed = 0;
		anim.StopPlayback();	
		switchToState(Constants.ATTACKING);
		anim.Play(Constants.STRING_KICK);
		AudioSource.PlayClipAtPoint(throwAttackSound, transform.position);
	}
	
	public void enableDash()
	{
		dashEnabled = true;
	}
	public void disableDash()
	{
		dashEnabled = false;
	}
	
	public void changeMoveSpeed(int newSpeed) {
		moveSpeed = newSpeed;
	}
	
	public void returnToWalkingSpeed() {
		moveSpeed = initialSpeed;
	}
	
	private void doDash()
	{
		attackStrength = 2;
		anim.StopPlayback();	
		switchToState(Constants.DASHING);
		anim.Play(Constants.STRING_DASH);
		AudioSource.PlayClipAtPoint(throwAttackSound, transform.position);
	}
	
	public int getEnergyLevel() {
		return energy;
	}
	
	public void increaseEnergy(int amount) {
		energy += amount;
		if (energy > startingEnergy) {
			energy = startingEnergy;
		}
	}
	
	public void enableShield() {
		shieldEnabled = true;
		shield.SetActive(true);
	}
	
	public void disableShield() {
		shieldEnabled = false;
		shield.SetActive(false);
		alreadyHit = false;
	}
}
