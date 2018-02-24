using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public bool verticalMovement;
	public bool horizontalMovement;
	private Vector3 startPos;
	private bool movingUp;
	private bool movingRight;
	public float verticalRange;
	public float horizontalRange;

	private PlayerController player;
	private bool playerAttached;


	// Use this for initialization
	void Start () {
		startPos = transform.position;
		if(verticalMovement){
			movingUp = false;
		}
		if(horizontalMovement){
			movingRight = true;
		}

		player = FindObjectOfType<PlayerController>();
	
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleMovement();
		if(playerAttached) KeepPlayerAttached();
		
	}

	void HandleMovement(){
		if(verticalMovement){
			if(movingUp){
				Vector3 newPos = new Vector3(transform.position.x, transform.position.y + 0.05f, 0f);
				transform.position = newPos;
				if(playerAttached){
					player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.05f, 0f);
				}
				if(transform.position.y > startPos.y){
					movingUp = false;
				}
			}
			else{
				Vector3 newPos = new Vector3(transform.position.x, transform.position.y - 0.05f, 0f);
				transform.position = newPos;
				if(playerAttached){
					player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y-0.05f, 0f);
				}
				if(transform.position.y < startPos.y - verticalRange){
					movingUp = true;
				}
			}
		}
		if(horizontalMovement){
			if(movingRight){
				Vector3 newPos = new Vector3(transform.position.x + 0.05f, transform.position.y, 0f);
				transform.position = newPos;
				if(playerAttached){
					player.transform.position = new Vector3(player.transform.position.x + 0.05f, player.transform.position.y, 0f);
				}
				if(transform.position.x > startPos.x + horizontalRange){
					movingRight = false;
				}
			}
			else{
				Vector3 newPos = new Vector3(transform.position.x - 0.05f, transform.position.y, 0f);
				transform.position = newPos;
				if(playerAttached){
					player.transform.position = new Vector3(player.transform.position.x -0.05f, player.transform.position.y, 0f);
				}
				if(transform.position.x < startPos.x){
					movingRight = true;
				}
			}
		}
	}

	void KeepPlayerAttached(){ //if the player is on a moving platform, move the player with the platform
		 
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.collider.tag == "Player"){
			playerAttached = true;
		}
	}
	void OnCollisionExit2D(Collision2D collision){
		if(collision.collider.tag == "Player"){
			playerAttached = false;
		}
	}
}
