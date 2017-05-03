using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";
	const string DIFF_KEY = "difficulty";
	const string EDITED_OPTIONS_KEY = "edited_options";

	public static void SetEditedOptions(int value){
		if (value == 1) {
			PlayerPrefs.SetInt (EDITED_OPTIONS_KEY, value);
		} else {
			Debug.LogError ("Incorrect input into edited options: 1 for has edited options");
		}
	}

	public static int GetEditedOptions(){
		return PlayerPrefs.GetInt (EDITED_OPTIONS_KEY);
	}

	public static void SetMasterVolume (float volume){
		if (volume >= 0f && volume <= 1f) {
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError ("Master Volume out of range: should be betweeon 0 and 1");
		}
	}
	public static float GetMasterVolume(){
		return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY);
	}

	public static void SetDifficulty(int difficulty){
		if (difficulty >= 1 && difficulty <= 3) {
			PlayerPrefs.SetInt (DIFF_KEY, difficulty);
		} else {
			Debug.LogError ("Difficulty out of range: should be between 1 and 3");
		}
	}

	public static float GetDifficulty(){
		return PlayerPrefs.GetInt (DIFF_KEY);
	}

	public static void UnlockLevel (int level){
		if (level <= SceneManager.sceneCountInBuildSettings - 1) {
			PlayerPrefs.SetInt (LEVEL_KEY + level.ToString (), 1); //Use 1 for true
		} else {
			Debug.LogError ("Trying to unlock level not in build order");
		}
	}
	public static bool levelUnlocked (int level){
		if (level <= SceneManager.sceneCountInBuildSettings - 1) {
			int levelValue = PlayerPrefs.GetInt (LEVEL_KEY + level.ToString ());
			if (levelValue == 1) {
				return true;
			} else {
				return false;
			}
		} else {
			Debug.LogError ("Checking level unlocked not in build order");
			return false;
		}
	}
}
