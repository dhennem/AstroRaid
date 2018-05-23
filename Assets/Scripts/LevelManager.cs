using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public float waitTimeBeforeNextLevel;
	public bool isSplash;
	public PlayerController player;
	public static int highestLevelReached;

	// Use this for initialization
	void Start () {
		if(isSplash){
			LoadNextLevel();
		}
		player = FindObjectOfType<PlayerController>();
		
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
		RefreshValuesBeforeNewLevel();

	}
	public void LoadLoseWithPause(){
		Invoke("Lose",3f);
	}
	public void LoadHighestLevelReached(){
		Application.LoadLevel(highestLevelReached);
	}
	private void Lose(){
		if(Application.loadedLevel > highestLevelReached) highestLevelReached = Application.loadedLevel;
		LoadLevel("Lose");
		player.DeactivateSuperpower();
	}

	private void RefreshValuesBeforeNewLevel(){
		//return static variables, etc to original values before loading a new level
		player.shieldBar.resourceValue = player.shieldBar.GetMinValue();
		player.healthBar.resourceValue = player.originalMaxHealth;
		player.healthBar.ChangeMaxValue(player.originalMaxHealth);
		player.DeactivateSuperpower();
	}
}
