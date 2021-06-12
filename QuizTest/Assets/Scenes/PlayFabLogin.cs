using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayFabLogin : MonoBehaviour
{
    private string username;
    private string userEmail;
    private string userPassword;    
    public GameObject loginPanel;

    public void Start()
    {
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId)){
            PlayFabSettings.TitleId = "448FE"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true};
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        if(PlayerPrefs.HasKey("EMAIL"))
        {
        userEmail = PlayerPrefs.GetString("EMAIL");
        userPassword = PlayerPrefs.GetString("PASSWORD");
        var request = new LoginWithEmailAddressRequest {Email = userEmail, Password = userPassword};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL",userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginPanel.SetActive(false);
        LoadMainMenu();
        
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL",userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginPanel.SetActive(false);
        //result.InfoResultPayload.PlayerProfile.DisplayName = "anonim";
        LoadMainMenu();
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.Log("Eroare");
        Debug.LogError(error.GenerateErrorReport());
    }
    
    private void OnLoginFailure(PlayFabError error)
    {
        var registerRequest = new RegisterPlayFabUserRequest {Email = userEmail, Password = userPassword, Username = username};
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }

    public void GetUserEmail (string emailIn)
    {
        userEmail = emailIn;
    }

    public void GetUserPassword (string passwordIn)
    {
        userPassword = passwordIn;
    }

     public void GetUserUsername (string usernameIn)
    {
        username= usernameIn;

    }

    public void OnClickLogin()
    {
        var request = new LoginWithEmailAddressRequest {Email = userEmail, Password= userPassword};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Scene_home");
    }
}