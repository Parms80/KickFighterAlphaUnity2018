using UnityEngine;
using System.Collections;

public class ObjectOffScreenScript : MonoBehaviour {

	BoxCollider2D coll;

	void Start () {
		coll = gameObject.GetComponent<Collider2D>() as BoxCollider2D;
	}

	void Update () {

		Vector3 objectRightEdge = getObjectRightEdge();

		if (hasObjectGoneOffScreen (objectRightEdge)) {
			SendMessage (Constants.STRING_HANDLEOBJECTOFFSCREEN);
		} 
	}

	Vector3 getObjectRightEdge()
	{
		Vector3 rightEdge = transform.position;
		rightEdge.x += coll.size.x * transform.localScale.x;

		return rightEdge;
	}

	bool hasObjectGoneOffScreen(Vector3 obj)
	{
		return Camera.main.WorldToScreenPoint(obj).x <= 0;
	}
}
