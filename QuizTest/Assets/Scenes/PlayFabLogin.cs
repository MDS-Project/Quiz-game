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
    [SerializeField]
    public GameObject loginPanel;
    [SerializeField]
    public GameObject Error;
    public void Start()
    {
        Error.SetActive(false);
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId)){
            PlayFabSettings.TitleId = "448FE"; 
        }
        if(PlayerPrefs.HasKey("EMAIL"))
        {
        userEmail = PlayerPrefs.GetString("EMAIL");
        Debug.Log(userEmail);
        userPassword = PlayerPrefs.GetString("PASSWORD");
        Debug.Log(userPassword);
        var request = new LoginWithEmailAddressRequest {Email = userEmail, Password = userPassword};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL",userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        username = PlayerPrefs.GetString("USERNAME");
        LoadMainMenu();
        
    }

    
    private void OnLoginFailure(PlayFabError error)
    {
        StartCoroutine(Wait2());
        //var registerRequest = new RegisterPlayFabUserRequest {Email = userEmail, Password = userPassword, Username = username};
        //PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }

     IEnumerator Wait2(){

        Error.SetActive(true);
        yield return new WaitForSeconds (3);
        Error.SetActive(false);
         
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