using UnityEngine;
using System.Collections;

public class Projection_detail : MonoBehaviour
{

    public string LeaderTextName = "leaderText";
    public string NameTextName = "nameText";
    public string NoticeTextName = "noticeText";
    public string ProgressTextName = "progressText";
    public GameObject LeaderText;
    public GameObject NameText;
    public GameObject NoticeText;
    public GameObject ProgressText;

    // Use this for initialization
    void Start()
    {
        LeaderText = GameObject.Find(LeaderTextName);
        NameText = GameObject.Find(NameTextName);
        NoticeText = GameObject.Find(NoticeTextName);
        ProgressText = GameObject.Find(ProgressTextName);
    }

    // Update is called once per frame
    void Update()
    {
        LeaderText.GetComponent<TextMesh>().text = "Name:" + ProjectionStatic.curProjectionLeader;
        NameText.GetComponent<TextMesh>().text = "Leader:" + ProjectionStatic.curProjectionName;
        NoticeText.GetComponent<TextMesh>().text = "Notice:" + ProjectionStatic.curProjectionNotice;
        ProgressText.GetComponent<TextMesh>().text = "Progress:" + ProjectionStatic.curProjectionProgress;
    }
}
