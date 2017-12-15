using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShipSections : MonoBehaviour, IPointerClickHandler {

	public string info = "";

	public GameObject sectInfo;
	public Text name;
	public Text Info;

	public void OnPointerClick(PointerEventData eventData){
		sectInfo.GetComponent<Image> ().color = new Color(gameObject.GetComponent<Image> ().color.r, gameObject.GetComponent<Image> ().color.g, gameObject.GetComponent<Image> ().color.b );
		name.text = gameObject.name;
		info = info.Replace ("\\n", "\n");
		Info.text = info;
	}
}

