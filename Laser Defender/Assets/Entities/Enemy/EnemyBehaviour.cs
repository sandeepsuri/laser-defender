using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float health = 150;
	public float projectileSpeed = 0.9f;
	public float shotsPerSecond = 0.5f;
	
	public int scoreValue = 150;
	
	public GameObject projectile;
	public GameObject explode;
	
	public AudioClip enemyLaser;
	public AudioClip enemyDestroyed;
	
	
	private ScoreKeeper scoreKeeper;
	
	void Start() {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	void Update() {
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability) {
			Fire ();
		}
	}

	
	//Enemy auto fire
	void Fire() {
		Vector3 offset = new Vector3 (0, -1f, 0);
		Vector3 startPosition = transform.position + offset;
		GameObject enemyBeam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		enemyBeam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
		//enemy laser sound
		AudioSource.PlayClipAtPoint (enemyLaser, transform.position);
	}		
			
				
						
	void OnTriggerEnter2D(Collider2D collider){
			Projectile missile = collider.gameObject.GetComponent<Projectile>();
			if(missile){
				health -= missile.GetDamage();
				missile.Hit();
				//enemy dies if less than health
				if (health <= 0) {
					Death ();
				}
			}
	}
	
	
	void Death () {
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
		explodeEnemy();	
		//enemy destroyed noise
		AudioSource.PlayClipAtPoint (enemyDestroyed, transform.position);
	}
	
	//Explosion comes out once enemy is destroyed
	void explodeEnemy () {
		GameObject smokeShip = Instantiate(explode, transform.position, Quaternion.identity) as GameObject;
		smokeShip.GetComponent<ParticleSystem>().startColor =  gameObject.GetComponent<SpriteRenderer>().color;
		//Changes the particles colour into 
		smokeShip.GetComponent<ParticleSystem>().startColor = new Color(241, 182, 0);
	}
	
	
}

