using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour {

	static public int artifactsCollected;

	public PortalSpawner portalSpawner;

	[SerializeField]
	private BoxCollider2D playerCollider;
	[SerializeField]
	private BoxCollider2D artifactCollider; //defined in the inspector
	[SerializeField]
	private AudioClip collectedSound;


	// Use this for initialization
	void Start () {
		artifactsCollected = 0;//initially at 0
		playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
		Physics2D.IgnoreCollision(playerCollider, artifactCollider, true);  //prevent player and artifact from colliding to ensure smooth movement, but trigger still works
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.name == "Player"){
			HandlePickups();
		}
	}

	//handles what happens when the player picks up an artifact
	void HandlePickups(){
		AudioSource.PlayClipAtPoint(collectedSound, transform.position);
		artifactsCollected +=1;
		Destroy(gameObject);
		CheckPortalSpawn();
	}


	//spawning a portal to enter the next level once all artifacts are collected
	void CheckPortalSpawn(){
		if(artifactsCollected == ArtifactSpawner.totalNumArtifacts){
			portalSpawner.SpawnWinPortal();
		}
	}
}
