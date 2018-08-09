using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	int c;
	public int minC = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.C)){
			c++;
			GameObject.Find("Coins").GetComponent<Text>().text = c.ToString();
		}
		
		if(Input.GetKeyDown(KeyCode.Return)){
			if(c < minC){
				Debug.Log("Cant, need coins.");
			} else {
				Debug.Log("Load next scene.");
				// SceneManager.LoadScene("Game");
			}
		}

	}
}
