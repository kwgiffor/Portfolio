using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {
	public string resource = "";
	public int amount = 0;
	public Sprite easyCustomer;
	public Sprite mediumCustomer;
	public Sprite hardCustomer;
	public int minVal = 0;
	public int maxVal = 0;



	void Awake () {
		
		// Initializes the resource type and amount for the given customer
		this.amount = Random.Range (1, 4); // Amount of the given resource
		int resourceNum = Random.Range (1, 4); // Type of the resource
		int customerType = Random.Range (1, 4); // Customer difficulty level

		/**
		 * Assigns the resource type according to the randomly selected number
		 * above, resourceNum
		 */
		switch (resourceNum)
		{
		// Resource 1 = wood
		case 1:
			//Debug.Log ("wood");
			resource = "wood";
			break;
		// Resource 2 = berries
		case 2:
			//Debug.Log ("berries");
			this.resource = "berries";
			break;
		// Resource 3 = coal
		default:
			//Debug.Log ("coal");
			this.resource = "coal";
			break;
		}

		/**
		 * Assigns the resource type according to the randomly selected number
		 * above, resourceNum
		 */
		switch (customerType)
		{
		// 1 = Easy
		case 1:
			GetComponent<SpriteRenderer> ().sprite = easyCustomer;
			this.maxVal = 98;
			break;
		// 2 = Medium
		case 2:
			GetComponent<SpriteRenderer> ().sprite = mediumCustomer;
			this.maxVal = 39;
			break;
		// 3 = Hard
		default:
			GetComponent<SpriteRenderer> ().sprite = hardCustomer;
			this.maxVal = 10;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Request(){
		Debug.Log ("resource: " + this.resource + ", amount: " + this.amount + ", max: " + this.maxVal);
	}
}
