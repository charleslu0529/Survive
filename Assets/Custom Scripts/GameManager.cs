// Sound effect used in this game are generated using bfxr.net
// bgm used in this game are taken from "Kodomo no Omocha"
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public GameObject Player;
	public GameObject SpawnPoint;
	public GameObject HPBar;
	public Camera MainCamera;
	public Text ScoreText;
	public Text InstructionText;
	public Text CenterText;
	public Text HighScoreText;
	public static GameManager instance = null;
	public bool isEnd;
	public float endGameTimer = 5f;
	public float HPLerpTime = 0.5f;
	/*public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;*/
	public float timer = 5f;
	public int TotalEnemyOnField = 200;
	protected int currentScore = 0;
	public static int HighScore;
	float shakeAmount;
	float tempTime;
	float screenShakeTime;
	float maxHPWidth;
	float playerMaxHealth;
	float hpBarMaxPosX;
	AudioSource playerShrinkSound;
	AudioSource enemyShrinkSound;
	AudioSource extinguishSound;
	AudioSource hitSound;



	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}else if(instance != this)
		{
			Destroy(gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		isEnd = false;
		tempTime = timer;
		maxHPWidth = HPBar.GetComponent<RectTransform>().sizeDelta.x;
		hpBarMaxPosX = HPBar.GetComponent<RectTransform>().anchoredPosition.x;
		AudioSource[] audios = GetComponents<AudioSource>();
		playerShrinkSound = audios[0];
		enemyShrinkSound = audios[1];
		extinguishSound = audios[2];
		hitSound = audios[3];

		HighScoreText.text = "High Score: " + HighScore.ToString();

		//Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

		//Cursor.visible = true;


	}
	
	// Update is called once per frame
	void Update () {

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		if(!isEnd){
			timer -= Time.deltaTime;
			if(timer < 0){
				int randomPattern = Random.Range(0, 5);

				if( enemies.Length < TotalEnemyOnField)
				{
					SpawnSpawnPoint(randomPattern);
				}
				
				InstructionText.text = "";
				timer = tempTime;
			}
		}

		ScoreText.text = currentScore.ToString();

		if(Input.GetKey(KeyCode.Escape)){
			SceneManager.LoadScene("Menu Scene");
		}

		if(screenShakeTime > 0f)
		{
			if(Player != null){
				MainCamera.transform.position = MainCamera.transform.position + Random.insideUnitSphere * shakeAmount;
			}
		}

		screenShakeTime -= Time.deltaTime;

		if(isEnd){
			if(currentScore > HighScore)
			{
				HighScore = currentScore;
			}
			CenterText.text = "ReStArTiNg In :  " + Mathf.Round(endGameTimer).ToString();
			endGameTimer -= Time.deltaTime;
			if(endGameTimer < 0){
				SceneManager.LoadScene( SceneManager.GetActiveScene().name );
			}
		}
	}

	void SpawnSpawnPoint(int pattern){
		// float randomNumber = Random.Range(-1f, 1f); 
		switch (pattern){
			case 1:

				Instantiate(SpawnPoint, new Vector3(13, 0, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(13, 5, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(12, -2, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(10, 0, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(1, -9, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-5, 5, 0.25f), Quaternion.identity);
				// Instantiate(SpawnPoint, new Vector3(-9, 8, 0), Quaternion.identity);
				// Instantiate(SpawnPoint, new Vector3(-3, -2, 0), Quaternion.identity);
				// Instantiate(SpawnPoint, new Vector3(-6, 9, 0), Quaternion.identity);
				// Instantiate(SpawnPoint, new Vector3(-13, 7, 0), Quaternion.identity);
				
				break;
			case 2:
				Instantiate(SpawnPoint, new Vector3(10, 5, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(11, 1, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(0, -2, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(1, -8, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(3, -9, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-5, 7, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(7, 8, 0.25f), Quaternion.identity);
				// Instantiate(SpawnPoint, new Vector3(-3, -2, 0), Quaternion.identity);
				// Instantiate(SpawnPoint, new Vector3(-2, 9, 0), Quaternion.identity);
				// Instantiate(SpawnPoint, new Vector3(-12, -7, 0), Quaternion.identity);
				break;
			case 3:
				Instantiate(SpawnPoint, new Vector3(0, 0, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(6, -1, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(12, -2, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(13, 8, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(1, -9, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-5, -2, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-13, 0, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-3, -2, 0.25f), Quaternion.identity);
				// Instantiate(SpawnPoint, new Vector3(-5, 0, 0), Quaternion.identity);
				// Instantiate(SpawnPoint, new Vector3(-11, 7, 0), Quaternion.identity);
				break;
			case 4:
				Instantiate(SpawnPoint, new Vector3(7, 0, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(0, 5, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(12, -2, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(4, 0, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(1, -9, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-5, 5, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-4, 8, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-3, -2, 0.25f), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-6, 9, 0.25f), Quaternion.identity);
				// Instantiate(SpawnPoint, new Vector3(-13, 7, 0), Quaternion.identity);
				break;
			/*case 5:
				Instantiate(SpawnPoint, new Vector3(7, 0, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(0, 5, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(12, -2, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(4, 0, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(1, -9, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-5, 5, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-4, 8, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-3, -2, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-6, 9, 0), Quaternion.identity);
				// Instantiate(SpawnPoint, new Vector3(-13, 7, 0), Quaternion.identity);
				break;
			case 6:
				Instantiate(SpawnPoint, new Vector3(7, 0, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(0, 5, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(12, -2, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(4, 0, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(1, -9, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-5, 5, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-4, 8, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-3, -2, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-6, 9, 0), Quaternion.identity);
				// Instantiate(SpawnPoint, new Vector3(-13, 7, 0), Quaternion.identity);
				break;
			case 7:
				Instantiate(SpawnPoint, new Vector3(7, 0, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(0, 5, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(12, -2, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(4, 0, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(1, -9, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-5, 5, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-4, 8, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-3, -2, 0), Quaternion.identity);
				Instantiate(SpawnPoint, new Vector3(-6, 9, 0), Quaternion.identity);
				// Instantiate(SpawnPoint, new Vector3(-13, 7, 0), Quaternion.identity);
				break;*/
			default:
				
				for(int i=0;i< 10;i++){
					Instantiate(SpawnPoint, new Vector3(Random.Range(-13f, 13f), Random.Range(-9f, 9f), 0), Quaternion.identity);
				}
				break;
		}
	}

	public void setEnd(){
		isEnd = true;
	}

	public bool getIsEnd(){
		return isEnd;
	}

	public Vector3 getPlayerPos(){
		if( Player != null)
		{
			return Player.transform.position;
		}
			else
		{
			return Vector3.zero;
		}
		
	}

	public void incScore(){
		if(!isEnd){
			currentScore += 1;
		}
	}
	public void playPlayerShrinkSound(){
		playerShrinkSound.Play();
	}
	public void playEnemyShrinkSound(){
		enemyShrinkSound.Play();
	}

	public void playExtinguishSound(){
		extinguishSound.Play();
	}

	public void playHitSound(){
		hitSound.Play();
	}

	public void ScreenShake(float shrinktime, float intensity){
		screenShakeTime = shrinktime;
		shakeAmount = intensity;
	}

	public void setMaxHP(float maxHP){
		playerMaxHealth = maxHP;
	}

	public void UpdateHP(float newHP){
		Vector2 newSizeDelta = new Vector2( maxHPWidth * (newHP/playerMaxHealth), HPBar.GetComponent<RectTransform>().sizeDelta.y );
		HPBar.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(HPBar.GetComponent<RectTransform>().sizeDelta, newSizeDelta, HPLerpTime);
		Vector2 newAnchoredPosition = new Vector2( hpBarMaxPosX * (newHP/playerMaxHealth), HPBar.GetComponent<RectTransform>().localPosition.y);
		HPBar.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(HPBar.GetComponent<RectTransform>().anchoredPosition, newAnchoredPosition, HPLerpTime);
	}
}
