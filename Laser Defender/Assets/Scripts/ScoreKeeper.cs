using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	//instead of storing score in instant, we store it in template so template keeps track of score
	public static int score;
	
	private Text myText;
	
	void Start() {
		myText = GetComponent<Text>();
		Reset ();
	}
	
	public void Score(int points) {
		Debug.Log ("Point Scored");
		score += points;
		//Changes score int into a string everytime score increaes
		myText.text = score.ToString();
	}
	
	public static void Reset(){
		score = 0;
		
	}
}
