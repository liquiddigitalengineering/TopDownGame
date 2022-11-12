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

	public int frameRate = 60;

	void Start(){
		Application.targetFrameRate = frameRate;
	}
	public void SceneLoad(string SceneToLoad){
		SceneManager.LoadScene(SceneToLoad);
	}
	public void QuitGame(){
		Application.Quit();
	}
}

