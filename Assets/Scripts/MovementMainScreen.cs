using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMainScreen : MonoBehaviour {

    public float initCd = 0.75f;
    public float cd;
    public float grid = 1.75f;
    public GameObject tail;
    public int tailCount;
    public Vector3 direction = new Vector3(-1, 0, 0);
    private Vector3 initPos;
    private int currDir;
    private Vector3 newVector;
    private List<GameObject> tails;

	// Use this for initialization
	void Start () {
        // direction ;
        newVector = direction;
        currDir = 0;
        cd = initCd;
        tails = new List<GameObject>();
        // initPos = new Vector3(1 / 3f - 0.15f, 1 / 3f - 0.15f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(transform.position.x) > 15){
			direction *=  -1;
		}
        if (cd <= 0){
            if (tails.Count > tailCount){
                GameObject move = tails[0];
                tails.RemoveAt(0);
                move.transform.position = transform.position;
                tails.Add(move);
            }else{
                addTail();
            }
            transform.position += direction / grid;

            cd = 0.035f;
        }
        else{
            cd -= Time.deltaTime;
        }
	}

	private void addTail(){
		GameObject aux = Instantiate(tail);
		aux.transform.position = transform.position;
		tails.Add(aux);
	}
}

