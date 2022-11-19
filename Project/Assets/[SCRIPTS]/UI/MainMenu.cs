using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI timerText;
	public int frameRate = 60;

	private void OnEnable()
	{
		PlayerController.PlayerDiedEvent += UpdateTextTime;
	}
	private void OnDisable()
	{
        PlayerController.PlayerDiedEvent += UpdateTextTime;
    }

	void Start(){
		Application.targetFrameRate = frameRate;
	}
	public void SceneLoad(string SceneToLoad){
		SceneManager.LoadScene(SceneToLoad);
	}
	public void QuitGame(){
		Application.Quit();
	}

	private async void UpdateTextTime()
	{
		await Task.Delay(1000);
		float bestTime = PlayerPrefs.GetFloat("BestTime");
		timerText.text = $"Best time: {bestTime.ToString("#.00")} s";
	}
}

