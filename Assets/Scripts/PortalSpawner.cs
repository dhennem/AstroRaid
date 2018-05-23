using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour {

	public GameObject portal;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnWinPortal(){
		Vector3 spawnPos = transform.GetChild(0).transform.position;
		GameObject portalSpawn = Instantiate(portal, spawnPos, Quaternion.identity) as GameObject;
		print("portal should be spawned");
		print(spawnPos); 

	}
}
