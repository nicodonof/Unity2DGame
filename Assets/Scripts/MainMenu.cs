using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public int minC = 2;

	public AudioSource start;
	public AudioSource cant;

	// Use this for initialization
	void Start () {
        GameObject.Find("Coins").GetComponent<Text>().text = LocalStorage.coins.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.C)){
			LocalStorage.coins++;
			GameObject.Find("Coins").GetComponent<Text>().text = LocalStorage.coins.ToString();
			GetComponent<AudioSource>().Play();
		}
		
		if(Input.GetKeyDown(KeyCode.Return)){
			if(LocalStorage.coins < minC){
				Debug.Log("Cant, need coins.");
				cant.Play();
			} else {
                LocalStorage.coins -= minC;
				GameObject.Find("Coins").GetComponent<Text>().text = LocalStorage.coins.ToString();
				start.Play();
				SceneManager.LoadScene("Game", LoadSceneMode.Single);
			}
		}

	}
}
