using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public float waitTimeBeforeNextLevel;
	public bool isSplash;

	// Use this for initialization
	void Start () {
		if(isSplash){
			LoadNextLevel();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadLevel(string levelName){
		Application.LoadLevel(levelName);
	}
	public void QuitRequestl(){
		Debug.Log("Quit requested");
	}
	public void LoadNextLevel(){
		Invoke("LoadNextLevelWithWait", waitTimeBeforeNextLevel);
	}
	private void LoadNextLevelWithWait(){
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	public void LoadLoseWithPause(){
		Invoke("Lose",3f);
	}
	private void Lose(){
		LoadLevel("Lose");
	}

	private void RefreshValuesBeforeNewLevel(){
		//return static variables, etc to original values before loading a new level
	}
}
