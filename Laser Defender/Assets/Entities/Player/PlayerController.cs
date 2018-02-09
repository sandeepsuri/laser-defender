using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private float speed = 20f;
	
	public float health = 250f ;
	public float padding = 0.5f;
	public float projectileSpeed = 5f;
	public GameObject laser; 
	public float firingRate = 0.2f;
	public AudioClip laserSound;
	public AudioClip playerDeath;
	
	float xMin;
	float xMax;
	
	void Start() {
		//distance b/w camera and the object
		float distance = transform.position.z - Camera.main.transform.position.z;
		
		/* Lets us act on the main camera; Viewport to WorldPoint returns a worldpoint coordinate */
		Vector3 leftMostPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightMostPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		                                                     xMin = leftMostPos.x + padding;
		xMax = rightMostPos.x - padding;
		
		}
		
	void Fire() {
		//So player laser doesn't spawn on top of the ship and destroy it
		Vector3 offset = new Vector3(0, 1, 0);
		
		GameObject beam = Instantiate(laser, transform.position + offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint (laserSound, transform.position);
	}	
	
	
	void Update () {
		// Shoot out lasers
		if(Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, firingRate);
			}
		
		else if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
			}
	
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {	
			//Moves the block with speed reletive to time; Time.deltaTime = time it takesb/w frame updates its independent of the framerate						
			
			//transform.position += new Vector3 (-speed * Time.deltaTime, 0f, 0f);
			transform.position += Vector3.left * speed * Time.deltaTime;
			
			}	

		else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) { 
			//transform.position += new Vector3 (speed * Time.deltaTime, 0f, 0f);
			transform.position += Vector3.right * speed * Time.deltaTime;
		
			}
		
		// Restricts the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax); 	
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);	
		
	}
	
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0) {
				PlayerDeath();
			}
		}
	}

	void PlayerDeath () {
		Destroy(gameObject);
		AudioSource.PlayClipAtPoint (playerDeath, transform.position);
		//Goes to End Screen
		LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");
	}

}
