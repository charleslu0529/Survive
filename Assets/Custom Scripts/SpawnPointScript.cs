using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour {


	public GameObject Enemy;
	public float SpawnTimer = 3f;
	public float ColourIntervalDivision = 10f;
	public Color AlertColour = Color.red;
	float localSpawnTimer;
	Color originalColour;
	float redColourChangeInterval;
	float greenColourChangeInterval;
	float blueColourChangeInterval;
	int colourDirection;
	// Use this for initialization
	void Start () {
		colourDirection = 1;
		localSpawnTimer = SpawnTimer;
		originalColour = gameObject.GetComponent<SpriteRenderer>().color;
		redColourChangeInterval = ( AlertColour.r - originalColour.r ) / ColourIntervalDivision;
		greenColourChangeInterval = ( AlertColour.g - originalColour.g ) / ColourIntervalDivision;
		blueColourChangeInterval = ( AlertColour.b - originalColour.b ) / ColourIntervalDivision;
	}
	
	// Update is called once per frame
	void Update () {
		localSpawnTimer -= Time.deltaTime;

		if(localSpawnTimer < 1f)
		{
			gameObject.GetComponent<SpriteRenderer>().color = new Color(originalColour.r + ( redColourChangeInterval * colourDirection) ,
									originalColour.g  + ( greenColourChangeInterval * colourDirection) ,
									originalColour.b  + ( blueColourChangeInterval * colourDirection) , 1f);
		}

		if( gameObject.GetComponent<SpriteRenderer>().color.r >= AlertColour.r )
		{
			colourDirection = -1;
		}
			else if(gameObject.GetComponent<SpriteRenderer>().color.r <= originalColour.r)
		{
			colourDirection = 1;
		}

		if(localSpawnTimer < 0)
		{
			Instantiate(Enemy, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
