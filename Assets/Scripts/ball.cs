using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour {

	Vector3 direction;

	public float speed = 0.5f;
	// public GameObject wall;

	public GameObject tryAgain;

	public bool isDead;

	// bool freezed = false;

	float freezedCd = 1f;

	// Use this for initialization
	void Start () {
		direction = new Vector3(1f,-1f,0);
	}

	// Update is called once per frame
	void Update () {
        // speed = Mathf.Min(1f, speed - (score / 10000f))
		if(!GameObject.Find("Player").GetComponent<Movement>().isDead && freezedCd <= 0){
			transform.position += speed * Time.deltaTime * direction;       
		} else {
			freezedCd -= Time.deltaTime;
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		// if(other.gameObject.tag == "Player" && freezed){
		// 	freezed = false;
		// }
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player" && freezedCd > 0){
            freezedCd = 1;
		}
		if(other.gameObject.name == "Player"){
            freezedCd = 1;
		}
        float ballMaxY = transform.position.y + transform.GetComponent<CircleCollider2D>().radius;
        float ballMaxX = transform.position.x + transform.GetComponent<CircleCollider2D>().radius;

        float ballMinY = transform.position.y - transform.GetComponent<CircleCollider2D>().radius;
        float ballMinX = transform.position.x - transform.GetComponent<CircleCollider2D>().radius;

        float otherMaxY = other.transform.position.y + transform.GetComponent<Collider2D>().bounds.size.y / 2;
        float otherMaxX = other.transform.position.x + transform.GetComponent<Collider2D>().bounds.size.x / 2;

        float otherMinY = other.transform.position.y - transform.GetComponent<Collider2D>().bounds.size.y / 2;
        float otherMinX = other.transform.position.x - transform.GetComponent<Collider2D>().bounds.size.x / 2;

		Transform othTr = other.transform;
		bool iswallh = other.gameObject.tag == "WallH";
		bool iswallv = other.gameObject.tag == "WallV";
		if(iswallh || iswallv){
			if(direction == new Vector3(1f,1f,0)){
				if(iswallh)
					direction = new Vector3(1f, -1f, 0);
				else
					direction = new Vector3(-1f, 1f, 0);
			} else if((direction == new Vector3(-1f, 1f, 0))) {
				if(iswallh)
					direction = new Vector3(-1f, -1f, 0);
				else
					direction = new Vector3(1f, 1f, 0);
			}else if((direction == new Vector3(1f, -1f, 0))) {
				if(iswallh)
					direction = new Vector3(1f, 1f, 0);
				else
					direction = new Vector3(-1f, -1f, 0);
			} else {
				if (iswallh)
					direction = new Vector3(-1f, 1f, 0);
				else
					direction = new Vector3(1f, -1f, 0);
			}
			if(((SpriteRenderer) other.gameObject.GetComponent("SpriteRenderer")).color == Color.red){
				GameObject.Find("Player").GetComponent<Movement>().isDead = true;
				tryAgain.SetActive(true);
			}else{
				((SpriteRenderer) other.gameObject.GetComponent("SpriteRenderer")).color = Color.red;
			}
		} else {
			if(direction == new Vector3(1f,1f,0)){
				if((transform.position.y > otherMinY && transform.position.y < otherMaxY))
					direction = new Vector3(-1f, 1f, 0);
				else
					direction = new Vector3(1f, -1f, 0);
			} else if((direction == new Vector3(-1f, 1f, 0))) {
				if(transform.position.y > otherMinY && transform.position.y < otherMaxY)
					direction = new Vector3(1f, 1f, 0);
				else
					direction = new Vector3(-1f, -1f, 0);
			}else if((direction == new Vector3(1f, -1f, 0))) {
				if(transform.position.x > otherMinX && transform.position.x < otherMaxX)
					direction = new Vector3(1f, 1f, 0);
				else
					direction = new Vector3(-1f, -1f, 0);
			} else {
				if ((transform.position.x > otherMinX && transform.position.x < otherMaxX))
					direction = new Vector3(-1f, 1f, 0);
				else
					direction = new Vector3(1f, -1f, 0);
			}
		}
	}

}
