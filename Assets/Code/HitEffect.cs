using UnityEngine;
using System.Collections;

public class HitEffect : MonoBehaviour {
	
	public static HitEffect hitEffect;
	private Animator hitEffectAnimator;
	
	void Awake() {
		hitEffect = this;
	}
		
	void Start () {
		hitEffectAnimator = GetComponent<Animator>();
		gameObject.SetActive(false);
	}
	
	void Update () {
		if (hitEffectAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 &&
			!hitEffectAnimator.IsInTransition(0)) {
			gameObject.SetActive(false);
		}
	}
	
	public void StartHitEffect() {
		
		gameObject.SetActive(true);
		hitEffectAnimator.Play ("Hit effect", 0);
	}
}
