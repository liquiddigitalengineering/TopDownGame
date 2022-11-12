using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer musicMixer;

    [SerializeField] Slider audioSlider;
    [SerializeField] Slider musicSlider;

    void Start(){
        Load();
    }
    public void Load(){     //Load() is its own function so it can be called anytime it needs to be, rather than the code just being in Start()
        audioMixer.SetFloat("MainVolume", PlayerPrefs.GetFloat("masterVolume"));
        musicMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("musicVolume"));
        audioSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    public void SetVolume (float volume)
    {
        if(volume <= -39){
            audioMixer.SetFloat("MainVolume", -80);
            PlayerPrefs.SetFloat("masterVolume", -80);}
        else{
            audioMixer.SetFloat("MainVolume", volume);
            PlayerPrefs.SetFloat("masterVolume", volume);}

    }
    public void SetMusic (float volume)
    {
        if(volume <= -39){
            musicMixer.SetFloat("MusicVolume", -80);
            PlayerPrefs.SetFloat("musicVolume", -80);}
        else{
            musicMixer.SetFloat("MusicVolume", volume);
            PlayerPrefs.SetFloat("musicVolume", volume);}
    }
    public void MuteVolume(bool boolMute){
        if(boolMute == true){audioMixer.SetFloat("MainVolume", -80);}
        if(boolMute == false){audioMixer.SetFloat("MainVolume", PlayerPrefs.GetFloat("masterVolume"));}
    }
    public void MuteMusic(bool boolMute){
        if(boolMute == true){musicMixer.SetFloat("MusicVolume", -80);}
        if(boolMute == false){musicMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("musicVolume"));}
    }

}
