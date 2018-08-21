using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	Vector3 direction;
	public float initCd = 0.75f;
	float cd;
	public float grid = 2f;

	// Use this for initialization
	void Start () {
		direction = new Vector3(1,0,0);
		cd = initCd;
	}
	
	// Update is called once per frame
	void Update () {
		if(cd <= 0){
			transform.position += direction / grid;
			cd = initCd;
		}
		else{
			cd -= Time.deltaTime;
		}
		if(Input.GetAxis("Horizontal") > 0 )
            direction = new Vector3(1, 0, 0);
		else if(Input.GetAxis("Horizontal") < 0)
            direction = new Vector3(-1, 0, 0);
		else if(Input.GetAxis("Vertical") > 0 )
            direction = new Vector3(0, 1, 0);
		else if(Input.GetAxis("Vertical") < 0)
            direction = new Vector3(0, -1, 0);
	}
}
