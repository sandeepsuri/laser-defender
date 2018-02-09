using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float damage = 200f;
	
	
	public float GetDamage() {
		//returns damage we deal
		return damage;
	}
	
	
	public void Hit() {
		Destroy(gameObject);
	}
}
