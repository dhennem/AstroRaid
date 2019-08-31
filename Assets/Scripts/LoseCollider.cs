using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {

	public LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision){
		//player loses when they touch lose collider
		if(collision.collider.name == "Player"){
			Destroy(collision.collider.gameObject);
			if(PlayerController.hasShieldActivated){
				GameObject shieldSpawn = GameObject.Find("Shield(Clone)");
				Destroy(shieldSpawn);
			}
			levelManager.LoadLoseWithPause();
		}

		//enemies are killed when they touch lose collider
		if(collision.collider.IsTouchingLayers(9)){
			Destroy(collision.collider.gameObject);
		}
	}
}
