using UnityEngine;
using System.Collections;

public class GameLoader : MonoBehaviour {

	public GameObject panel;
	private Animator anim;
	
	void Start () {
		anim = panel.GetComponent <Animator>();
	}
	
	void Update () {
		if (hasAnimationFinished()) {
			Application.LoadLevel (Application.loadedLevel + 1);
		}
	}
	
    protected bool hasAnimationFinished()
    {
		return anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && 
			!anim.IsInTransition(0);			
	}
}
