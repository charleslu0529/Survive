using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public GameObject Player;
	public float CameraZPosition = -10f;
	public float CameraSmoothTime = 0.5f;
	Vector3 targetPosition;
	Vector3 cameraVelocity = Vector3.zero;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(Player != null){
			targetPosition  = new Vector3(Player.transform.position.x, Player.transform.position.y, CameraZPosition);
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraVelocity, CameraSmoothTime);
		}
		
	}
}