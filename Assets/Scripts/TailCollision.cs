using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailCollision : MonoBehaviour {

	private ball ballScript;
	private Collider2D ballColl;
	private GameObject ball;

	private Collider2D coll;
	// Use this for initialization
	void Start () {
		ball = GameObject.Find("Ball");
		ballColl = ball.GetComponent<Collider2D>();
		ballScript = ball.GetComponent<ball>();
		coll = GetComponent<Collider2D>();
	}
	
	private void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.name == "Ball" && ballScript.frozen) {
			ballScript.freezedCd--;
			ballScript.frozen = false;
		}
	}
	
	
}
