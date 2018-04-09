using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour {


	public ButtonControllers buttonControllers;

	[SerializeField]
	private BoxCollider2D playerCollider;
	[SerializeField]
	private BoxCollider2D mysteryBoxCollider; //defined in the inspector to equal the mystery box's collider, not its trigger
	[SerializeField]
	private AudioClip collectedSound;

	// Use this for initialization
	void Start () {
		playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();  //don't allow player and box to collide so that movement is smooth. Player and box will only interact with a trigger
		Physics2D.IgnoreCollision(playerCollider, mysteryBoxCollider, true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	void OnTriggerEnter2D(Collider2D collider){
		if(collider.name == "Player"){
			AudioSource.PlayClipAtPoint(collectedSound, transform.position);
			Destroy(gameObject);
			print(PlayerController.superpower);
			if(PlayerController.superpower==0){ //if the player already has a superpower, do nothing
				switch(Random.Range(0,3)){
					case 0:
						//shield
						PlayerController.superpower = 1; 
						break;
					case 1:
						//jetpack
						PlayerController.superpower = 2;
						break;
					case 2:
						//superjump
						PlayerController.superpower = 3;
						break;
				}
				buttonControllers.ActivateSuperpowerButton();
			}
			print(PlayerController.superpower);
		}
	}
}
