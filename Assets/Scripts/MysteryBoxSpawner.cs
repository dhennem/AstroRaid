using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBoxSpawner : MonoBehaviour {


	public GameObject mysteryBox;

	private int numBoxesToSpawn; //defined relative to the game difficulty

	private int numSpawnPositions;
	private int gameDifficulty;

	// Use this for initialization
	void Start () {
		numSpawnPositions = transform.childCount;
		gameDifficulty = (int) PlayerPrefsManager.GetDifficulty();
		numBoxesToSpawn = numSpawnPositions - gameDifficulty + 1;
		SpawnMysteryBoxes();
		
	}

	void SpawnMysteryBoxes(){
		while(numBoxesToSpawn > 0){
			int childIndex = Random.Range(0,numSpawnPositions); //randomly selects a spawn position
			Transform spawnPosition = transform.GetChild(childIndex);
			if(spawnPosition.childCount == 0){ //if this position doesn't already have a spawn, create one
				GameObject artifactSpawn = Instantiate(mysteryBox, spawnPosition.transform.position, Quaternion.identity) as GameObject;
				artifactSpawn.transform.parent = spawnPosition.transform; //creates an artifact spawn at the location of this spawn point
				numBoxesToSpawn -=1;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
