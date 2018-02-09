using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f; 
	public float Enemyspeed = 2f;
	public float padding = 0.5f;
	public float spawnDelay = 0.5f;
	
	
	private bool movingRight = true;
	private float xMin;
	private float xMax;
	
	
	void Start () {
		//distance b/w camera and the object
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		/* Lets us act on the main camera; Viewport to WorldPoint returns a worldpoint coordinate */
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
		
		xMax = rightEdge.x + padding;
		xMin = leftEdge.x - padding;
		SpawnUntilFull();
	
	}
	
	void spawnEnemies() {
		//repeats a group of statements in each element in an array or object collection
		foreach(Transform child in transform) {
			//This instantiate object is returning a gameobject; this function makes enemy spawn
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			
			//transform of w.e this enemy spawner script is attached to; the enemyFormation 
			enemy.transform.parent = child;
		}
	}  
	
	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition();

		if (freePosition) {
			//This instantiates the object is returning a gameobject; this function makes enemies spawn
			GameObject enemy = Instantiate(enemyPrefab, freePosition.transform.position, Quaternion.identity) as GameObject;
			//transform of w.e this enemy spawner script is attached to; the enemyFormation 
			enemy.transform.parent = freePosition;
		}
		
		if(NextFreePosition()) {
			//Use Invoke method to add a little delay
			Invoke ("SpawnUntilFull", spawnDelay);	
		}
	}
	
	
	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	
	void Update () {
		if (movingRight) {
			transform.position += Vector3.right * Enemyspeed * Time.deltaTime;
		}
		
		else {
			transform.position += Vector3.left * Enemyspeed * Time.deltaTime;
		}
		
		// Restricting enemy to gamespace
		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);
		
		/* if enemy less than xMin, it will move right
		   if enemy is more than xMax, it will move left
		 */
		if(leftEdgeOfFormation < xMin) {
			movingRight = true;
		}
		else if(rightEdgeOfFormation > xMax){
			movingRight = false;
		}
		
		//Check to see if all enemies are dead
		if(AllMembersDead()) {
			Debug.Log ("Empty Formation");
			SpawnUntilFull();
		}
		
	}
	
	
	
	Transform NextFreePosition() {
		foreach(Transform childPositionGameObject in transform) {
			if(childPositionGameObject.childCount ==  0) {
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	
	
	bool AllMembersDead() {
		//going over every child position in the transform og the formation
		foreach(Transform childPositionGameObject in transform) {
			//counting to see if all enemies are dead
			if( childPositionGameObject.childCount > 0) {
				return false;
			}		
		}
		return true; //every member is dead
	}
				
}


