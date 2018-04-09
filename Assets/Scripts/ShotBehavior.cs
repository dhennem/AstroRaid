using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehavior : MonoBehaviour {

	[SerializeField]
	private AudioClip shotSound;

	// Use this for initialization
	void Start () {
		AudioSource.PlayClipAtPoint(shotSound, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision){
		Destroy(gameObject);
	}
}
