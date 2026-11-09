
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public GameObject leaderboardCanvas;
    public GameObject playerCanvas;
    public GameObject loginCanvas;
    public GameObject[] leaderboardEntries;

    public static Leaderboard instance;

    void Awake()
    {
        instance = this;
    }

    public void OnLoggedIn()
    {
        if (loginCanvas != null)
            loginCanvas.SetActive(false);

        if (playerCanvas != null)
            playerCanvas.SetActive(true);

        if (leaderboardCanvas != null)
            leaderboardCanvas.SetActive(true);

        DisplayLeaderboard();
    }

    public void DisplayLeaderboard()
    {
        GetLeaderboardRequest request = new GetLeaderboardRequest
        {
            StatisticName = "FastestTime",
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(
            request,
            result => UpdateLeaderboardUI(result.Leaderboard),
            error => Debug.LogError(
                "Leaderboard error: " + error.GenerateErrorReport()
            )
        );
    }

    void UpdateLeaderboardUI(List<PlayerLeaderboardEntry> leaderboard)
    {
        for (int x = 0; x < leaderboardEntries.Length; x++)
        {
            if (x >= leaderboard.Count)
            {
                leaderboardEntries[x].SetActive(false);
                continue;
            }

            leaderboardEntries[x].SetActive(true);

            leaderboardEntries[x].transform.Find("PlayerName")
                .GetComponent<TextMeshProUGUI>().text =
                (leaderboard[x].Position + 1) + ". " +
                leaderboard[x].DisplayName;

            leaderboardEntries[x].transform.Find("ScoreText")
                .GetComponent<TextMeshProUGUI>().text =
                (leaderboard[x].StatValue * 0.001f).ToString("F2");
        }
    }

    public void SetLeaderboardEntry(int newScore)
    {
        ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest
        {
            FunctionName = "UpdateHighscore",
            FunctionParameter = new { score = newScore },
            GeneratePlayStreamEvent = true
        };

        PlayFabClientAPI.ExecuteCloudScript(
            request,
            result =>
            {
                if (result.Error != null)
                {
                    Debug.LogError(
                        "Cloud Script error: " + result.Error.Message
                    );
                }
                else
                {
                    Debug.Log("Score submitted.");
                    DisplayLeaderboard();
                }
            },
            error => Debug.LogError(
                "Score submission error: " + error.GenerateErrorReport()
            )
        );
    }
}
