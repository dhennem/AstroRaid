using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControllers : MonoBehaviour {

	//defined in the inspector
	public Button sprintButton;
	public Button sButton;
	public Button jpButton;
	public Button sjButton;
	/*public Color readyButtonColor;
	public Color fadedButtonColor;*/


	static public Button shieldButton;
	static public Button jetpackButton;
	static public Button superjumpButton;

	// Use this for initialization
	void Start () {
		//these buttons need to be static, but they also have to be defined in the inspector, so this strategy gets around that
		shieldButton = sButton;
		jetpackButton = jpButton;
		superjumpButton = sjButton;
		/*ToggleButtonInvisibility(shieldButton);
		ToggleButtonInvisibility(jetpackButton);
		ToggleButtonInvisibility(superjumpButton);*/
		shieldButton.gameObject.SetActive(false);
		jetpackButton.gameObject.SetActive(false);
		superjumpButton.gameObject.SetActive(false);
		/*shieldButton.GetComponent<Image>().color = readyButtonColor;
		jetpackButton.GetComponent<Image>().color = readyButtonColor;
		superjumpButton.GetComponent<Image>().color = readyButtonColor;*/
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ToggleButtonInvisibility(Button button){
		print("Trying to toggle button visiblity");
		bool active = button.IsActive();
		if(active){
			print("Trying to make superpower button invisible");
			button.gameObject.SetActive(false);
		}
		else{
			print("Trying to make superpower button visible");
			button.gameObject.SetActive(true);
			//button.GetComponent<Image>().color = readyButtonColor; 
		}
	}

	public void ActivateSuperpowerButton(){
		print("Trying to activate superpower button");
		if(PlayerController.superpower == 1){
			ToggleButtonInvisibility(shieldButton);
		}
		if(PlayerController.superpower == 2){
			ToggleButtonInvisibility(jetpackButton);
		}
		if(PlayerController.superpower == 3){
			ToggleButtonInvisibility(superjumpButton);
		}
	}
	public void DeactivateSuperpowerButton(){
		if(PlayerController.superpower == 1){
			ToggleButtonInvisibility(shieldButton);
		}
		if(PlayerController.superpower == 2){
			ToggleButtonInvisibility(jetpackButton);
		}
		if(PlayerController.superpower == 3){
			ToggleButtonInvisibility(superjumpButton);
		}
	}
	public void fadeButtons(){
		/*switch(PlayerController.superpower){
			case 1:
				//fade shield button
				shieldButton.GetComponent<Image>().color = fadedButtonColor;
				break;
			case 2:
				//fade jetpack button
				jetpackButton.GetComponent<Image>().color = fadedButtonColor;
				break;
			case 3:
				//fade SJ button
				superjumpButton.GetComponent<Image>().color = fadedButtonColor;
				break;
		}*/
	}
}
