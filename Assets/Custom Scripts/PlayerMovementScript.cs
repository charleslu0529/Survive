using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

	public float MovementSpeed = 5f;
	public Camera MainCamera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up* Time.deltaTime * MovementSpeed * Input.GetAxisRaw("Vertical"));
		transform.Translate(Vector3.right* Time.deltaTime * MovementSpeed * Input.GetAxisRaw("Horizontal"));
		

		if(Input.GetMouseButtonDown(0)){

			Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
			mousePos = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(mousePos);
			Vector3 moveDir = new Vector3( (mousePos - transform.position).x, (mousePos - transform.position).y, 0);
		}
	}
}
