using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactSpawner : MonoBehaviour {

	//numArtifactsToSpawn should be affected by difficulty level

	public GameObject artifact; //defined as the artifact prefab in the inspector

	private int numSpawnPositions;

	private int numArtifactsToSpawn; //defined relative to game difficulty

	//accessed in the header
	static public int totalNumArtifacts;

	private int gameDificulty;


	void Awake(){
		gameDificulty = (int) PlayerPrefsManager.GetDifficulty();
		numSpawnPositions = transform.childCount;
		numArtifactsToSpawn = numSpawnPositions - (8-gameDificulty);
		totalNumArtifacts = numArtifactsToSpawn;
	}

	// Use this for initialization
	void Start () {
		SpawnArtifacts();
		
	}

	void SpawnArtifacts(){
		while(numArtifactsToSpawn > 0){
			int childIndex = Random.Range(0,numSpawnPositions); //randomly selects a spawn position
			Transform spawnPosition = transform.GetChild(childIndex);
			if(spawnPosition.childCount == 0){ //if this position doesn't already have a spawn, create one
				GameObject artifactSpawn = Instantiate(artifact, spawnPosition.transform.position, Quaternion.identity) as GameObject;
				artifactSpawn.transform.parent = spawnPosition.transform; //creates an artifact spawn at the location of this spawn point
				numArtifactsToSpawn -=1;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
