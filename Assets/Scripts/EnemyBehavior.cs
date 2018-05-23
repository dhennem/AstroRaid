using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	//maxHealth and chanceOfShooting should be dependent on difficulty level

	public GameObject shot;
	public float movementRange; //number of world units left and right the enemy is allowed to move
	public GameObject lifeHeart;

	private PlayerController player;
	private int maxHealth;
	private int currentHealth;
	private bool shooting;

	private bool rightOfPlayer;
	private bool movingRight;
	private bool isMoving;
	private bool waitingAtEdge;
	private bool waitingAtWall;
	private int gameDifficulty;

	[SerializeField]
	private AudioClip deathSound;
	[SerializeField]
	private float groundRadius;
	[SerializeField]
	private LayerMask whatIsGround; //determines what layers are considered ground for the enemy
	[SerializeField]
	private Transform[] groundPoints;
	[SerializeField]
	private ResourceBar enemyHealthBar;
	[SerializeField]
	private Canvas enemyHealthCanvas;
	[SerializeField]
	private AudioClip enemyShotSound;
	[SerializeField]
	private float wallRadius;
	[SerializeField]
	private LayerMask whatIsWall; //determines what layers are considered a wall for the enemy
	[SerializeField]
	private Transform[] leftWallPoints;
	[SerializeField]
	private Transform[] rightWallPoints;


	// Use this for initialization
	void Start () {
		gameDifficulty = (int) PlayerPrefsManager.GetDifficulty();
		maxHealth = gameDifficulty + 2; //enemy health indicated by value of difficulty
		shooting = false;
		player = FindObjectOfType<PlayerController>();
		movingRight = false;
		currentHealth = maxHealth;
		enemyHealthCanvas.enabled = false; //do not show health bar at first
		enemyHealthBar.ChangeMaxValue(maxHealth);
		enemyHealthBar.resourceValue = currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(player!=null){
			if(!shooting && !isMoving && isInRangeOfPlayer()){ //only start enemy shooting and movement when they are close to player
				shooting = true;
				isMoving = true;
				//InvokeRepeating("Shoot", 0.5f, 2f);
			}
			DeterminePositionRelativeToPlayer();
			HandleDirection();
			if(isMoving && !isTooCloseToPlayer()){
				HandleMovement();
			}
			if(shooting){
				if(Random.Range(0,100) < (Mathf.Clamp(0.5f*gameDifficulty, 0.5f, 1.5f))){  //.5 * difficulty% chance of shooting each frame
					Shoot();
				}
			}
			if(isInRangeOfPlayer()){
				if(DetermineIfHittingLeftWall()) print("hitting left wall");
				if(DetermineIfHittingRightWall()) print("hitting right wall");

			}
		}
		
	}

	void DeterminePositionRelativeToPlayer(){
		if(transform.position.x > player.transform.position.x){
			rightOfPlayer = true;
		} else{
			rightOfPlayer = false;
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.collider.tag == "PlayerShot"){
			if(!isMoving){
				isMoving = true;
				shooting = true;
			}
			if(!enemyHealthCanvas.isActiveAndEnabled){
				enemyHealthCanvas.enabled = true; //only show health bar once enemy starts taking damage
			}
			if(currentHealth>1){
				HandleNonFatalHits();
			}
			else{
				HandleFatalHits();
			}
			currentHealth -= 1;
			enemyHealthBar.resourceValue = currentHealth;
		}
	}

	void HandleMovement(){
		bool isGrounded = DetermineIfGroundedOnLeft() && DetermineIfGroundedOnRight();
		if(isInRangeOfPlayer()){
			//print(isGrounded);
		}
		if(!isGrounded){
			waitingAtEdge = true;
		}
		if(DetermineIfHittingLeftWall() || DetermineIfHittingRightWall()){
			waitingAtWall = true;
		}
		if (waitingAtEdge){  //not grounded
			if(DetermineIfGroundedOnLeft() && !DetermineIfGroundedOnRight()){ //hanging off a right edge
				//print("Not grounded on right");
				if(!rightOfPlayer){
					waitingAtEdge = true;
				}
				else{
					waitingAtEdge = false;
				}
			}
			else if(DetermineIfGroundedOnRight() && !DetermineIfGroundedOnLeft()){ //hanging off a left edge
				//print("Not grounded on left");
				if(rightOfPlayer){
					waitingAtEdge = true;
				}
				else{
					waitingAtEdge = false;
				}

			}
			else{
				waitingAtEdge = false;
			}
		}
		if(waitingAtWall){
			if(DetermineIfHittingLeftWall() && !DetermineIfHittingRightWall()){ //hitting a left wall
				if(!rightOfPlayer){
					waitingAtWall = false;
				}
				else{
					waitingAtWall = true;
				}
			}
			else if(!DetermineIfHittingLeftWall() && DetermineIfHittingRightWall()){ //hitting a right wall
				if(!rightOfPlayer){
					waitingAtWall = true;
				}
				else{
					waitingAtWall = false;
				}
			}
			else{
				waitingAtWall = false;
			}
		}
		if(!waitingAtEdge && !waitingAtWall){
			if(movingRight){ //move right
				Vector3 currentPosition = transform.position;
				Vector3 newPosition = new Vector3(currentPosition.x+0.05f, currentPosition.y, 0f);
				transform.position = newPosition;
			}
			else{ //move left
				Vector3 currentPosition = transform.position;
				Vector3 newPosition = new Vector3(currentPosition.x-0.05f, currentPosition.y, 0f);
				transform.position = newPosition;
			}
		}
	}

	bool DetermineIfGroundedOnRight(){ //determines if the enemy's right side is hanging off an edge
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoints[1].position, groundRadius, whatIsGround);
		for(int i = 0; i < colliders.Length; i++){
			if(colliders[i].gameObject.layer != 9){ //if the right ground point is colliding with something other than the player, then the player is grounded on the right
				return true;
			}
		}
		return false;
	}

	bool DetermineIfGroundedOnLeft(){  //determines if the enemy's left side is hanging off an edge
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoints[0].position, groundRadius, whatIsGround);
		for(int i = 0; i < colliders.Length; i++){
			if(colliders[i].gameObject.layer != 9){ //if the right ground point is colliding with something other than the player, then the player is grounded on the right
				return true;
			}
		}
		return false;
	}

	bool DetermineIfHittingLeftWall(){ //determines if an enemy's left side is touching a wall
		foreach(Transform leftWallPoint in leftWallPoints){
				Collider2D[] colliders = Physics2D.OverlapCircleAll(leftWallPoint.position, wallRadius, whatIsWall);
				for(int i = 0; i < colliders.Length; i++){
					if(colliders[i].gameObject != gameObject){ //if one of the ground points is colliding with something other than the player, then the player is grounded
						return true;
					}
				}
		}
		return false;
	}

	bool DetermineIfHittingRightWall(){ //determines if an enemy's right side is touching a wall
		foreach(Transform rightWallPoint in rightWallPoints){
				Collider2D[] colliders = Physics2D.OverlapCircleAll(rightWallPoint.position, wallRadius, whatIsWall);
				for(int i = 0; i < colliders.Length; i++){
					if(colliders[i].gameObject != gameObject/* and layer of collider is the ground layer*/){ //if one of the ground points is colliding with something other than the player, then the player is grounded
						return true;
					}
				}
		}
		return false;
	}


	void HandleDirection(){
		transform.rotation = Quaternion.identity;
		if(rightOfPlayer){
			movingRight = false;
			GetComponent<SpriteRenderer>().flipX = false;
		}
		else{
			movingRight = true;
			GetComponent<SpriteRenderer>().flipX = true;
		}
	}

	void HandleFatalHits(){
		GameObject lifeHeartSpawn = Instantiate(lifeHeart, transform.position, Quaternion.identity) as GameObject;
		lifeHeartSpawn.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), 5f);
		//AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Destroy(gameObject);
	}
	void HandleNonFatalHits(){
	}

	void Shoot(){
		GameObject enemyShotSpawn = Instantiate(shot, transform.position, Quaternion.identity) as GameObject;
		if(rightOfPlayer){ //shoot leftwards
			GetComponent<SpriteRenderer>().flipX = false;
			enemyShotSpawn.GetComponent<Rigidbody2D>().velocity = new Vector2(-15f,0f);
		} 
		else{ //shoot rightwards
			GetComponent<SpriteRenderer>().flipX = true;
			enemyShotSpawn.GetComponent<Rigidbody2D>().velocity = new Vector2(15f,0f);
			enemyShotSpawn.GetComponent<SpriteRenderer>().flipX = false;
		}
		AudioSource.PlayClipAtPoint(enemyShotSound, transform.position);
		
	}

	bool isInRangeOfPlayer(){
		return (Mathf.Abs(transform.position.x-player.transform.position.x)<10.5 && Mathf.Abs(transform.position.y-player.transform.position.y)<5);
	}

	bool isTooCloseToPlayer(){
		return (Mathf.Abs(transform.position.x-player.transform.position.x)<3);
	}
}
