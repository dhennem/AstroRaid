using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour {

	public Image bar;
	public Text resourceDisplay;
	public float lerpSpeed;
	public float resourceValue;
	public string typeOfResource;


	private float maxValue;
	private float minValue;

	private PlayerController player;

	// Use this for initialization
	void Start () {
		if(typeOfResource == "Shield"){
			maxValue = 5;
		}
		player = FindObjectOfType<PlayerController>();
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateFillAmount();
		UpdateTextDisplay();
		if(typeOfResource == "Shield" && PlayerController.hasShieldActivated){
			resourceValue = PlayerController.shieldValue;
			//print(maxValue);
		}
		else if(typeOfResource == "Shield" && !PlayerController.hasShieldActivated){
			resourceValue = 0;
		}
		
	}

	private void UpdateFillAmount(){
		//bar.fillAmount = Mathf.Lerp(bar.fillAmount, (resourceValue / (maxValue-minValue)), Time.deltaTime * lerpSpeed); //converts the resource value on a scale between 0 and 1
		float newFill = resourceValue / (maxValue-minValue);
		float currentFill = bar.fillAmount;
		bar.fillAmount = Mathf.Lerp(currentFill, newFill, Time.deltaTime * 2);
	}

	private void UpdateTextDisplay(){
		resourceDisplay.text = /*typeOfResource + ": " + */resourceValue.ToString();
	}

	public void ChangeMaxValue(float newValue){
		maxValue = newValue;
	}

	public void ChangeMinValue(float newValue){
		minValue = newValue;
	}

	public float GetMaxValue(){
		return maxValue;
	}
	public float GetMinValue(){
		return minValue;
	}
}
