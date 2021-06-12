using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderBoardManager : MonoBehaviour
{

    public GameObject rowPrefab;
    public Transform rowsParent;  

    public void Start(){
        GetLeaderboard();
    }

    public void SendLeaderboard(int score){
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "Top Scores",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);

    }

    private void OnError(PlayFabError error)
    {
        Debug.Log("Error");
    }

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result){
        Debug.Log("Successfull leaderboard sent");
    }

    public void GetLeaderboard(){
        var request = new GetLeaderboardRequest {
            StatisticName = "Top Scores",
            StartPosition = 0,
            MaxResultsCount = 5
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);

    }

    private void OnLeaderboardGet(GetLeaderboardResult result){
        foreach(var item in result.Leaderboard){
            GameObject newGo =  Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
}
