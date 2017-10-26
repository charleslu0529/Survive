using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	public GameObject PointingArrow;
	public Camera MainCamera;
	public GameObject Bullet;
	public float BulletSpeed = 2f;
	public float HealthPoint = 10f;
	public GameObject HPBar;
	public float KnockBack = 15f;
	protected Vector3 mousePos;
	protected Vector3 screenPos;
	protected Vector3 positionDifference;
	protected float angleToRotate;
	protected float maxHPWidth;
	float maxHealth;
	Vector3 newPlayerYPos;
	AudioSource PickupSound;
	AudioSource HitSound;
	Vector3 playerPositionTemp;


	// Use this for initialization
	void Start () {
		positionDifference = PointingArrow.transform.position - transform.position;
		maxHPWidth = HPBar.GetComponent<RectTransform>().sizeDelta.x;
		maxHealth = HealthPoint;
		AudioSource[] audios = GetComponents<AudioSource>();
		PickupSound = audios[0];
		HitSound = audios[1];
	}
	
	// Update is called once per frame
	void Update () {

		mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		screenPos = MainCamera.GetComponent<Camera>().WorldToScreenPoint(transform.position);
		angleToRotate = Mathf.Atan2((mousePos - screenPos).y,(mousePos - screenPos).x) * Mathf.Rad2Deg -90f;


		// at end game
		if(!GameManager.instance.getIsEnd()){
			transform.rotation = Quaternion.AngleAxis(angleToRotate, Vector3.forward);

			if(transform.localScale.x > 0.15f){

				if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")){
					GameObject newBullet = Instantiate(Bullet, transform.position + (transform.up/1.4f), Quaternion.identity) as GameObject;
					newBullet.GetComponent<Rigidbody2D>().velocity = transform.up * BulletSpeed;
					transform.localScale = new Vector3( (transform.localScale.x - 0.05f), (transform.localScale.y - 0.05f), 1f);
					transform.parent.GetComponent<PlayerMovementScript>().MovementSpeed += 0.5f;
				}
			}
			if(HealthPoint <= 0){
				transform.localScale -= new Vector3(0.05f,0.05f, 0.05f);
			}
			if(transform.localScale.x < 0.09f){

				GameManager.instance.setEnd();
				GameManager.instance.playPlayerShrinkSound();
				Destroy(gameObject);
			}
		}
			
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    	GetComponent<Rigidbody2D>().angularVelocity = 0f; 
			
	}

	void OnCollisionEnter2D(Collision2D col2D){
		if(col2D.gameObject.tag == "Bullet"){
			Destroy(col2D.gameObject);
			transform.localScale = new Vector3( (transform.localScale.x + 0.05f), (transform.localScale.y + 0.05f), 1f);
			transform.parent.GetComponent<PlayerMovementScript>().MovementSpeed -= 0.5f;
			PickupSound.Play();
		}

		if(col2D.gameObject.tag == "Enemy"){
			HealthPoint -= 1f;
			HitSound.Play();
			GetComponent<Rigidbody2D>().AddRelativeForce((transform.position - col2D.gameObject.transform.position) * (-1f) * KnockBack);
			HPBar.GetComponent<RectTransform>().sizeDelta = new Vector2(maxHPWidth * (HealthPoint/maxHealth),HPBar.GetComponent<RectTransform>().sizeDelta.y);
			HPBar.GetComponent<RectTransform>().localPosition = new Vector3(-(maxHPWidth-HPBar.GetComponent<RectTransform>().sizeDelta.x)/2, 0,0);
		}
	}
}
