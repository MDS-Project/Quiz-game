using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayFabSignUp : MonoBehaviour
{
    private string username;
    private string userEmail;
    private string userPassword;    
    public GameObject loginPanel;
    [SerializeField]
    public GameObject Error;
    public void Start()
    {
        Error.SetActive(false);
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId)){
            PlayFabSettings.TitleId = "448FE"; 
        }
    }

    public void OnClickSingUp()
    {
        var registerRequest = new RegisterPlayFabUserRequest {Email = userEmail, Password = userPassword, Username = username};
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }
   
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL",userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        PlayFabClientAPI.UpdateUserTitleDisplayName( new UpdateUserTitleDisplayNameRequest {
        DisplayName = username
     }, resul => {
        Debug.Log("The player's display name is now: " + resul.DisplayName);
    }, error => Debug.LogError(error.GenerateErrorReport()));
        LoadMainMenu();
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        StartCoroutine(Wait2());
        Debug.LogError(error.GenerateErrorReport());
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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Scene_home");
    }
}