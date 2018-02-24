﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public ButtonControllers buttonControllers;
	public LevelManager levelManager;
	public GameObject shield;
	public GameObject shot;
	public ResourceBar healthBar;
	public ResourceBar shieldBar;
	//defined in the inspector differently for each level
	public int maxHealth;
	static public int shieldValue;

	private bool facingLeft;
	private bool facingRight;
	private bool sprinting;
	private GameObject shieldSpawn;
	private bool readyToTakeFallDamage;
	private bool isGrounded;
	[SerializeField]
	private float groundRadius;
	[SerializeField]
	private Transform[] groundPoints; //defined in the inspector to contain all of the ground points of the player
	[SerializeField]
	private LayerMask whatIsGround; //determines what layers are considered ground for the player
	[SerializeField]
	private AudioClip deathSound;
	[SerializeField]
	private AudioClip takingDamageSound;
	[SerializeField]
	private AudioClip deflectingShotSound;



	static public bool hasSuperjumpActivated;
	static public bool hasJetpackActivated;
	static public bool hasShieldActivated;

	static public float speedMultiplier;

	//0 for none, 1 for shield, 2 for jetpack, 3 for superjump
	static public int superpower;
	static public int currentHealth;


	// Use this for initialization
	void Start () {
		facingLeft = false;
		facingRight = false;
		isGrounded = true;
		speedMultiplier = 1f;
		superpower = 0;
		currentHealth = maxHealth;
		healthBar.resourceValue = currentHealth;
		healthBar.ChangeMinValue(0f);
		healthBar.ChangeMaxValue(maxHealth);
		shieldBar.resourceValue = 0f;
		shieldBar.ChangeMinValue(0f);
		shieldBar.ChangeMaxValue(1f);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!hasJetpackActivated){ 
			HandleMovement();
		}
		else{
			HandleJetpack();
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			Shoot();
		}
		HandleTurning();
		isGrounded = DetermineIfGrounded();
	}

	void HandleMovement(){
		transform.rotation = Quaternion.identity;
		if(GetComponent<Rigidbody2D>().velocity.y < -10f){
			readyToTakeFallDamage = true;   //now, whenever the player collides with something, they take fall damage
		}
		if(isGrounded){
			if((Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow)) || Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) ){ //jump to the left or to the right
				if(hasSuperjumpActivated){
					//jump higher
					GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 15f); //maintain original x velocity during a jump
				}
				else{
					//jump normally
					GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 10f); //maintain original x velocity during a jump
				}
			}
			//on the ground or a platform, running left
			else if(Input.GetKey(KeyCode.LeftArrow)){
				GetComponent<Rigidbody2D>().velocity = new Vector2(-5f*speedMultiplier, 0f);
				facingRight = false;
				facingLeft = true;
			}
			//on the ground or a platform, running right
			else if(Input.GetKey(KeyCode.RightArrow)){
				GetComponent<Rigidbody2D>().velocity = new Vector2(5f*speedMultiplier, 0f);
				facingLeft = false;
				facingRight = true;
			}
			//jumping from the ground or a platform
			else if(Input.GetKeyDown(KeyCode.UpArrow)){ //player cannot double jump
				if(hasSuperjumpActivated){
					//jump higher
					GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 15f); //maintain original x velocity during a jump
				}
				else{
					//jump normally
					GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 10f); //maintain original x velocity during a jump
				}
			}
			else{ //no key pressed, stop movement
				GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
			}
		}
		else{ //handles midair movement
			//this allows the player to move slightly left while jumping
			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				if(hasSuperjumpActivated){
					GetComponent<Rigidbody2D>().velocity = new Vector2(-4f, GetComponent<Rigidbody2D>().velocity.y);
				}
				else{
					GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, GetComponent<Rigidbody2D>().velocity.y);
				}
				facingRight = false;
				facingLeft = true;
			}
			//this allows the player to move slightly right while jumping
			if(Input.GetKeyDown(KeyCode.RightArrow)){
				if(hasSuperjumpActivated){
					GetComponent<Rigidbody2D>().velocity = new Vector2(4f, GetComponent<Rigidbody2D>().velocity.y);
				}
				else{
					GetComponent<Rigidbody2D>().velocity = new Vector2(2f, GetComponent<Rigidbody2D>().velocity.y);
				}
				facingRight = true;
				facingLeft = false;
			}
		}
	}

	void HandleJetpack(){
		transform.rotation = Quaternion.identity;
		Vector2 currentVelocity = GetComponent<Rigidbody2D>().velocity;
		if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow)){
			GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 5f); 
			facingRight = false;
			facingLeft = true;
		}
		else if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow)){
			GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 5f); 
			facingLeft = false;
			facingRight = true;
		}
		else{
			if(Input.GetKey(KeyCode.UpArrow)){
				GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocity.x, 5f); 
			}
			if(Input.GetKey(KeyCode.LeftArrow)){
				GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, currentVelocity.y);
				facingRight = false;
				facingLeft = true; 
			}
			if(Input.GetKey(KeyCode.RightArrow)){
				GetComponent<Rigidbody2D>().velocity = new Vector2(5f, currentVelocity.y);
				facingLeft = false;
				facingRight = true; 
			}
		}
		
	}

	void Shoot(){
		if(facingLeft){
			GameObject shotSpawn = Instantiate(shot, transform.position, Quaternion.identity) as GameObject;
			shotSpawn.GetComponent<Rigidbody2D>().velocity = new Vector2(-15f, 0f);
			shotSpawn.GetComponent<SpriteRenderer>().flipX = true;
		}
		else{
			GameObject shotSpawn = Instantiate(shot, transform.position, Quaternion.identity) as GameObject;
			shotSpawn.GetComponent<Rigidbody2D>().velocity = new Vector2(15f, 0f);
		}
	}

	void HandleTurning(){
		if(facingLeft){
			GetComponent<SpriteRenderer>().flipX = true;
		}
		if(facingRight){
			GetComponent<SpriteRenderer>().flipX = false;
		}
	}

	bool DetermineIfGrounded(){
		if(GetComponent<Rigidbody2D>().velocity.y <= 0f){
			foreach(Transform groundPoint in groundPoints){
				Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoint.position, groundRadius, whatIsGround);
				for(int i = 0; i < colliders.Length; i++){
					if(colliders[i].gameObject != gameObject){ //if one of the ground points is colliding with something other than the player, then the player is grounded
						return true;
					}
				}
			}
		}
		return false;
	}


	void OnCollisionEnter2D(Collision2D collision){
		if(readyToTakeFallDamage){
			readyToTakeFallDamage = false;
			HandleFallDamage();
		}

		if((collision.collider.gameObject.layer == 12 || collision.collider.gameObject.layer == 9)){ //player struck by enemy or enemy shot
			if(!hasShieldActivated){
				if(currentHealth > 1){
					HandleNonFatalHits();
				}
				else{
					HandleFatalHits();
				}
				currentHealth -=1;
				if(currentHealth<=maxHealth && healthBar.GetMaxValue() > maxHealth){
					healthBar.ChangeMaxValue(maxHealth);
				}
				healthBar.resourceValue = currentHealth;
			}
			else{
				//has shield activated
				shieldValue -= 1;
				//AudioSource.PlayClipAtPoint(deflectingShotSound, transform.position);
				if(shieldValue == 0){
					GameObject currentShieldSpawn = GameObject.FindGameObjectWithTag("Shield");
					Destroy(currentShieldSpawn);
					print("Should have destroyed the shield spawn");
					DeactivateSuperpower();
				}
			}
		}


	}

	void HandleNonFatalHits(){
		//called when the player takes a hit that wouldn't kill them
	}

	void HandleFatalHits(){
		//called when the player takes a hit that kills them
		//put in a death animation here
		levelManager.LoadLoseWithPause();
	}

	void HandleFallDamage(){
		print("Taking fall damage");
		if(currentHealth > 1){
			HandleNonFatalHits();
		}
		else{
			HandleFatalHits();
		}
		currentHealth -=1;
		if(currentHealth<=maxHealth && healthBar.GetMaxValue() > maxHealth){
			healthBar.ChangeMaxValue(maxHealth);
		}
		healthBar.resourceValue = currentHealth;
	}

	public void HandleLifeHeartPickups(int heartValue){
		if(currentHealth + heartValue > healthBar.GetMaxValue()){
			healthBar.ChangeMaxValue(healthBar.GetMaxValue() + heartValue);
		}
		currentHealth += heartValue;
		healthBar.resourceValue = currentHealth;	
	}

	public void ToggleSprint(){
		if(!sprinting){
			sprinting = true;
			speedMultiplier = 2f;
		}
		else{
			sprinting = false;
			speedMultiplier = 1f;
		}

	}

	public void ActivateNewSuperpower(){
		switch(superpower){
			case 0:
				return;
			case 1:
				ActivateShield();
				break;
			case 2:
				ActivateJetpack();
				break;
			case 3:
				ActivateSuperjump();
				break;
		}
		
	}

	void ActivateShield(){
		print("shield activated");
		hasShieldActivated = true;
		shieldSpawn = Instantiate(shield, transform.position, Quaternion.identity) as GameObject;
		//shieldSpawn.transform.parent = transform; //ensures that the shield always stays at the same position as the player while active
		shieldBar.resourceValue = shieldBar.GetMaxValue();
		shieldValue = 5;
		//Invoke("DeactivateSuperpower", 5f);
	}
	void ActivateJetpack(){
		print("jetpack activated");
		hasJetpackActivated = true;
		Invoke("DeactivateSuperpower", 20f);
	}
	void ActivateSuperjump(){
		print("superjump activated");
		hasSuperjumpActivated = true;
		Invoke("DeactivateSuperpower", 5f); //this superpower lasts for 5 seconds. Once it is over, the superpower button disappears
	}



	void DeactivateSuperpower(){
		print("Deactivating superpower");
		if(hasShieldActivated){
			shieldBar.resourceValue = shieldBar.GetMinValue();
		}
		hasShieldActivated = false;
		hasJetpackActivated = false;
		hasSuperjumpActivated = false;
		buttonControllers.DeactivateSuperpowerButton();
		superpower = 0;
	}
}