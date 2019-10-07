using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewObjectPoolScript : MonoBehaviour {

	public static NewObjectPoolScript current;
	public List<GameObject> pooledObjectTypes;
	public int pooledAmount = Constants.MAX_OBJECTS_PER_TYPE;
	public bool willGrow = true;
	List<List<GameObject>> pooledObjects;

	void Awake()
	{
		current = this;
		setupPooledObjectsOfEachType();
	}

	void setupPooledObjectsOfEachType()
	{
		pooledObjects = new List<List<GameObject>> ();

		foreach (GameObject poolObject in pooledObjectTypes)
		{
			createInstancesAndAddToPool(poolObject);
		}
	}

	void createInstancesAndAddToPool(GameObject poolObject)
	{
		List<GameObject> listOfObjectsOfThisType = new List<GameObject>();

		for (int i = 0; i < pooledAmount; i++)
		{
			GameObject thisObject = (GameObject)Instantiate(poolObject);
			thisObject.SetActive (false);
			listOfObjectsOfThisType.Add(thisObject);
		}
		pooledObjects.Add(listOfObjectsOfThisType);
	}


	public GameObject GetPooledObject(int objectType)
	{
		for (int i = 0; i < pooledObjects[objectType].Count; i++)
		{
			GameObject thisObject = pooledObjects[objectType][i];

			if (!thisObject.activeInHierarchy)
			{
				return thisObject;
			}
		}

		if (willGrow)
		{
			GameObject obj = createNewObjectAndAddToList(objectType);
			return obj;
		}

		return null;
	}

	GameObject createNewObjectAndAddToList(int objectType)
	{
		GameObject obj = (GameObject)Instantiate(pooledObjectTypes[objectType]);
		pooledObjects[objectType].Add(obj);

		return obj;
	}

	public int countAllActiveObjects()
	{
		int numActiveObjects = 0;

		for (int i = 0; i < pooledObjects.Count; i++)
		{
			numActiveObjects += countActiveObjectsOfType(i);
		}
		
		return numActiveObjects;
	}
	
	public int countActiveObjectsOfType(int objectType)
	{
		int numActive = 0;

		for (int i = 0; i < pooledObjects[objectType].Count; i++) 
		{
			GameObject thisObject = pooledObjects[objectType][i];
			
			if (thisObject.activeInHierarchy)
			{
				numActive++;
			}
		}

		return numActive;
	}
}
