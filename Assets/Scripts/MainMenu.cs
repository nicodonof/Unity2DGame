using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	int c;
	public int minC = 2;

	public AudioSource start;
	public AudioSource cant;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.C)){
			c++;
			LocalStorage.coins = c;
			GameObject.Find("Coins").GetComponent<Text>().text = LocalStorage.coins.ToString();
			GetComponent<AudioSource>().Play();
		}
		
		if(Input.GetKeyDown(KeyCode.Return)){
			if(c < minC){
				Debug.Log("Cant, need coins.");
				cant.Play();
			} else {
				c -= minC;
                LocalStorage.coins = c;
				GameObject.Find("Coins").GetComponent<Text>().text = c.ToString();
				start.Play();
				SceneManager.LoadScene("Game", LoadSceneMode.Single);
			}
		}

	}
}
