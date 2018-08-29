using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ball : MonoBehaviour {

	Vector3 direction;

	public float speed = 0.5f;
	public GameObject tryAgain;
	public bool isDead;
	private float freezedCd = 1f;
	private GameObject player;
	private float initSpeed;

	private Vector3 initPos;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		direction = new Vector3(1f,-1f,0);
		initPos = transform.position;
		initSpeed = speed;
	}

	// Update is called once per frame
	void Update () {
        // speed = Mathf.Min(1f, speed - (score / 10000f))
		if(!player.GetComponent<Movement>().isDead && freezedCd <= 0){
			transform.position += speed * Time.deltaTime * direction;
		} else {
			freezedCd -= Time.deltaTime;
		}

		if (player.GetComponent<Movement>().isDead) {
			transform.position = initPos;
			speed = initSpeed;
		}
		// Debug.Log(freezedCd);
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.name == "Player"){
			Movement aux = other.gameObject.GetComponent<Movement>();
			freezedCd = (aux.tailCount + 1) * 0.2f;
			speed *= 1.02f;
		}

		GameObject.Find("bounce").GetComponent<AudioSource>().Play();

        float otherMaxY = other.transform.position.y + transform.GetComponent<Collider2D>().bounds.size.y / 2;
        float otherMaxX = other.transform.position.x + transform.GetComponent<Collider2D>().bounds.size.x / 2;

        float otherMinY = other.transform.position.y - transform.GetComponent<Collider2D>().bounds.size.y / 2;
        float otherMinX = other.transform.position.x - transform.GetComponent<Collider2D>().bounds.size.x / 2;

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
			if(((SpriteRenderer) other.gameObject.GetComponent("SpriteRenderer")).color == Color.red){
				GameObject.Find("Player").GetComponent<Movement>().isDead = true;
				tryAgain.SetActive(true);
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
		} else {
			speed *= 1.01f;
            Movement mvtScr = GameObject.Find("Player").GetComponent<Movement>();
			mvtScr.score += 5;
            GameObject.Find("Score").GetComponent<Text>().text = mvtScr.score.ToString();
            GameObject aux = GameObject.Find("IndicatorPool").GetComponent<PoolManager>().getIndicator();
            GameObject cam = GameObject.Find("Main Camera");
			aux.GetComponent<Text>().text = "+5";
            aux.transform.position = cam.GetComponent<Camera>().WorldToScreenPoint(transform.position);
            aux.transform.position += new Vector3(25, 25, 0);
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
