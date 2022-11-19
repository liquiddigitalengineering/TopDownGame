// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SteamAchievements : MonoBehaviour
// {
//     void Start(){
//         try{
//             Steamworks.SteamClient.Init(/*number here*/);
//         }
//         catch(System.Exception e){

//         }
//     }
//     void Update(){
//         Steamworks.SteamClient.RunCallbacks();
//     }
//     [Button]
//     public void IsThisAchievementUnlocked(string id){
//         var ach = new Steamworks.Data.Achievement(id);
//     }
//     [Button]
//     public void UnlockAchievement(string id){
//         var ach = new Steamworks.Data.Achievement(id);
//         ach.Trigger();
//     }
//     void OnApplicationQuit(){
//         Steamworks.SteamClient.Shutdown();
//     }
// }
