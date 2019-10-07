using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

	public Camera cam;
	public int objectType;
	public float spawnWaitTime;
	public int maxObjects;
	float spawnTimeLeft;


	void Start()
	{
		spawnTimeLeft = spawnWaitTime;
	}

	void Update()
	{
		spawnObjectOnTimeout ();
	}
	
	void spawnObjectOnTimeout()
	{
		spawnTimeLeft -= Time.deltaTime;
		
		if (readyToSpawn()) 
		{
			spawnObject();
			spawnTimeLeft = spawnWaitTime;
		}	
	}

	bool readyToSpawn()
	{
		int numObjectsActive = NewObjectPoolScript.current.countActiveObjectsOfType(
			objectType);

		if (spawnTimeLeft < 0 && numObjectsActive < maxObjects) {
			return true;
		} else {
			return false;
		}
	}

	public void spawnObject()
	{
		try
		{
			GameObject obj = getObjectFromPool();
			placeObjectOffScreen(obj);
			putObjectOnGround(obj);
			
			obj.SetActive(true);
			obj.SendMessage (Constants.STRING_RESET);
		}
		catch (UnityException e)
		{
			Debug.Log(Constants.STRING_ERRORSPAWNINGOBJECT+""+e);
		}
	}

	public void setObjectType(int type)
	{
		objectType = type;
	}
	
	public int getObjectType()
	{
		return objectType;
	}
	
	GameObject getObjectFromPool()
	{
		GameObject obj = NewObjectPoolScript.current.GetPooledObject(objectType);
		return obj;
	}
	
	void placeObjectOffScreen(GameObject obj)
	{
		Vector3 spawnPosition;
		
		spawnPosition = cam.ScreenToWorldPoint (new Vector3 (getScreenXOfSpriteOffScreen(obj), 0, 0));
		spawnPosition.z = 0;
		spawnPosition.y += Constants.SPAWN_Y_POSITION;
		obj.transform.position = spawnPosition;
	}

	float getScreenXOfSpriteOffScreen(GameObject obj)
	{
		float spriteWidth = obj.GetComponent<Renderer>().bounds.size.x;
		float pixelsPerUnit = Constants.PIXELS_PER_UNIT;
		return Screen.width + (spriteWidth / 2)*pixelsPerUnit;
	}
	
	void putObjectOnGround(GameObject obj)
	{
		Vector2 linecastStartPos = new Vector2 (obj.transform.position.x, 0);
		Vector2 linecastEndPos = new Vector2 (obj.transform.position.x, 
		                                      Constants.LINECAST_END_Y);
		RaycastHit2D groundPos = Physics2D.Linecast (linecastStartPos, 
		                                             linecastEndPos, 
		                                             1 << LayerMask.NameToLayer(Constants.STRING_GROUND));
		
		Vector3 spawnPosition = obj.transform.position;
		spawnPosition.y = groundPos.transform.position.y;
		obj.transform.position = spawnPosition;
	}

	public int getMaxObjects()
	{
		return maxObjects;
	}

	public void setMaxObjects(int max)
	{
		maxObjects = max;
	}
}
