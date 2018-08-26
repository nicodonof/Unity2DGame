using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
	Vector3 direction;
    public float initCd = 0.75f;
    float cd;
    public float grid = 1.5f;
	public GameObject tail;
	public int score = 0;

	public GameObject scoreDisplay;

	List<GameObject> tails;

	void Start () {
        direction = new Vector3(1, 0, 0);
        cd = initCd;
		tails = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if(cd <= 0){
			addTail();
			transform.position += direction / grid;
			score++;
			cd = Math.Max(0.2f ,initCd - ( score / 10000f ));
			scoreDisplay.GetComponent<Text>().text = score.ToString();

			if(tails.Count > 0){
				GameObject toDel = tails[0];
				tails.RemoveAt(0);
				DestroyImmediate(toDel);
			}
		}
		else{
			cd -= Time.deltaTime;
		}
        if (Input.GetAxis("Horizontal") > 0 && direction.x == 0)
            direction = new Vector3(1, 0, 0);
        else if (Input.GetAxis("Horizontal") < 0 && direction.x == 0)
            direction = new Vector3(-1, 0, 0);
        else if (Input.GetAxis("Vertical") > 0 && direction.y == 0)
            direction = new Vector3(0, 1, 0);
        else if (Input.GetAxis("Vertical") < 0&& direction.y == 0)
            direction = new Vector3(0, -1, 0);
	}

    private void addTail(){
        GameObject aux = Instantiate(tail);
        aux.transform.position = transform.position;
        tails.Add(aux);
    }

    void OnCollisionEnter(Collision collision){
		print("HOla");
		collision.gameObject.transform.position = new Vector3(0,0,0);
	}
}
