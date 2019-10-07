using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

	public Slider slider;
	private Player player;
	
	void Start() {
		player = GetComponent<Player>();
		slider.maxValue = player.startingEnergy;
	}
	
	void Update () {
		int energy = player.getEnergyLevel();
		slider.value = Mathf.MoveTowards(slider.value, energy, 0.15f);
	}
}
