using UnityEngine;
using System.Collections;

public class Projection_detail : MonoBehaviour
{

    public string LeaderTextName = "leaderText";
    public string NameTextName = "nameText";
    public GameObject LeaderText;
    public GameObject NameText;

    // Use this for initialization
    void Start()
    {
        LeaderText = GameObject.Find(LeaderTextName);
        NameText = GameObject.Find(NameTextName);
    }

    // Update is called once per frame
    void Update()
    {
        LeaderText.GetComponent<TextMesh>().text = "Leader:" + ProjectionStatic.curProjectionLeader;
        NameText.GetComponent<TextMesh>().text = "Name:" + ProjectionStatic.curProjectionName;
    }
}
