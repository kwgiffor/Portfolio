using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShipAbilities : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	public Color active;

	public void OnPointerClick(PointerEventData eventData){
		//name.text = gameObject.name;
		//Info.text = info;
		resetOthers();
		gameObject.GetComponent<Image>().color = new Color( active.r, active.g, active.b, 0.5f);
	}

	public void OnPointerEnter(PointerEventData eD){
		RectTransform tr = gameObject.GetComponent<RectTransform> ();
		tr.position = new Vector3 (tr.position.x, 71f, 0f);
	}

	public void OnPointerExit(PointerEventData eD) {
		RectTransform tr = gameObject.GetComponent<RectTransform> ();
		tr.position = new Vector3 (tr.position.x, -43f, 0f);
	}

	public void resetOthers(){
		var temp = gameObject.GetComponent<Image> ().color;
		foreach (GameObject x in GameObject.FindGameObjectsWithTag ("Ability")) {
			x.GetComponent<Image> ().color = new Color(255, 255, 255, 0.5f);
		}
	}
}
