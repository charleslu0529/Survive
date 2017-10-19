using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Button StartButton;
	public Button ExitButton;
	/*public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;*/
	// Use this for initialization
	void Start () {
		StartButton.GetComponent<Button>().onClick.AddListener(TaskOnClickStart);
		ExitButton.GetComponent<Button>().onClick.AddListener(TaskOnClickExit);
		//Cursor.visible = true;
		//Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("space")){
			SceneManager.LoadScene("Game Scene");
		}

		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}

	void TaskOnClickStart()
    {
        SceneManager.LoadScene("Game Scene");
    }

    void TaskOnClickExit()
    {
        Application.Quit();
    }
}
