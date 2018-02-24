using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	public AudioClip[] levelMusicArray;

	private AudioSource audiosource;

	void Awake(){
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource>();
		
	}
	
	void OnLevelWasLoaded(int level){
		AudioClip currentLevelMusic = levelMusicArray[level];
		if(currentLevelMusic!=null){
			audiosource.clip = currentLevelMusic;
			audiosource.loop = true;
			audiosource.Play();
		}
	}

	public void SetVolume(float volume){
		audiosource.volume = volume;
	}
}
