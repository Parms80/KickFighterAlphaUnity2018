using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour {

	public bool isKickPressed()
	{	
		if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space)) && !EventSystem.current.IsPointerOverGameObject()){
			return true;
		} else {
			return false;
		}
		
	}
}
