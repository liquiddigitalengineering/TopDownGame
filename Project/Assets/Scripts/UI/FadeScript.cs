using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUIGroup;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;


    public void ShowUI(){
        myUIGroup.alpha = 1;
    }

    public void HideUI(){
        myUIGroup.alpha = 0;
    }

    private void Update(){
        if(fadeIn && myUIGroup.alpha < 1){
            myUIGroup.alpha += Time.deltaTime;
            if(myUIGroup.alpha >= 1){
                fadeIn = false;
            }
        }

        if(fadeOut && myUIGroup.alpha >= 0){
            myUIGroup.alpha -= Time.deltaTime;
            if(myUIGroup.alpha == 0){
                fadeOut = false;
            }
        }
    }
}
