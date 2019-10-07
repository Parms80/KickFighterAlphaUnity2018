using UnityEngine;
using System.Collections;

public class Humanoid : MonoBehaviour {

	public float moveSpeed;
	public float jumpStrength;
	public Vector2 knockBackForce;
	public AudioClip hitSound;
	public AudioClip throwAttackSound;
	public bool alreadyHit;
	public int startingEnergy;
	public int attackStrength = 1;
	public float initialSpeed;
	protected GameObject gameLogicObject;
	protected GameLogic gameLogicScript;
	protected Collider2D collisionBox;
	protected Animator anim;
	protected int state;
	[SerializeField] protected bool grounded;
	protected int energy;
	protected bool shieldEnabled = false;
	private Transform groundCheck;
	private BoxCollider2D groundCheckBoxCollider;
	private GameObject hitParticles;
	
	public virtual void Start () {
		anim = GetComponent <Animator>();
		groundCheck = transform.Find(Constants.STRING_GROUNDCHECK);
		groundCheckBoxCollider = groundCheck.GetComponent<BoxCollider2D>();
		gameLogicObject = GameObject.Find ("GameLogic");
		gameLogicScript = gameLogicObject.GetComponent<GameLogic>();
		collisionBox = gameObject.GetComponent<Collider2D>();
		hitParticles = transform.Find("Hit particles").gameObject;
	}

	public virtual void reset()
	{
		switchToState(Constants.RUNNING);
		anim.Play(Constants.STRING_RUN);
		alreadyHit = false;
		energy = startingEnergy;
	}
	
	void Update () {
		if (!gameLogicScript.isPaused) {
			if (gameLogicScript.gameState == Constants.GAMESTATE_PLAY || 
			    gameLogicScript.gameState == Constants.GAMESTATE_GETREADY) {
				grounded = isCharacterOnGround ();
				checkAndRunState ();
			} else {
//				anim.Play(Constants.STRING_RUN);
			}
		} 
	}

	bool isCharacterOnGround()
	{
		bool isTouching = groundCheckBoxCollider.IsTouchingLayers(LayerMask.GetMask(Constants.STRING_GROUND));
		Debug.Log("isTouching = "+isTouching);
		return isTouching;
//		return Physics2D.Linecast(transform.localPosition, 
//		                          groundCheck.position, 
//		                          1 << LayerMask.NameToLayer(Constants.STRING_GROUND));
//		if (transform.position.y <= -0.64f) {
//			return true;
//		} else {
//			return false;
//		}
	}

	public virtual void checkAndRunState()	{
		switch (state) 
		{
			case Constants.FALLING:
				disableColliderWhenFalling();
				enableColliderBeforeHittingDestroyer();
				break;
			
			case Constants.KNOCKED_BACK:
				if (landedOnGround()) {
					moveSpeed = 0;
					if (hasAnimationFinished()) {
						state = Constants.GET_UP;
						anim.Play (Constants.STRING_GETUP);
					}
				}
			break;
			
			case Constants.GET_UP:
				if (hasAnimationFinished()) {
					moveSpeed = initialSpeed;
					state = Constants.RUNNING;
					anim.Play (Constants.STRING_RUN);
					alreadyHit = false;
				}
			break;
		}
	}

	void runForward() {
		float moveDistance = moveSpeed * Time.deltaTime;
		transform.Translate(Vector3.right * moveDistance);
	}

	protected void jump() {
		anim.StopPlayback();	
		switchToState(Constants.JUMPING);
		GetComponent<Rigidbody2D>().AddForce(new Vector2(Constants.HORIZONTAL_JUMP_STRENGTH, jumpStrength));
		anim.Play(Constants.STRING_JUMP);
	}
	
	public void takeHit(int energyDeducted) {
		if (!shieldEnabled) {
			fallBack();
			energy -= energyDeducted;
			if (energy <= 0) {
				MakeHumanFall();
			}
		}
	}
	
	private void fallBack() {
		moveSpeed *= -2;
		GetComponent<Rigidbody2D>().AddForce(knockBackForce);
		AudioSource.PlayClipAtPoint(hitSound, transform.position);
		anim.Play(Constants.STRING_FALLING);
		state = Constants.KNOCKED_BACK;
	}
	
	private void MakeHumanFall(){
		collisionBox.enabled = false;
		state = Constants.FALLING;
	}
	
	public void startHitParticles() {
		if (hitParticles != null) {
			hitParticles.GetComponent<ParticleSystem>().Play();
		}
	}
	
	public void switchToState(int newState) {
		state = newState;
	}
	
	private void HandleObjectOffScreen()
	{
		this.gameObject.SetActive (false);
	}
	
	private bool landedOnGround() {
		if (grounded && transform.GetComponent<Rigidbody2D>().velocity.y < 0)
		{
			return true;
		}
		return false;
	}
	
	private void disableColliderWhenFalling()
	{
		if (!grounded && anim.enabled)
		{
			this.transform.GetComponent<Collider2D>().enabled = false;
		}
	}
	
	private void enableColliderBeforeHittingDestroyer()
	{
		if (transform.position.y < -3.0f && collisionBox.enabled != true)
		{		
			collisionBox.enabled = true;
			this.transform.GetComponent<Collider2D>().enabled = true;
			anim.enabled = false;				// Prevents box collider being disabled again for Red Dragon
		}
	}	

	protected bool hasAnimationFinished()
	{
		return anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && 
			!anim.IsInTransition(0);
	}

	public int getState()
	{
		return state;
	}

}
