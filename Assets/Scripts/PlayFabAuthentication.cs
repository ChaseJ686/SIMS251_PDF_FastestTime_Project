using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabAuthentication : MonoBehaviour
{
    [Header("Paste your PlayFab Title ID here")]
    public string titleId = "";
    private string status = "Enter your PlayFab Title ID in the Inspector.";
    private bool busy;
    private string playerId = "";

    private void Start()
    {
        if (!string.IsNullOrWhiteSpace(titleId))
        {
            PlayFabSettings.staticSettings.TitleId = titleId.Trim();
            status = "Ready. Click Login / Register.";
        }
    }

    public void Login()
    {
        if (busy) return;
        if (string.IsNullOrWhiteSpace(titleId)) { status = "Missing Title ID. Set it in the Inspector."; return; }
        PlayFabSettings.staticSettings.TitleId = titleId.Trim();
        busy = true;
        status = "Contacting PlayFab...";
        // Demo authentication: creates a PlayFab account on first use for this device.
        var request = new LoginWithCustomIDRequest
        {
            CustomId = GetOrCreateDeviceId(),
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private string GetOrCreateDeviceId()
    {
        const string key = "SIMS251_PlayFab_DemoId";
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetString(key, Guid.NewGuid().ToString("N"));
            PlayerPrefs.Save();
        }
        return PlayerPrefs.GetString(key);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        busy = false;
        playerId = result.PlayFabId;
        status = "Login successful!";
        Debug.Log("PlayFab login successful. PlayFab ID: " + playerId);
        if (Leaderboard.instance != null) Leaderboard.instance.OnLoggedIn();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        busy = false;
        status = "Login failed: " + error.ErrorMessage;
        Debug.LogError(error.GenerateErrorReport());
    }

    private void OnGUI()
    {
        if (!string.IsNullOrEmpty(playerId)) return;
        var area = new Rect(Mathf.Max(10, Screen.width / 2f - 230), Mathf.Max(10, Screen.height / 2f - 125), 460, 250);
        GUILayout.BeginArea(area, GUI.skin.box);
        GUILayout.Space(12);
        GUILayout.Label("SIMS 251 - PlayFab Authentication");
        GUILayout.Space(10);
        GUILayout.Label(status);
        GUILayout.Space(12);
        GUI.enabled = !busy;
        if (GUILayout.Button("Login / Register", GUILayout.Height(48))) Login();
        GUI.enabled = true;
        if (!string.IsNullOrEmpty(playerId)) GUILayout.Label("PlayFab ID: " + playerId);
        GUILayout.Space(10);
        GUILayout.Label("Demo: anonymous device-linked account, not email/password.");
        GUILayout.EndArea();
    }
}
