using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PLayGame()
    {
        SceneManager.LoadScene("Scene_1");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Scene_home");
    }

    public void LoadOptionsMenu()
    {                         
        SceneManager.LoadScene("Scene_OptionsMenu");
    }

    public void LoadPlayMenu()
    {
        SceneManager.LoadScene("Scene_PlayMenu");
    }

    public void LoadSignup()
    {
        SceneManager.LoadScene("Scene_SignUp");
    }

    public void LoadLeaderboard()
    {
        SceneManager.LoadScene("Scene_Leaderboard");
    }
    public void LoadLogIn()
    {
	SceneManager.LoadScene("Scene_Profile");
    }
    public void LoadUpdate()
    {
	SceneManager.LoadScene("Scene_home");
    }

}
