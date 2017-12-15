using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public string nextScene;

	// Use this for initialization
	public void Clicked(){
		SceneManager.LoadScene (nextScene);
	}
}
