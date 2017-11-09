using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class playerScript : MonoBehaviour {

	public GameObject PointingArrow;
	public Camera MainCamera;
	public GameObject Bullet;
	public GameObject HPBar;
	public LayerMask CastLayer;
	public float KnockBack = 15f;
	public float BulletSpeed = 2f;
	public float HealthPoint = 10f;
	public float CastLength = 0.5f;
	public float MovementSpeed = 5f;
	public float MovementLerpTime = 1f;
	public float ScreenShakeTime = 0.2f;
	public float ScreenShakeIntensity = 0.2f;
	Vector3 mousePos;
	Vector3 screenPos;
	Vector3 positionDifference;
	float angleToRotate;
	float maxHPWidth;
	float maxHealth;
	Vector3 newPlayerYPos;
	AudioSource PickupSound;
	AudioSource HitSound;
	Vector3 playerPositionTemp;
	RaycastHit2D castLeft;
	RaycastHit2D castRight;
	RaycastHit2D castDown;
	RaycastHit2D castUp;
	Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		positionDifference = PointingArrow.transform.position - transform.position;
		maxHPWidth = HPBar.GetComponent<RectTransform>().sizeDelta.x;
		maxHealth = HealthPoint;
		AudioSource[] audios = GetComponents<AudioSource>();
		PickupSound = audios[0];
		HitSound = audios[1];
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = Vector3.zero;
	}

	// Update is called once per frame
	void Update () {

		// Ray cast to stop player from jittering
		castLeft = Physics2D.Raycast(transform.position, Vector3.left, CastLength, CastLayer);
		castRight = Physics2D.Raycast(transform.position, Vector3.right, CastLength, CastLayer);
		castDown = Physics2D.Raycast(transform.position, Vector3.down, CastLength, CastLayer);
		castUp = Physics2D.Raycast(transform.position, Vector3.up, CastLength, CastLayer);

		mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		screenPos = MainCamera.GetComponent<Camera>().WorldToScreenPoint(transform.position);
		angleToRotate = Mathf.Atan2((mousePos - screenPos).y, (mousePos - screenPos).x) * Mathf.Rad2Deg - 90f;

		rb.velocity = Vector2.Lerp(rb.velocity, new Vector2( Input.GetAxisRaw("Horizontal") , Input.GetAxisRaw("Vertical") ).normalized * MovementSpeed , MovementLerpTime );
/******************************
Stop player when next to wall
******************************/
		if (castLeft.collider != null)
		{
			if ( Input.GetAxisRaw("Horizontal") < 0) {
				rb.velocity = new Vector2(0f, rb.velocity.y);
			} else {
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
			}
		}

		if (castRight.collider != null)
		{
			if ( Input.GetAxisRaw("Horizontal") > 0) {
				rb.velocity = new Vector2(0f, rb.velocity.y);
			} else {
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
			}
		}

		if (castDown.collider != null)
		{
			if ( Input.GetAxisRaw("Vertical") < 0) {
				rb.velocity = new Vector2(rb.velocity.x, 0f);
			} else {
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
			}
		}

		if (castUp.collider != null)
		{
			if ( Input.GetAxisRaw("Vertical") > 0) {
				rb.velocity = new Vector2(rb.velocity.x, 0f);
			} else {
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
			}
		}
/******************************
******************************/


/******************************
If not the end of a game
******************************/
		// if not end game
		if (!GameManager.instance.getIsEnd()) {
			transform.rotation = Quaternion.AngleAxis(angleToRotate, Vector3.forward);

		/******************************
		shoot on mouse left click
		******************************/
			if (transform.localScale.x > 0.15f) {

				if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")) {
					GameObject newBullet = Instantiate(Bullet, transform.position + (transform.up / 1.4f), Quaternion.identity) as GameObject;
					newBullet.GetComponent<Rigidbody2D>().velocity = transform.up * BulletSpeed;
					transform.localScale = new Vector3( (transform.localScale.x - 0.05f), (transform.localScale.y - 0.05f), 1f);
					MovementSpeed += 0.5f;
					GameManager.instance.ScreenShake(ScreenShakeTime, ScreenShakeIntensity);
				}
			}
		/******************************
		******************************/
			if (HealthPoint <= 0) {
				transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
			}
			if (transform.localScale.x < 0.09f) {

				GameManager.instance.setEnd();
				GameManager.instance.playPlayerShrinkSound();
				Destroy(gameObject);
			}
		}
/******************************
******************************/
		GetComponent<Rigidbody2D>().angularVelocity = 0f;

	}

	void OnCollisionEnter2D(Collision2D col2D) {
		if (col2D.gameObject.tag == "Bullet") {
			Destroy(col2D.gameObject);
			transform.localScale = new Vector3( (transform.localScale.x + 0.05f), (transform.localScale.y + 0.05f), 1f);
			MovementSpeed -= 0.5f;
			PickupSound.Play();
		}

		if (col2D.gameObject.tag == "Enemy") {
			HealthPoint -= 1f;
			HitSound.Play();
			GetComponent<Rigidbody2D>().AddRelativeForce((transform.position - col2D.gameObject.transform.position) * (-1f) * KnockBack);
			HPBar.GetComponent<RectTransform>().sizeDelta = new Vector2(maxHPWidth * (HealthPoint / maxHealth), HPBar.GetComponent<RectTransform>().sizeDelta.y);
			HPBar.GetComponent<RectTransform>().localPosition = new Vector3(-(maxHPWidth - HPBar.GetComponent<RectTransform>().sizeDelta.x) / 2, 0, 0);
		}
	}
}
