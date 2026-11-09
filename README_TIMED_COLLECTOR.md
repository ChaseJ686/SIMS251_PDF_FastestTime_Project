# SIMS 251 — Timed Collector with PlayFab Leaderboards

This is an extension of the uploaded Unity 6.3 PlayFab authentication project. The original `Assets/Scenes/AuthenticationDemo.unity` and authentication script are preserved. The new timed collecting game is built using an Editor menu action.

## Setup in Unity
1. Open **this project folder** (the folder containing `Assets`, `Packages`, and `ProjectSettings`) with Unity Hub using Unity 6.3.
2. Wait for script compilation. In **Edit > Project Settings > Player > Active Input Handling**, select **Both** or **Input Manager (Old)**, if necessary, and restart Unity when prompted.
3. In PlayFab Developer Portal, select your title and configure a **classic player statistic** named `HighScore` (exact capitalization). Set its aggregation to **Max** if available so later lower scores do not replace the best score. This project uses the legacy `UpdatePlayerStatistics` and `GetLeaderboard` Client APIs, not the newer entity leaderboards API.
4. From Unity's top menu choose **SIMS 251 > Create Timed Collector + Leaderboard Scene**. This generates `Assets/Scenes/TimedCollector.unity` and adds it to Build Profiles.
5. Select `Timed Collector Game + PlayFab Leaderboard` in the Hierarchy. Verify the Title ID matches your own PlayFab title. The previous screenshot showed `1641AC`, so that is the starter value.
6. Press **Play**, click **Login / Register**, enter a display name (3–25 characters), then click **START 60-SECOND ROUND**.
7. Move with WASD or arrow keys and collect yellow spheres. At 0 seconds the score is submitted to PlayFab. Click **Refresh Leaderboard** to view the top 10.
8. Confirm a score appears in PlayFab and still appears after restarting the game. For testing multiple player names, use another device/browser profile or a different stored custom ID; this project deliberately reuses your prior assignment's device-linked account.
9. **File > Build Profiles**, select Windows (or install/select WebGL/Mac), verify `TimedCollector` is included, and **Build**. Run the resulting executable to test network access.
10. Upload the Unity project and complete build folder to GitHub. Do not upload `Library`, `Temp`, `Logs`, `UserSettings`, or secret developer keys. GitHub has individual file size limits; use Git LFS or a Release asset for oversized builds.

## Notes / limitations
- Unity and the PlayFab service are not available to run inside this environment; the game has **not** been compiled or integration-tested here.
- The uploaded project contains a PlayFab SDK and a working prior login example. This game implements its own login and uses the same PlayerPrefs custom ID.
- The leaderboard requires a compatible PlayFab classic statistic named `HighScore` and permission for client statistic updates. For a classroom demo this is convenient; in production, scores should be verified server-side to prevent cheating.
- The original authentication was device-linked anonymous login, not email/password registration. This extension preserves that behavior.
- If PlayFab reports `StatisticNotFound`, `API disabled`, or another error, verify the title's statistics and API settings before changing code.
- The UI uses Unity OnGUI and the scene is generated on demand. No prefabs or third-party UI packages are required.
