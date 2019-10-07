using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	public GameLogic gameLogicObject;
	public Player player;
	public float scrollSpeed;
	private bool scrollingForward;
	
	public void resetBackgrounds()
	{
		int numChildren = transform.childCount;
		float startX = -7.859533f;
		
		for (int i = 0; i < numChildren; i++)
		{
			GameObject pane = transform.GetChild(i).gameObject;
			Vector3 tempVector = new Vector3 (pane.transform.position.x, 
			                                  pane.transform.position.y, 
			                                  pane.transform.position.z);
			tempVector.x = startX + 10.0f*i;
			pane.transform.position = tempVector;
		}
	}
	
	void Update()
	{
		if (gameLogicObject.gameState == Constants.GAMESTATE_STARTGAME)
		{
			resetBackgrounds();
		}
		else if (gameLogicObject.gameState == Constants.GAMESTATE_PLAY && 
				 !gameLogicObject.isPaused)
		{
			scrollBackground();
			moveOffscreenBackgroundPaneToFront();
		}
	}
	
	void scrollBackground()
	{	
		float moveDistance = scrollSpeed * player.moveSpeed * Time.deltaTime;		
		int numChildren = transform.childCount;
		
		for (int i = 0; i < numChildren; i++)
		{
			GameObject pane = transform.GetChild(i).gameObject;
			pane.transform.Translate(-Vector3.right * moveDistance);
		}
		
		if (player.moveSpeed >= 0)
		{
			scrollingForward = true;
		}
		else
		{
			scrollingForward = false;
		}
	}

	void moveOffscreenBackgroundPaneToFront()
	{
		int numChildren = transform.childCount;
		
		for (int i = 0; i < numChildren; i++)
		{
			GameObject pane = transform.GetChild(i).gameObject;
			
			if (scrollingForward && isOffscreen(pane))
			{
				GameObject rightMostPane = getRightMostPane();
				movePaneToRight(pane, rightMostPane);
			}
			else if (!scrollingForward && isOffRightOfScreen(pane))
			{
				GameObject leftMostPane = getLeftMostPane();
				movePaneToLeft(pane, leftMostPane);
			}
		}
	}

	bool isOffscreen(GameObject pane)
	{
		Vector3 objectRightEdge = getObjectRightEdge(pane);

		if (hasObjectGoneOffScreen(objectRightEdge))
		{
			return true;
		}

		return false;
	}

	Vector3 getObjectRightEdge(GameObject obj)
	{
		Vector3 rightEdge = obj.transform.position;
		rightEdge.x += getObjectWidth(obj) * getObjectScale(obj);
		
		return rightEdge;
	}

	float getObjectWidth(GameObject obj)
	{
		return obj.GetComponent<Collider2D>().bounds.size.x;
	}

	float getObjectScale(GameObject obj)
	{
		return obj.transform.localScale.x;
	}
	
	bool hasObjectGoneOffScreen(Vector3 obj)
	{
		return Camera.main.WorldToScreenPoint(obj).x <= 0;
	}

	GameObject getRightMostPane()
	{
		float rightMostPosition = -1000.0f;
		GameObject rightMostPane = null;

		try
		{
			int children = transform.childCount;

			for (int i = 0; i < children; ++i)
			{
				GameObject pane = transform.GetChild(i).gameObject;
				if (pane.transform.position.x > rightMostPosition)
				{
					rightMostPane = pane;
					rightMostPosition = rightMostPane.transform.position.x;
				}
			}

			return rightMostPane;
		}
		catch (UnityException e)
		{
			Debug.Log ("No pane found " + e);
			return null;
		}
	}

	void movePaneToRight(GameObject thisPane, GameObject rightMostPane)
	{
		Vector3 tempVector = new Vector3 (thisPane.transform.position.x, 
		                                  thisPane.transform.position.y, 
		                                  thisPane.transform.position.z);
		tempVector.x = rightMostPane.transform.position.x + getObjectWidth(rightMostPane);
		thisPane.transform.position = tempVector;
	}
	
	bool isOffRightOfScreen(GameObject obj) {
		Vector3 position = obj.transform.position;
		return Camera.main.WorldToScreenPoint(position).x >= Screen.width;
	}
	
	GameObject getLeftMostPane()
	{
		float leftMostPosition = 1000.0f;
		GameObject leftMostPane = null;
		
		try
		{
			int children = transform.childCount;
			
			for (int i = 0; i < children; ++i)
			{
				GameObject pane = transform.GetChild(i).gameObject;
				if (pane.transform.position.x < leftMostPosition)
				{
					leftMostPane = pane;
					leftMostPosition = leftMostPane.transform.position.x;
				}
			}
			
			return leftMostPane;
		}
		catch (UnityException e)
		{
			Debug.Log ("No pane found " + e);
			return null;
		}
	}
	
	void movePaneToLeft(GameObject thisPane, GameObject leftMostPane)
	{
		Vector3 tempVector = new Vector3 (thisPane.transform.position.x, 
		                                  thisPane.transform.position.y, 
		                                  thisPane.transform.position.z);
		tempVector.x = leftMostPane.transform.position.x - getObjectWidth(leftMostPane);
		thisPane.transform.position = tempVector;
	}
}
