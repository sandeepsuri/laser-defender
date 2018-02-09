using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

	// Gizmos are in the editor
	void OnDrawGizmos () {
		// Draws a circle to show object
		Gizmos.DrawWireSphere(transform.position, 1);
	}
}
