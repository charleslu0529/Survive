using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerScript : MonoBehaviour {

	public float MovementSpeed = 1f;
	bool hasHit = false;
	Vector3 destination;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(hasHit){
			if( (GameManager.instance.getPlayerPos() - transform.position).magnitude < 4){
				GetComponent<Rigidbody2D>().velocity = new Vector2( (GameManager.instance.getPlayerPos() - transform.position).x , (GameManager.instance.getPlayerPos() - transform.position).y ).normalized * 2;
			}
		}	
	}

	void OnCollisionEnter2D(Collision2D col2D){

		if(col2D.gameObject.tag != "Bullet"){
			hasHit = true;
		}
	}
}
