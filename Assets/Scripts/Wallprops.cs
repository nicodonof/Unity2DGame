using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallprops : MonoBehaviour {

	private int weight = 0;

	private Color[] colorArray;


	// Use this for initialization
	void Start () {
		colorArray = new Color[7];
		colorArray[0] = new Color(0.4156f,0.7450f,0.1882f);
		colorArray[1] = new Color(0.3568f,0.4313f,0.8823f);
		colorArray[2] = new Color(0.4627f,0.2588f,0.5411f);
		colorArray[3] = new Color(0.9843f,0.9490f,0.2117f);
		colorArray[4] = new Color(0.8745f,0.4431f,0.1490f);
		colorArray[5] = new Color(0.8431f,0.4823f,0.7294f);
		// colorArray[6] = new Color();
		weight = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getWeight(){
		return weight;
	}

	public Color addWeight(){
		weight++;
		return colorArray[weight];
	}

	public Color getColor(){
		return colorArray[weight];
	}

	public Color restart(){
		weight = 0;
		return colorArray[weight % 6];
	}

}
