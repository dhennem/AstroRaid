using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	private PlayerController player;


	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController>();
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position;
		
	}
}
