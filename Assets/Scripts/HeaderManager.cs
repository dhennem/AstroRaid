using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderManager : MonoBehaviour {

	//defined in the inspector
	public Text levelDisplay;
	public Text artifactDisplay;
	public int currentLevelNumber;  //the current level number has to be defined in the inspector with each new level

	// Use this for initialization
	void Start () {
		levelDisplay.text = "Level: " + currentLevelNumber.ToString();
		UpdateArtifactDisplay();
		
	}

	public void UpdateArtifactDisplay(){
		artifactDisplay.text = "Artifacts: " + Artifact.artifactsCollected.ToString() + "/" + ArtifactSpawner.totalNumArtifacts.ToString();
	}


	
	// Update is called once per frame
	void Update () {
		artifactDisplay.text = "Artifacts: " + Artifact.artifactsCollected.ToString() + "/" + ArtifactSpawner.totalNumArtifacts.ToString();
		
	}
}
