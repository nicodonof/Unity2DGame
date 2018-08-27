using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	GameObject fruit;

	// Use this for initialization
	void Start () {
		this.fruit = GameObject.Find("Fruit");
	}

	// Update is called once per frame
	void Update () {
		this.checkForFruit();
	}

	void checkForFruit(){
		if(this.fruit.activeSelf
			&& this.inRange(this.transform.position.x, this.fruit.transform.position.x)
			&& this.inRange(this.transform.position.y, this.fruit.transform.position.y)){
			this.fruit.SetActive(false);
		}
	}

	bool inRange(float position1, float position2){
		return (position1 <= position2 + 0.25f) && (position1 >= position2 - 0.25f);
	}
}
