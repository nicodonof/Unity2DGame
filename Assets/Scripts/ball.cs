using System;
using UnityEngine;
using UnityEngine.UI;

public class ball : MonoBehaviour {

	Vector3 direction;

	public float speed = 0.5f;
	public GameObject tryAgain;
	public GameObject coins;
	public GameObject coinIcon;
	public bool isDead;
	public bool frozen;
	public int freezedCd = 0;
	private GameObject player;
	private float initSpeed;
	private bool collide;
	

	private Vector3 initPos;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		direction = new Vector3(1f,-1f,0);
		initPos = transform.position;
		initSpeed = speed;
		frozen = false;
		collide = true;
	}

	// Update is called once per frame
	void Update () {
        // speed = Mathf.Min(1f, speed - (score / 10000f))
		if(!player.GetComponent<Movement>().isDead && !frozen){
			transform.position += speed * Time.deltaTime * direction;
 		}

		if (player.GetComponent<Movement>().isDead) {
			transform.position = initPos;
			speed = initSpeed;
		}
		// Debug.Log(freezedCd);
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.name == "Player") {
			transform.position = other.transform.position;
			freezedCd = 1;
			frozen = true;
			speed *= 1.02f;
		}

		GameObject.Find("bounce").GetComponent<AudioSource>().Play();

		
		bool iswallh = other.gameObject.CompareTag("WallH");
		bool iswallv = other.gameObject.CompareTag("WallV");
		if(iswallh || iswallv) {
			speed *= 1.01f;
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
			if(((SpriteRenderer) other.gameObject.GetComponent("SpriteRenderer")).color == new Color(0.674f, 0.196f, 0.196f, 1f)){
				GameObject.Find("Player").GetComponent<Movement>().isDead = true;
                tryAgain.SetActive(true);
                coins.SetActive(true);
                coinIcon.SetActive(true);
                GameObject.Find("Background Music").GetComponent<AudioSource>().Stop();
                GameObject.Find("Die").GetComponent<AudioSource>().Play();
			}else{
				Movement mvtScr = GameObject.Find("Player").GetComponent<Movement>();
				mvtScr.score += 10;
				GameObject.Find("Score").GetComponent<Text>().text = mvtScr.score.ToString();
				GameObject aux = GameObject.Find("IndicatorPool").GetComponent<PoolManager>().getIndicator();
				GameObject cam = GameObject.Find("Main Camera");
				aux.GetComponent<Text>().text = "+5";
                aux.transform.position = cam.GetComponent<Camera>().WorldToScreenPoint(transform.position);
				aux.transform.position += new Vector3(25, 25, 0);
                ((SpriteRenderer) other.gameObject.GetComponent("SpriteRenderer")).color = new Color(0.674f,0.196f,0.196f,1f); //172 50 50 255
			}
		} else if(collide){
			speed *= 1.01f;
            Movement mvtScr = GameObject.Find("Player").GetComponent<Movement>();
			mvtScr.score += 5;
            GameObject.Find("Score").GetComponent<Text>().text = mvtScr.score.ToString();
            GameObject aux = GameObject.Find("IndicatorPool").GetComponent<PoolManager>().getIndicator();
            GameObject cam = GameObject.Find("Main Camera");
			aux.GetComponent<Text>().text = "+5";
            aux.transform.position = cam.GetComponent<Camera>().WorldToScreenPoint(transform.position);
            aux.transform.position += new Vector3(25, 25, 0);

			float ballMaxY = transform.position.y + transform.GetComponent<CircleCollider2D>().radius;
			float ballMaxX = transform.position.x + transform.GetComponent<CircleCollider2D>().radius;
			float ballMinY = transform.position.y - transform.GetComponent<CircleCollider2D>().radius;
			float ballMinX = transform.position.x - transform.GetComponent<CircleCollider2D>().radius;
		
			float otherMaxY = other.transform.position.y + transform.GetComponent<Collider2D>().bounds.size.y / 2;
			float otherMaxX = other.transform.position.x + transform.GetComponent<Collider2D>().bounds.size.x / 2;

			float otherMinY = other.transform.position.y - transform.GetComponent<Collider2D>().bounds.size.y / 2;
			float otherMinX = other.transform.position.x - transform.GetComponent<Collider2D>().bounds.size.x / 2;
			float diff = 0;
			float diff2 = 0;
			if(direction == new Vector3(1f,1f,0)){ //dir right-up
				if (ballMaxX > otherMinX && ballMinX <= otherMinX && transform.position.x < otherMinX) {
					//hit on the left
					direction = new Vector3(-1f, 1f, 0);
					diff = Math.Min(Math.Abs(transform.position.x - otherMinX), Math.Abs(transform.position.x - otherMaxX));
				}

				if (ballMaxY >= otherMinY && ballMinY < otherMinY && transform.position.y < otherMinY) {
					//hit on the bottom
					diff2 = Math.Min(Math.Abs(transform.position.y - otherMinY), Math.Abs(transform.position.y - otherMaxY));
					if(diff2 > diff)
						direction = new Vector3(1f, -1f, 0);
				}
			}
			if(direction == new Vector3(-1f, 1f, 0)) { //dir left-up
				if (ballMinX <= otherMaxX && ballMaxX > otherMaxX && transform.position.x > otherMaxX) {
					//hit on the right
					diff = Math.Min(Math.Abs(transform.position.x - otherMaxX), Math.Abs(transform.position.x - otherMinX));
					direction = new Vector3(1f, 1f, 0);
				}

				if (ballMaxY >= otherMinY && ballMinY < otherMinY && transform.position.y < otherMinY) {
					//hit on the bottom
					diff2 = Math.Min(Math.Abs(transform.position.y - otherMinY), Math.Abs(transform.position.y - otherMaxY));
					if(diff2 > diff)
						direction = new Vector3(-1f, -1f, 0);
				}
			}
			if(direction == new Vector3(1f, -1f, 0)) { //dir down-right
				if (ballMinY <= otherMaxY && ballMaxY > otherMaxY && transform.position.y > otherMaxY) {
					//hit on the top
					diff = Math.Min(Math.Abs(transform.position.y - otherMaxY), Math.Abs(transform.position.y - otherMinY));
					direction = new Vector3(1f, 1f, 0);
				}

				if (ballMaxX > otherMinX && ballMinX <= otherMinX && transform.position.x < otherMinX) {
					//hit on the left
					diff2 = Math.Min(Math.Abs(transform.position.x - otherMinX), Math.Abs(transform.position.x - otherMaxX));
					if(diff2 > diff) 
						direction = new Vector3(-1f, -1f, 0);
				}
			} 
			if(direction == new Vector3(-1f, -1f, 0)) { //dir down-left
				if (ballMinY <= otherMaxY && ballMaxY > otherMaxY && transform.position.y > otherMaxY) {
					//hit on the top
					diff = Math.Min(Math.Abs(transform.position.y - otherMaxY), Math.Abs(transform.position.y - otherMinY));
					direction = new Vector3(-1f, 1f, 0);
				}
				if (ballMinX <= otherMaxX && ballMaxX > otherMaxX && transform.position.x > otherMaxX) {
					//hit on the right
					diff2 = Math.Min(Math.Abs(transform.position.x - otherMaxX), Math.Abs(transform.position.x - otherMinX));
					if(diff2 > diff)
						direction = new Vector3(1f, -1f, 0);
				}
			}

			collide = false;
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		bool iswallh = other.gameObject.CompareTag("WallH");
		bool iswallv = other.gameObject.CompareTag("WallV");
		if (!iswallh && !iswallv) {
			collide = true;
		}
	}
}
