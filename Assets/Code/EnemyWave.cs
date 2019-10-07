using UnityEngine;
using System.Collections;

public class EnemyWave : MonoBehaviour {

	public static EnemyWave enemyWaveGenerator;
	public Camera cam;
	public GameLogic gameLogic;
	public int levelToAddEnemyVariation;
	public GameObject enemy;
	private Level level;
	private int[] enemySpacing;
	private int currentWave;
	private int maxEnemies;

	void Awake()
	{
		enemyWaveGenerator = this;
	}

	void Start()
	{
		enemySpacing = new int[10];
		level = GameObject.FindObjectOfType<Level>();
	}

	
	public void DeactivateAllEnemies()
	{
		Enemy[] enemies = FindObjectsOfType<Enemy>();
		for (int i = 0; i < enemies.Length; i++)
		{
			enemies[i].gameObject.SetActive(false);
			initialiseAttackCollider(enemies[i]);
		}
		currentWave = 0;
	}
	
	private void initialiseAttackCollider(Enemy enemy) {
		Transform attackBoxTransform = enemy.transform.Find("CollisionBox");
		BoxCollider2D attackBoxCollider = attackBoxTransform.GetComponent<BoxCollider2D>();
		attackBoxCollider.enabled = false;
	}
	
	public void createNewWave()
	{
		try
		{
			createFormation();
			Vector3 lastPosition = new Vector3(0,0,0);
			int enemyType = determineEnemyTypeToGenerate();
			
			for (int currentEnemyIndex = 0; currentEnemyIndex < maxEnemies; currentEnemyIndex++)
			{
				GameObject obj = getObjectFromPool (enemyType);				
				placeObjectOffScreen(obj);
				if (currentEnemyIndex > 0)
				{
					adjustSpacing(obj, currentEnemyIndex, lastPosition);
				}
				putObjectOnGround(obj);
				obj.SetActive(true);
				obj.SendMessage (Constants.STRING_RESET);
				lastPosition = obj.transform.position;
			}
			currentWave++;
		}
		catch (UnityException e)
		{
			Debug.Log(Constants.STRING_ERRORSPAWNINGOBJECT+""+e);
		}
	}
	
	void createFormation()
	{
		for (int i = 0; i < maxEnemies; i++) {
			enemySpacing [i] = Random.Range(1,3);
		}
	}
	
	int determineEnemyTypeToGenerate()
	{
		int numEnemyTypes = NewObjectPoolScript.current.pooledObjectTypes.Count;
		bool found = false;
		int enemyType = 0;
		
		while (!found) {
			enemyType = Random.Range (0,numEnemyTypes);
			GameObject enemyObject = getObjectFromPool (enemyType);
			Misc misc = enemyObject.GetComponent<Misc>();
			if (level.getLevel() >= misc.levelToIntroduce) 
			{
				found = true;
			}		
		}
//		enemyType = 1;
		return enemyType;
	}

	GameObject getObjectFromPool(int enemyType)
	{
		GameObject obj = NewObjectPoolScript.current.GetPooledObject(enemyType);

//		GameObject obj = (GameObject)Instantiate(enemy);
//			GameObject thisObject = (GameObject)Instantiate(poolObject);
//		thisObject.SetActive (false);
		return obj;
	}
	
	void placeObjectOffScreen(GameObject obj)
	{
		Vector3 spawnPosition;
		
		spawnPosition = cam.ScreenToWorldPoint (new Vector3 (getScreenXOfSpriteOffScreen(obj), 0, 0));
		spawnPosition.z = -1;
		spawnPosition.y += Constants.SPAWN_Y_POSITION;
		obj.transform.position = spawnPosition;
	}
	
	float getScreenXOfSpriteOffScreen(GameObject obj)
	{
		float spriteWidth = obj.GetComponent<Renderer>().bounds.size.x;
		float pixelsPerUnit = Constants.PIXELS_PER_UNIT;
		return Screen.width + (spriteWidth / 2)*pixelsPerUnit;
	}

	void adjustSpacing(GameObject obj, int positionInFormation, Vector3 lastEnemyPosition)
	{
		float spriteWidth = Constants.ENEMY_SPRITE_WIDTH;//obj.renderer.bounds.size.x;
		float spacingAdjustment = obj.GetComponent<Misc>().spacingAdjustment;
		Vector3 position = lastEnemyPosition;
		position.x += spriteWidth * enemySpacing[positionInFormation] + spacingAdjustment;
		obj.transform.position = position;
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

	public int getMaxEnemies()
	{
		return maxEnemies;
	}
	
	public void setMaxEnemies(int max)
	{
		maxEnemies = max;
	}
	
	public int getCurrentWave()
	{
		return currentWave;
	}
}
