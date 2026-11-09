# SIMS 251 - M04 DV01: PlayFab Authentication Demo

Unity demo that logs into Microsoft PlayFab using an anonymous, locally saved custom ID. The first login registers a new account; later logins reuse it on the same browser/device storage.

## Setup
1. Open this folder with Unity Hub (Unity 6.3 LTS (6000.3.4f1), matching your installed Editor).
2. Import the official **PlayFab Unity SDK** from PlayFab's GitHub repository: https://github.com/PlayFab/UnitySDK . Follow the repository's installation instructions. The scripts depend on the PlayFab SDK and will show compilation errors until it is imported.
3. Create a PlayFab title in the PlayFab developer portal and copy its **Title ID**.
4. In Unity, use **SIMS 251 > Create Authentication Demo Scene**.
5. Select **PlayFab Authentication Manager** in the Hierarchy and paste the Title ID in the Inspector.
6. Save the scene (Ctrl+S). Press Play and click **Login / Register**. Check the status and Unity Console.
7. Build via **File > Build Settings** (or **Build Profiles** in newer Unity): select WebGL or Windows, switch platform, and Build to a folder named `WebGLBuild` or `WindowsBuild`.
8. For the class submission, upload your full Unity project including Assets, Packages, ProjectSettings, and the build folder to GitHub. Do not commit Library, Temp, or Obj. The .gitignore intentionally excludes generic Build/Builds but permits WebGLBuild and WindowsBuild.

## Scope
This is a prepared Unity 6.3-targeted starter project, **not a tested Unity build**. The package manifest is deliberately minimal so Unity can resolve its own built-in packages. It uses PlayFab custom-ID authentication and Unity's built-in OnGUI interface, rather than implementing the exact steps of the linked UGS video. It does not contain the PlayFab SDK or a live Title ID, and is not secure production authentication. For a tutorial-exact submission, compare with the video and adapt accordingly.
