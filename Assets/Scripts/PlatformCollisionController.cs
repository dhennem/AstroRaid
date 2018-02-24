using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollisionController : MonoBehaviour {

	private BoxCollider2D playerCollider;


	[SerializeField]
	private BoxCollider2D platformCollider;
	[SerializeField]
	private BoxCollider2D platformTrigger;

	// Use this for initialization
	void Start () {
		playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
		Physics2D.IgnoreCollision(platformCollider,platformTrigger,true); //ignore the collision between each platform's trigger and collider
		
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.name == "Player"){
			Physics2D.IgnoreCollision(platformCollider, playerCollider, true);  //allow player to pass through platform
		}
		if(collider.gameObject.layer == 9){
			BoxCollider2D enemyCollider = collider.GetComponent<BoxCollider2D>();   //gets the colliders for enemies on an individual basis because there are many enemies
			Physics2D.IgnoreCollision(platformCollider, enemyCollider, true);
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.gameObject.name == "Player"){
			Physics2D.IgnoreCollision(platformCollider, playerCollider, false);  //now allow Player to stand on platform
		}
		if(collider.gameObject.layer == 9){
			BoxCollider2D enemyCollider = collider.GetComponent<BoxCollider2D>();   //gets the colliders for enemies on an individual basis because there are many enemies
			Physics2D.IgnoreCollision(platformCollider, enemyCollider, false);
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
