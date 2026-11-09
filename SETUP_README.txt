SIMS 251 - PDF FASTESTTIME PROJECT

This project is derived from your supplied SIMS251_TimedCollector_Unity_Project(2).zip.
The old HighScore / 60-second runtime game was removed. Your PlayFabAuthentication script was retained and connected to the PDF leaderboard on successful login.

IN UNITY 6.3:
1. Open this folder as a project in Unity Hub and wait for packages to resolve.
2. In Window > Package Manager, ensure Physics, uGUI and TextMeshPro are installed. Import TMP Essentials if prompted.
3. From the top menu select SIMS 251 > Create PDF FastestTime Game Scene. Do not use the old Create Timed Collector command.
4. Open Assets/Scenes/PDF_FastestTime.unity if it isn't already open. Press Play.
5. Click Login / Register, then Play. Collect the 10 yellow spheres with WASD or arrows.
6. In PlayFab create a LEGACY statistic FastestTime, Manual reset, MINIMUM aggregation, matching the PDF.
7. In PlayFab Automation > Cloud Script (Legacy), paste Assets/CloudScript/UpdateHighscore.js and deploy the revision.
8. In PlayFab API Features, enable 'Allow client to post player statistics' ONLY if your specific legacy setup needs it; the PDF instead submits via Cloud Script.
9. Build Windows or WebGL in File > Build Profiles, add the scene, and upload project (excluding Library/Temp) and build to GitHub.

IMPORTANT:
- This is a source project, not a verified compiled Unity build. Test in Unity.
- The existing login uses a device-linked Custom ID rather than the email/password login UI in the PDF. This is the one retained component from your supplied working project.
- For the PDF's exact email/password authentication UI, you would need the prior login/register source prefab or scripts; they were not present in this ZIP.
- The PDF's PlayFab Cloud Script example uses the legacy server statistics API; confirm it is available to your title.
- The PDF's score submission format is negative milliseconds. It expects MINIMUM aggregation; confirm ranking behavior in your title.
- The original ten orbs are not respawned after a completed run, consistent with the PDF. Re-enter Play Mode to retry.
