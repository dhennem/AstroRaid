using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactSpawner : MonoBehaviour {

	public GameObject artifact; //defined as the artifact prefab in the inspector

	private int numSpawnPositions;

	public int numArtifactsToSpawn; //defined in the inspector

	//accessed in the header
	static public int totalNumArtifacts;


	void Awake(){
		totalNumArtifacts = numArtifactsToSpawn;
	}

	// Use this for initialization
	void Start () {
		numSpawnPositions = transform.childCount;
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
