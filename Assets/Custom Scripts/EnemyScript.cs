using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyScript : MonoBehaviour {
	public float MovementSpeed = 0.5f;
	public float HealthPoints = 2f;
	public float DamageDealt = 1f;
	public float ViewRadius = 6f;
	public float BounceBackRecoveryTimer = 0.5f;
	protected Vector3 destination;
	bool isAfterPlayer = true;
	bool hasHitPlayer = false;
	float localBBRTimer;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		destination = transform.position;
		localBBRTimer = BounceBackRecoveryTimer;

		rb = gameObject.GetComponent<Rigidbody2D>()
	}
	
	// Update is called once per frame
	void Update () {

		if( isAfterPlayer )
		{
				if( (GameManager.instance.getPlayerPos() - transform.position).magnitude < ViewRadius)
				{
				rb.velocity = (GameManager.instance.getPlayerPos() - transform.position).normalized * 2;
				
				if((GameManager.instance.getPlayerPos() - transform.position).magnitude < (ViewRadius/2))
				{
					rb.velocity = (GameManager.instance.getPlayerPos() - transform.position).normalized * 5;

					//transform.Translate( (GameManager.instance.getPlayerPos() - transform.position) * Time.deltaTime * MovementSpeed * 3);
					/*if((GameManager.instance.getPlayerPos() - transform.position).magnitude < 2)
					{
						rb.velocity = (GameManager.instance.getPlayerPos() - transform.position).normalized * 5;
					}*/
				}
			}
		}
			else
		{
			float randomNumber = Random.Range(-5f, 5f);
			if(transform.position == destination){
				destination = transform.position + new Vector3(randomNumber,randomNumber,0);
				if(destination.x > 13f || destination.x < -13f){
					destination = new Vector3(destination.x * (-1f), destination.y,0);
				}
				if(destination.y > 9f || destination.y < -9f){
					destination = new Vector3(destination.x, destination.y * (-1f),0);
				}
			}
			transform.Translate( destination * Time.deltaTime * MovementSpeed * 0.1f);
		}

		if(HealthPoints <= 0)
		{
			transform.localScale -= new Vector3(0.05f,0.05f, 0.05f);
		}
		if(transform.localScale.y < 0.05f)
		{
			GameManager.instance.incScore();
			GameManager.instance.playEnemyShrinkSound();
			Destroy(gameObject);
		}

		if( hasHitPlayer )
		{
			localBBRTimer -= Time.deltaTime;
			if( localBBRTimer < 0f )
			{
				hasHitPlayer = false;
				localBBRTimer = BounceBackRecoveryTimer;
			}
			rb.velocity = ( transform.position - GameManager.instance.getPlayerPos() ).normalized;
		}
		
	}

	void FixedUpdate(){
		
	}

	void OnCollisionEnter2D(Collision2D col2D){
		if(col2D.gameObject.tag == "Bullet"){
			HealthPoints -= DamageDealt;
			hasHitPlayer = true;
		}
	}
}
