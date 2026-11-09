using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public int maxCollectables = 10;
    public GameObject playButton;
    public TextMeshProUGUI curTimeText;
    private Rigidbody rig;
    private float startTime;
    private float timeTaken;
    private int collectablesPicked;
    private bool isPlaying;
    void Awake() { rig = GetComponent<Rigidbody>(); }
    void Update()
    {
        if (!isPlaying) return;
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        rig.linearVelocity = new Vector3(x, rig.linearVelocity.y, z);
        curTimeText.text = (Time.time - startTime).ToString("F2");
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isPlaying || !other.gameObject.CompareTag("Collectable")) return;
        collectablesPicked++;
        Destroy(other.gameObject);
        if (collectablesPicked == maxCollectables) End();
    }
    public void Begin()
    {
        collectablesPicked = 0;
        startTime = Time.time;
        isPlaying = true;
        playButton.SetActive(false);
    }
    void End()
    {
        timeTaken = Time.time - startTime;
        isPlaying = false;
        rig.linearVelocity = Vector3.zero;
        playButton.SetActive(true);
        Leaderboard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000.0f));
    }
}
