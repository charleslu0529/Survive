using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

	public LayerMask CastLayer;
	public float CastLength = 0.5f;
	public float MovementSpeed = 5f;
	public Camera MainCamera;
	RaycastHit2D castLeft;
	RaycastHit2D castRight;
	RaycastHit2D castDown;
	RaycastHit2D castUp;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// Ray cast to stop player from jittering
		castLeft = Physics2D.Raycast(transform.position, Vector3.left, CastLength, CastLayer);
		castRight = Physics2D.Raycast(transform.position, Vector3.right, CastLength, CastLayer);
		castDown = Physics2D.Raycast(transform.position, Vector3.down, CastLength, CastLayer);
		castUp = Physics2D.Raycast(transform.position, Vector3.up, CastLength, CastLayer);

		if(castLeft.collider != null)
		{
        	if( Input.GetAxisRaw("Horizontal") < 0){
        		transform.Translate(Vector3.up* Time.deltaTime * MovementSpeed * Input.GetAxisRaw("Vertical"));
        	}else{
        		transform.Translate(Vector3.up* Time.deltaTime * MovementSpeed * Input.GetAxisRaw("Vertical"));
				transform.Translate(Vector3.right* Time.deltaTime * MovementSpeed * Input.GetAxisRaw("Horizontal"));
        	}
       	}

       	if(castRight.collider != null)
       	{
       		if( Input.GetAxisRaw("Horizontal") > 0){
        		transform.Translate(Vector3.up* Time.deltaTime * MovementSpeed * Input.GetAxisRaw("Vertical"));
        	}else{
        		transform.Translate(Vector3.up* Time.deltaTime * MovementSpeed * Input.GetAxisRaw("Vertical"));
				transform.Translate(Vector3.right* Time.deltaTime * MovementSpeed * Input.GetAxisRaw("Horizontal"));
        	}
       	}

       	if(castDown.collider != null)
       	{
       		if( Input.GetAxisRaw("Vertical") < 0){
        		transform.Translate(Vector3.up* Time.deltaTime * MovementSpeed * Input.GetAxisRaw("Horizontal"));
        	}else{
        		transform.Translate(Vector3.up* Time.deltaTime * MovementSpeed * Input.GetAxisRaw("Vertical"));
				transform.Translate(Vector3.right* Time.deltaTime * MovementSpeed * Input.GetAxisRaw("Horizontal"));
        	}
       	}

       	if(castUp.collider != null)
       	{
       		if( Input.GetAxisRaw("Vertical") > 0){
        		transform.Translate(Vector3.up* Time.deltaTime * MovementSpeed * Input.GetAxisRaw("Horizontal"));
        	}else{
        		transform.Translate(Vector3.up* Time.deltaTime * MovementSpeed * Input.GetAxisRaw("Vertical"));
				transform.Translate(Vector3.right* Time.deltaTime * MovementSpeed * Input.GetAxisRaw("Horizontal"));
        	}
       	}
		

		if(Input.GetMouseButtonDown(0)){

			Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
			mousePos = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(mousePos);
			Vector3 moveDir = new Vector3( (mousePos - transform.position).x, (mousePos - transform.position).y, 0);
		}
	}
}
