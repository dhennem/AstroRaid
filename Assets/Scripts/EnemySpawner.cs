using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	static int enemiesLeft;

	public GameObject enemy1; //defined as the enemy1 prefab in the inspector
	public GameObject enemy2; //defined as the enemy2 prefab in the inspector
	public GameObject enemy3; //defined as the enemy3 prefab in the inspector

	private int numSpawnPositions;

	public int numEnemy1ToSpawn; //defined in the inspector
	public int numEnemy2ToSpawn; //defined in the inspector
	public int numEnemy3ToSpawn; //defined in the inspector

	// Use this for initialization
	void Start () {
		numSpawnPositions = transform.childCount;
		enemiesLeft = numEnemy1ToSpawn + numEnemy2ToSpawn + numEnemy3ToSpawn;
		SpawnEnemies();
		
	}


	//the number of enemy1's, enemy2's, and enemy3's are assigned randomly to their own unique spawn points
	//IMPORTANT: for each scene with an enemy spawner, make sure that the number of enemy1's enemy2's and enemy3's sum to an int less than the number of spawn points
	void SpawnEnemies(){
		while(numEnemy1ToSpawn > 0){
			int childIndex = Random.Range(0,numSpawnPositions); //randomly selects a spawn position
			Transform spawnPosition = transform.GetChild(childIndex);
			if(spawnPosition.childCount == 0){ //if this position doesn't already have a spawn, create one
				GameObject artifactSpawn = Instantiate(enemy1, spawnPosition.transform.position, Quaternion.identity) as GameObject;
				artifactSpawn.transform.parent = spawnPosition.transform; //creates an artifact spawn at the location of this spawn point
				numEnemy1ToSpawn -=1;
			}
		}
		while(numEnemy2ToSpawn > 0){
			int childIndex = Random.Range(0,numSpawnPositions); //randomly selects a spawn position
			Transform spawnPosition = transform.GetChild(childIndex);
			if(spawnPosition.childCount == 0){ //if this position doesn't already have a spawn, create one
				GameObject artifactSpawn = Instantiate(enemy2, spawnPosition.transform.position, Quaternion.identity) as GameObject;
				artifactSpawn.transform.parent = spawnPosition.transform; //creates an artifact spawn at the location of this spawn point
				numEnemy2ToSpawn -=1;
			}
		}
		while(numEnemy3ToSpawn > 0){
			int childIndex = Random.Range(0,numSpawnPositions); //randomly selects a spawn position
			Transform spawnPosition = transform.GetChild(childIndex);
			if(spawnPosition.childCount == 0){ //if this position doesn't already have a spawn, create one
				GameObject artifactSpawn = Instantiate(enemy3, spawnPosition.transform.position, Quaternion.identity) as GameObject;
				artifactSpawn.transform.parent = spawnPosition.transform; //creates an artifact spawn at the location of this spawn point
				numEnemy3ToSpawn -=1;
			}
		}
	}
}
