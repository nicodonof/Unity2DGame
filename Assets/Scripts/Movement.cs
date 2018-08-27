using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
	Vector3 direction;
	public GameObject tryAgain;
    public float initCd = 0.75f;
    float cd;
    public float grid = 1.75f;
	public GameObject tail;
	public int score;
	public bool isDead;
	public GameObject scoreDisplay;
	public Vector3 initPos;
	private int currDir;
	List<GameObject> tails;
	private int newDirection; //0 right, 1 left, 2 up, 3 bottom
	private Vector3 newVector;
	public AudioSource cant;
	public AudioSource start;
	public AudioSource coin;

	private int tailCount;

	void Start () {
        direction = new Vector3(1, 0, 0);
		newVector = direction;
		score = 0;
		currDir = 0;
        cd = initCd;
		tails = new List<GameObject>();
		tryAgain = Instantiate(tryAgain);
		tryAgain.SetActive(false);
		isDead = false;
		tailCount = 0;
	}

	void Restart() {
		direction = new Vector3(1,0,0);
		newVector = direction;
		newDirection = 0;
		currDir = 0;
		score = 0;
		cd = initCd;
		foreach (var t in tails) {
			DestroyImmediate(t);
		}
		tails.Clear();
		transform.position = initPos;
		tryAgain.SetActive(false);
		scoreDisplay.GetComponent<Text>().text = "0";
		tailCount = 0;
	}

	// Update is called once per frame
	void Update () {
		if(cd <= 0 && !isDead) {
			switch (newDirection) {
				case 0:
					if (currDir != 1) {
						direction = newVector;
						currDir = newDirection;
					}
					break;
				case 1:
					if (currDir != 0) {
						direction = newVector;
						currDir = newDirection;
					}
					break;
				case 2:
					if (currDir != 3) {
						direction = newVector;
						currDir = newDirection;
					}
					break;
				case 3:
					if (currDir != 2) {
						direction = newVector;
						currDir = newDirection;
					}
					break;
			}
			addTail();
			transform.position += direction / grid;
			score++;
			cd = Math.Max(0.2f ,initCd - ( score / 10000f ));
			scoreDisplay.GetComponent<Text>().text = score.ToString();

			if(tails.Count > tailCount){
				GameObject toDel = tails[0];
				tails.RemoveAt(0);
				DestroyImmediate(toDel);
			}
		}
		else{
			cd -= Time.deltaTime;
		}

		if (isDead) {
			if (Input.GetKeyDown(KeyCode.Return)) {
				if (LocalStorage.coins >= 2) {
					Restart();
					isDead = false;
				} else {
					cant.Play();
				}
				return;
			}
			if (Input.GetKeyDown(KeyCode.C)) {
				LocalStorage.coins++;
				coin.Play();
			}
		}
		if (Input.GetAxis("Horizontal") > 0 && direction.x == 0) {
			newVector = new Vector3(1, 0, 0);
			newDirection = 0;
		}
        else if (Input.GetAxis("Horizontal") < 0 && direction.x == 0) {
			newVector = new Vector3(-1, 0, 0);
			newDirection = 1;
		}
        else if (Input.GetAxis("Vertical") > 0 && direction.y == 0) {
			newVector = new Vector3(0, 1, 0);
			newDirection = 2;
		}
        else if (Input.GetAxis("Vertical") < 0 && direction.y == 0) {
			newVector = new Vector3(0, -1, 0);
			newDirection = 3;
		}
	}

    private void addTail(){
        GameObject aux = Instantiate(tail);
        aux.transform.position = transform.position;
        tails.Add(aux);
//	    tailCount++;
    }

    void OnCollisionEnter2D(Collision2D collision){
	    if (collision.gameObject.CompareTag("Player")) {
		    isDead = true;
		    tryAgain.SetActive(true);
	    }
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.CompareTag("Fruit") && collider.gameObject.activeSelf){
			collider.gameObject.SetActive(false);
			tailCount++;
		}
	}
}
