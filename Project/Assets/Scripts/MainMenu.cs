using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour
{

	void Start(){
		Application.targetFrameRate = 60;
	}
	public void SceneLoad(string SceneToLoad){
		SceneManager.LoadScene(SceneToLoad);
	}
	public void QuitGame(){
		Application.Quit();
	}
}

