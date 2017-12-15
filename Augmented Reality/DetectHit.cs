
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class DetectHit : MonoBehaviour {

	
    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ShootRay(ray);
        }
    }

    void ShootRay(Ray ray)
    {
        RaycastHit rhit;
        bool objectHit = false;
        GameObject gHit = null;

        if (Physics.Raycast(ray, out rhit, 1000f))
        {
            Debug.Log(rhit.collider.gameObject);
			if (rhit.collider.gameObject) {
				foreach (Transform x in gameObject.GetComponentInChildren<Transform>()) {
					x.gameObject.SetActive (!x.gameObject.activeSelf);
				}
			}

        }
    }
}
