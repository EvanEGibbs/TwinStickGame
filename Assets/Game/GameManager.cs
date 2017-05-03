using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

	public bool recording;
	private float fixedDeltaTimeValue;

	// Use this for initialization
	void Start () {
		fixedDeltaTimeValue = Time.fixedDeltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButton("Fire1")) {
			recording = false;
		}
		else {
			recording = true;
		}

		if (Input.GetKeyDown(KeyCode.P)) {
			if (Time.timeScale == 0) {
				ResumeGame();
			} else {
				PauseGame();
			}
		}
	}

	void PauseGame() {
		Time.timeScale = 0f;
		Time.fixedDeltaTime = 0f;
	}
	void ResumeGame() {
		Time.timeScale = 1f;
		Time.fixedDeltaTime = fixedDeltaTimeValue;
	}
}
