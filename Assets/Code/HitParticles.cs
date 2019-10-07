using UnityEngine;
using System.Collections;

public class HitParticles : MonoBehaviour {

	private ParticleSystem hitParticles;
	
	void Start() {
		hitParticles = GetComponent<ParticleSystem>();
	}
	
	public void StartParticles() {
		hitParticles.Play();	
	}
}
