using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
	public GameObject tryAgain;
	public float initCd = 0.75f;
	public float cd;
	public float grid = 1.75f;
	public GameObject tail;
	public int score;
	public bool isDead;
	public AudioSource cant;
	public AudioSource start;
	public AudioSource coin;
	public GameObject[] walls; //top,right,bottom,left
	public int tailCount;
	private Vector3 direction;
	private GameObject scoreDisplay;
	private Vector3 initPos;
	private int currDir;
	private int newDirection; //0 right, 1 left, 2 up, 3 bottom
	private Vector3 newVector;
	private List<GameObject> tails;
	private bool encolate;
	private int encoDir;
	private Vector3 encoVector;
    private int scoreCd;

    void Start () {
		direction = new Vector3(1, 0, 0);
		newVector = direction;
		score = 0;
		currDir = 0;
		cd = initCd;
		tails = new List<GameObject>();
		tryAgain.SetActive(false);
		tailCount = 0;
		initPos = new Vector3(1/3f-0.15f, 1/3f-0.15f, 0f);
		scoreDisplay = GameObject.Find("Score");
		encoDir = -1;
		scoreCd = 0;
	}

	void Restart() {
		direction = new Vector3(1,0,0);
		newVector = direction;
		newDirection = 0;
		encoDir = -1;
		currDir = 0;
		score = 0;
		scoreCd = 0;
		cd = initCd;
		foreach (var t in tails) {
			DestroyImmediate(t);
		}
		foreach(GameObject wall in walls){
			((SpriteRenderer) wall.GetComponent("SpriteRenderer")).color = Color.white;
		}
		tails.Clear();
		transform.position = initPos;
		tryAgain.SetActive(false);
		scoreDisplay.GetComponent<Text>().text = "0";
		tailCount = 0;
	}

	// Update is called once per frame
	void Update () {
		if (newDirection >= 0) {
			encolate = true;
		} else if (encoDir >= 0) {
			newVector = encoVector;
			newDirection = encoDir;
			encoDir = -1;
		} else {
			encolate = false;
		}
		
		
		if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && (direction.x >= 0 || encolate)) {
			if (newDirection != 0 && encolate) {
				encoVector = new Vector3(1,0,0);
				encoDir = 0;
			} else {
				newVector = new Vector3(1, 0, 0);
				newDirection = 0;
			}
		}
		if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && (direction.x <= 0 || encolate)) {
			if (newDirection != 1 && encolate) {
				encoVector = new Vector3(-1, 0, 0);
				encoDir= 1;
			} else {
				newVector = new Vector3(-1, 0, 0);
				newDirection = 1;	
			}
		}
		if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && (direction.y >= 0 || encolate)) {
			if (newDirection != 2 && encolate) {
				encoVector = new Vector3(0,1,0);
				encoDir = 2;
			} else {
				newVector = new Vector3(0, 1, 0);
				newDirection = 2;
			}
		}
		if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && (direction.y <= 0 || encolate)) {
			if (newDirection != 3 && encolate) {
				encoVector = new Vector3(0,-1,0);
				encoDir = 3;
			} else {
				newVector = new Vector3(0, -1, 0);
				newDirection = 3;
			}
		}
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
			if(tails.Count > tailCount){
				GameObject move = tails[0];
				tails.RemoveAt(0);
                move.transform.position = transform.position;
				tails.Add(move);
			} else {
				addTail();
			}
			newDirection = -1;
			transform.position += direction / grid;
			if(scoreCd > 0){
				scoreCd--;
			} else {
				scoreCd = 5;
				score++;
				scoreDisplay.GetComponent<Text>().text = score.ToString();
			}
			cd = Math.Max(0.000001f ,initCd - (float) Math.Log10(score) * 0.05f);

			
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
		
	}

  private void addTail(){
		GameObject aux = Instantiate(tail);
		aux.transform.position = transform.position;
		tails.Add(aux);
  }

  void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.CompareTag("Player")
				|| collision.gameObject.CompareTag("WallV")
				|| collision.gameObject.CompareTag("WallH")) {
			isDead = true;
			tryAgain.SetActive(true);
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.CompareTag("Fruit") && collider.gameObject.activeSelf){
			collider.gameObject.SetActive(false);
			tailCount++;
			shuffle(walls);
			foreach(GameObject wall in walls){
				if(((SpriteRenderer) wall.GetComponent("SpriteRenderer")).color == Color.red){
					((SpriteRenderer) wall.GetComponent("SpriteRenderer")).color = Color.white;
					break;
				}
			}
			score += 10;
			// Debug.Log(GameObject.Find("IndicatorPool").ToString());
			GameObject aux = GameObject.Find("IndicatorPool").GetComponent<PoolManager>().getIndicator();
			GameObject cam = GameObject.Find("Main Camera");
            aux.GetComponent<Text>().text = "+10";
			// cam.GetComponent<Camera>().WorldToScreenPoint(transform.position);
			aux.transform.position = cam.GetComponent<Camera>().WorldToScreenPoint(transform.position);
			scoreDisplay.GetComponent<Text>().text = score.ToString();

    	}
	}

	private void shuffle<GameObject>(IList<GameObject> list) {
		System.Random rng = new System.Random();
		int n = list.Count;
		while (n > 1) {
				n--;
				int k = rng.Next(n + 1);
				GameObject value = list[k];
				list[k] = list[n];
				list[n] = value;
		}
	}
}
