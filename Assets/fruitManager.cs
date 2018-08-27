using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitManager : MonoBehaviour {

	public GameObject fruit;

	// Use this for initialization
	void Start () {
		this.fruit = Instantiate(fruit);
	}

	// Update is called once per frame
	void Update () {
		this.newFruitIfNeeded();
	}

 	void newFruitIfNeeded(){
		if(!this.fruit.activeSelf){
			this.fruit.transform.position = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0);
			this.fruit.SetActive(true);
		}
 }
}
