﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

	public List<GameObject> indicators;
	
	public GameObject indicator;

	public int size = 3;
	// Use this for initialization
	void Start () {
		indicators = new List<GameObject>();
		for (int i = 0; i < size; i++){
			indicators.Add(Instantiate(indicator));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	GameObject getIndicator(){
		GameObject aux = indicators[0];
        indicators.RemoveAt(0); 
		return aux;
	}

	void returnIndicator(GameObject indicator){
		indicators[size] = indicator;
		size++;
	}

}
