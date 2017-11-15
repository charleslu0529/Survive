using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyScript : MonoBehaviour {
	public GameObject PointText;
	public float MovementSpeed = 0.5f;
	public float HealthPoints = 2f;
	public float DamageDealt = 1f;
	public float ViewRadius = 6f;
	public float BounceBackRecoveryTimer = 0.5f;
	public float DefaultMovementtimer = 0.5f;
	public float DefaultMovementRange = 2f;
	public float RightBoundary = 10f;
	public float TopBoundary = 7f;
	protected Vector3 destination;
	bool isAfterPlayer = true;
	bool hasHitPlayer = false;
	float localBBRTimer;
	float localMoveTimer;
	float randomNumber; 
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		destination = transform.position;
		localBBRTimer = BounceBackRecoveryTimer;

		rb = gameObject.GetComponent<Rigidbody2D>();
		localMoveTimer = DefaultMovementtimer;
	}
	
	// Update is called once per frame
	void Update () {

		// check if player is in view range
		if( (GameManager.instance.getPlayerPos() - transform.position).magnitude < ViewRadius )
		{
			isAfterPlayer = true;
		}
			else
		{
			isAfterPlayer = false;
		}

		//if player is in view range
		if( isAfterPlayer )
		{
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
				else
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

			randomNumber = Random.Range( ( (-1) * DefaultMovementRange), DefaultMovementRange );
		}
			else
		{
			localMoveTimer -= Time.deltaTime;

			if( localMoveTimer < 0f )
			{
				if( transform.position == destination )
				{

					destination = transform.position + new Vector3(randomNumber,randomNumber,0);

					if(destination.x > RightBoundary || destination.x < (-1 * RightBoundary))
					{
						destination = new Vector3(destination.x * (-1f), destination.y,0);
					}

					if(destination.y > TopBoundary || destination.y < (-1 * TopBoundary) )
					{
						destination = new Vector3(destination.x, destination.y * (-1f),0);
					}
				}

				rb.velocity = new Vector2(destination.x, destination.y).normalized * MovementSpeed;
			}
			/*float randomNumber = Random.Range(-5f, 5f);
			
			transform.Translate( destination * Time.deltaTime * MovementSpeed * 0.1f);*/
		}

		if(HealthPoints <= 0)
		{
			transform.localScale -= new Vector3(0.05f,0.05f, 0.05f);
		}
		if(transform.localScale.y < 0.05f)
		{
			GameManager.instance.incScore();
			GameManager.instance.playEnemyShrinkSound();
			Instantiate(PointText, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}

		
		
	}

	void FixedUpdate(){
		
	}

	void OnCollisionEnter2D(Collision2D col2D){
		if(col2D.gameObject.tag == "Bullet")
		{
			HealthPoints -= DamageDealt;
			GameManager.instance.playHitSound();
		}

		if(col2D.gameObject.tag == "Player")
		{
			hasHitPlayer = true;
		}

		// if(col2D.gameObject.tag == "Enemy")
		// {
		// 	rb.velocity = new Vector2(destination.x * -1f, destination.y * -1f).normalized * MovementSpeed;
		// }
	}
}
