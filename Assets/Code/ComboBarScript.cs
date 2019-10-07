using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComboBarScript : MonoBehaviour {
	
	public Player player;
	public Sprite[] powerUpSprites;
	public GameObject powerUpIcon;
	public AudioClip selectedSound;
	public AudioClip pingSound;
	public int maxComboCount = 15;
	public float depletionSpeed = 0.3f;
	private Slider slider;
	private int comboCount = 0;
	private Image powerUpIconImage;
	private int currentIcon = 0;
	private int cycleFramesCount = 0;
	private int framesToChangeIcon = 1;
	private bool isCycling = false;
	private bool powerUpApplied = false;
	private int numFlashesLeft = 0;
	
	void Start() {
		slider = GetComponent<Slider>();
		slider.maxValue = maxComboCount;
		powerUpIconImage = powerUpIcon.GetComponent<Image>();
	}
	
	public void Reset() {
		slider.value = 0;
		isCycling = false;
		powerUpApplied = false;
		powerUpIcon.SetActive(false);
		comboCount = 0;
	}
	
	void Update () {
		UpdateSlider();
		if (!isCycling && comboCount == maxComboCount)
		{
			isCycling = true;
			currentIcon = Random.Range(0,powerUpSprites.Length);
			powerUpIcon.SetActive(true);
		}
		if (isCycling) {			
			CyclePowerUpIcon();
		}
	}
	
	void UpdateSlider() {
		if (!powerUpApplied) {
			slider.value = Mathf.MoveTowards(slider.value, comboCount, 0.15f);
		} else {
			DecreaseComboBarWhenPowerUpApplied();
		}	
	}
	
	public void AddToComboBar() {
		if (!powerUpApplied && comboCount < maxComboCount) {
			comboCount++;
		}
	}
	
	void CyclePowerUpIcon() {
		cycleFramesCount += 1;
		if (cycleFramesCount >= framesToChangeIcon){
			ChangeIcon();
			cycleFramesCount = 0;
			framesToChangeIcon += 2;
			
			if (framesToChangeIcon >= Constants.FRAMES_TO_CHANGE_POWERUP_ICON) {
				ApplyPowerUp();
				StartCoroutine(MakeIconFlash());
			}
		}
		powerUpIconImage.sprite = powerUpSprites[currentIcon];
	}
	
	void ChangeIcon() {
		currentIcon++;
		if (currentIcon > powerUpSprites.Length-1) {
			currentIcon = 0;
		}		
		AudioSource.PlayClipAtPoint(pingSound, player.transform.position);
	}
	
	void ApplyPowerUp() {
		isCycling = false;
		powerUpApplied = true;
		ResetComboBar();
		cycleFramesCount = 0;
		framesToChangeIcon = 1;
		numFlashesLeft = 3;
		
		switch (currentIcon) {
			case 0:
				player.increaseEnergy(1);
			break;
				
			case 1:
				player.enableDash();
			break;
			
			case 2:
				player.enableShield();
			break;
		}
	}
	
	void DecreaseComboBarWhenPowerUpApplied() {	
		float decreaseRate = depletionSpeed;
		if (currentIcon == 0) {
			decreaseRate = 0.15f;
		}
		slider.value = Mathf.MoveTowards(slider.value, comboCount, decreaseRate);
		if (slider.value == 0) {
			powerUpApplied = false;
			player.disableDash();
			player.disableShield();
			powerUpIcon.SetActive(false);
		}
	}
	
	IEnumerator MakeIconFlash() {
		while (numFlashesLeft > 0) {
			yield return new WaitForSeconds(0.2f);
			powerUpIcon.SetActive(!powerUpIcon.activeSelf);
			if (!powerUpIcon.activeSelf) {
				numFlashesLeft--;
				AudioSource.PlayClipAtPoint(selectedSound, player.transform.position);
			}
		}
	}
	
	public void ResetComboBar() {
		if (!isCycling) {
			comboCount = 0;
		}
	}
}
