using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    public Scene_Profile sceneProfile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetData() {

    }
     
    public void SaveData() {
        var request = new UpdateUserDataRequest {
            data = new Dictionary<string, string> {
                {"Age", sceneProfile.age} ,
                {"Gender", sceneProfile.gender} ,
                {"City", sceneProfile.city}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, onDataSend, OnError);
    }

    void OnDAtaSend(UpdateUserDataResult result) {
        Debug.Log("Succesfull user data send");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
