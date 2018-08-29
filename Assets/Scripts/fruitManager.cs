﻿using UnityEngine;

public class fruitManager : MonoBehaviour {

	public GameObject fruit;


	void Start () {
		fruit = Instantiate(fruit);
		fruit.transform.position = new Vector3(Random.Range(-18, 20)/3f-0.15f, Random.Range(-13, 15)/3f-0.15f, 0);

	}
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		newFruitIfNeeded();
	}

 	void newFruitIfNeeded() {
		if(!fruit.activeSelf){
			fruit.transform.position = new Vector3(Random.Range(-18, 20)/3f-0.15f, Random.Range(-13, 15)/3f-0.15f, 0);
			fruit.SetActive(true);
		}
 }
}
