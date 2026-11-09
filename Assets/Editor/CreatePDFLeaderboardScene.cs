#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public static class CreatePDFLeaderboardScene
{
    static GameObject Obj(string name, Transform parent = null) { var g = new GameObject(name); if (parent) g.transform.SetParent(parent, false); return g; }
    static TextMeshProUGUI Text(string name, Transform parent, string text, int size, TextAlignmentOptions align)
    {
        var g = Obj(name, parent); var t = g.AddComponent<TextMeshProUGUI>(); t.text=text; t.fontSize=size; t.alignment=align; t.color=Color.black; return t;
    }
    static RectTransform Rect(GameObject g, Vector2 min, Vector2 max, Vector2 pos, Vector2 size)
    {
        var r = g.GetComponent<RectTransform>(); if (!r) r=g.AddComponent<RectTransform>(); r.anchorMin=min; r.anchorMax=max; r.anchoredPosition=pos; r.sizeDelta=size; return r;
    }
    static Button Button(string name, Transform parent, string caption, Vector2 pos, Vector2 size)
    {
        var g=Obj(name,parent); Rect(g,new Vector2(.5f,.5f),new Vector2(.5f,.5f),pos,size); g.AddComponent<Image>().color=new Color(.8f,.8f,.8f);
        var b=g.AddComponent<Button>(); var t=Text("Text (TMP)",g.transform,caption,20,TextAlignmentOptions.Center); Rect(t.gameObject,Vector2.zero,Vector2.one,Vector2.zero,Vector2.zero); return b;
    }
    static Canvas Canvas(string name)
    {
        var g=Obj(name); var c=g.AddComponent<Canvas>(); c.renderMode=RenderMode.ScreenSpaceOverlay; g.AddComponent<CanvasScaler>(); g.AddComponent<GraphicRaycaster>(); return c;
    }
    [MenuItem("SIMS 251/Create PDF FastestTime Game Scene")]
    public static void Create()
    {
        var scene=EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);
        var cam=Obj("Main Camera"); cam.tag="MainCamera"; var camera=cam.AddComponent<Camera>(); camera.fieldOfView=40; cam.transform.position=new Vector3(1.96f,11.6f,-7.54f); cam.transform.rotation=Quaternion.Euler(60,0,0);
        var light=Obj("Directional Light"); light.AddComponent<Light>().type=LightType.Directional; light.transform.rotation=Quaternion.Euler(50,-30,0);
        var ground=GameObject.CreatePrimitive(PrimitiveType.Plane); ground.name="Ground"; ground.GetComponent<Renderer>().sharedMaterial=new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard")); ground.GetComponent<Renderer>().sharedMaterial.color=Color.gray;
        var player=GameObject.CreatePrimitive(PrimitiveType.Cube); player.name="Player"; player.transform.position=new Vector3(0,.5f,0); player.GetComponent<Renderer>().sharedMaterial=new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard")); player.GetComponent<Renderer>().sharedMaterial.color=Color.cyan;
        var rb=player.AddComponent<Rigidbody>(); rb.constraints=RigidbodyConstraints.FreezeRotation;
        var controller=player.AddComponent<PlayerController>(); controller.speed=3; controller.maxCollectables=10;
        var collectibles=Obj("Collectables");
        var positions=new Vector2[]{new Vector2(-4,-4),new Vector2(-2,-3),new Vector2(1,-4),new Vector2(4,-3),new Vector2(-4,0),new Vector2(-2,3),new Vector2(1,2),new Vector2(4,1),new Vector2(-4,4),new Vector2(4,4)};
        for(int i=0;i<10;i++) { var orb=GameObject.CreatePrimitive(PrimitiveType.Sphere); orb.name="Collectable"; orb.transform.SetParent(collectibles.transform); orb.transform.position=new Vector3(positions[i].x,.5f,positions[i].y); orb.transform.localScale=Vector3.one*.5f; orb.GetComponent<SphereCollider>().isTrigger=true; orb.GetComponent<Renderer>().sharedMaterial=new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard")); orb.GetComponent<Renderer>().sharedMaterial.color=Color.yellow; orb.tag="Collectable"; }
        var eventObj=Obj("EventSystem"); eventObj.AddComponent<EventSystem>(); eventObj.AddComponent<StandaloneInputModule>();
        var board=Obj("Leaderboard"); var leaderboard=board.AddComponent<Leaderboard>();
        var canvas=Canvas("LeaderboardCanvas"); canvas.transform.SetParent(board.transform); leaderboard.leaderboardCanvas=canvas.gameObject;
        var bg=Obj("LeaderboardBG",canvas.transform); Rect(bg,new Vector2(1,.5f),new Vector2(1,.5f),new Vector2(-165,0),new Vector2(300,550)); bg.AddComponent<Image>().color=new Color(.94f,.94f,.94f,.92f);
        var header=Text("Header",bg.transform,"Leaderboard",30,TextAlignmentOptions.Center); Rect(header.gameObject,new Vector2(.5f,1),new Vector2(.5f,1),new Vector2(0,-35),new Vector2(290,45));
        var entries=Obj("LeaderboardEntries",bg.transform); Rect(entries,new Vector2(.5f,1),new Vector2(.5f,1),new Vector2(0,-260),new Vector2(280,440)); var layout=entries.AddComponent<VerticalLayoutGroup>(); layout.spacing=5; layout.childControlWidth=true; layout.childForceExpandHeight=false;
        leaderboard.leaderboardEntries=new GameObject[10];
        for(int i=0;i<10;i++) {var entry=Obj("LeaderboardEntry",entries.transform); entry.AddComponent<RectTransform>().sizeDelta=new Vector2(280,35); entry.AddComponent<LayoutElement>().preferredHeight=35; entry.AddComponent<Image>().color=new Color(.3f,.3f,.3f); var pn=Text("PlayerName",entry.transform,"",16,TextAlignmentOptions.MidlineLeft);pn.color=Color.white;Rect(pn.gameObject,new Vector2(0,.5f),new Vector2(0,.5f),new Vector2(95,0),new Vector2(190,35));var st=Text("ScoreText",entry.transform,"",16,TextAlignmentOptions.MidlineRight);st.color=Color.white;Rect(st.gameObject,new Vector2(1,.5f),new Vector2(1,.5f),new Vector2(-45,0),new Vector2(80,35));leaderboard.leaderboardEntries[i]=entry;entry.SetActive(false);}
        var refresh=Button("RefreshButton",bg.transform,"Refresh",new Vector2(0,-245),new Vector2(160,35)); Rect(refresh.gameObject,new Vector2(.5f,.5f),new Vector2(.5f,.5f),new Vector2(0,-245),new Vector2(160,35)); UnityEditor.Events.UnityEventTools.AddPersistentListener(refresh.onClick,leaderboard.DisplayLeaderboard);
        var pc=Canvas("PlayerCanvas"); leaderboard.playerCanvas=pc.gameObject; var time=Text("CurTimeText",pc.transform,"0.00",36,TextAlignmentOptions.TopLeft); Rect(time.gameObject,new Vector2(0,1),new Vector2(0,1),new Vector2(100,-35),new Vector2(190,55)); controller.curTimeText=time;
        var play=Button("PlayButton",pc.transform,"Play",new Vector2(100,-95),new Vector2(150,45));Rect(play.gameObject,new Vector2(0,1),new Vector2(0,1),new Vector2(100,-95),new Vector2(150,45));controller.playButton=play.gameObject; UnityEditor.Events.UnityEventTools.AddPersistentListener(play.onClick,controller.Begin);
        pc.gameObject.SetActive(false);
        var auth=Obj("PlayFab Authentication Manager"); var a=auth.AddComponent<PlayFabAuthentication>();a.titleId="1641AC";
        // The existing authentication script displays its own Login/Register UI until sign-in succeeds.
        if(!System.Array.Exists(UnityEditorInternal.InternalEditorUtility.tags,t=>t=="Collectable")) Debug.LogWarning("Create the Collectable tag in Tags and Layers, then assign it to all ten orbs.");
        System.IO.Directory.CreateDirectory("Assets/Scenes");EditorSceneManager.SaveScene(scene,"Assets/Scenes/PDF_FastestTime.unity");EditorBuildSettings.scenes=new[]{new EditorBuildSettingsScene("Assets/Scenes/PDF_FastestTime.unity",true)};
        Selection.activeGameObject=player; Debug.Log("PDF FastestTime scene created. Configure Cloud Script and statistic in PlayFab before testing submissions.");
    }
}
#endif
