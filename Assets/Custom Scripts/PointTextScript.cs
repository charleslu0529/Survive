using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTextScript : MonoBehaviour {

	public float DestroyTimer = 0.8f;
	public float LerpTime = 0.5f;
	public float MoveUpDist = 0.02f;

	// Use this for initialization
	void Start () {
		Object.Destroy(gameObject, DestroyTimer);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = Vector3.Lerp( gameObject.transform.position,
			new Vector3( gameObject.transform.position.x , ( gameObject.transform.position.y + MoveUpDist ), 0f), LerpTime);
	}
}
