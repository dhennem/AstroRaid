using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHeart : MonoBehaviour {

	public int LifeHeartValue; //defined in the inspector differently for different prefabs

	private PlayerController player;
	[SerializeField]
	private AudioClip pickupSound;


	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.collider.gameObject.tag == "Player"){
			player.HandleLifeHeartPickups(LifeHeartValue);
			AudioSource.PlayClipAtPoint(pickupSound, transform.position);
			Destroy(gameObject);
		}
	}
}
