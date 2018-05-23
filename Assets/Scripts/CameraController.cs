using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;


	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(player!=null){
			Vector3 unboundedCameraPos = player.transform.position + offset;
			float cameraXPos = Mathf.Clamp(unboundedCameraPos.x, 12f, 52f);
			float cameraYPos = Mathf.Clamp(unboundedCameraPos.y + 5.12f, 9f, 27f);  //5.12 is the height of the ground blocks. This ensures that the camera follows the player when they move upwards appropriately
			float cameraZPos = unboundedCameraPos.z;

			transform.position = new Vector3(cameraXPos, cameraYPos, cameraZPos);
		}
	}
}
