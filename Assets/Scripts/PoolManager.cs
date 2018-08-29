using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

	public List<GameObject> indicators;
	
	public GameObject indicator;

	public int size = 5;
	// Use this for initialization
	void Start () {
		indicators = new List<GameObject>();
		for (int i = 0; i < size; i++){
            GameObject aux = Instantiate(indicator);
            aux.transform.SetParent(FindObjectOfType<Canvas>().transform);
			aux.SetActive(false);
			indicators.Add(aux);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject getIndicator(){
		//Lo cambia de lugar para darle tiempo a mostrarse
		GameObject aux;
		aux = indicators[0];
		indicators.RemoveAt(0);
		aux.SetActive(false); // Reseteara la animation?
		aux.SetActive(true);
		indicators.Add(aux);
		return aux;
	}

	public void returnIndicator(GameObject indicator){
		indicators[size] = indicator;
		size++;
	}

}
