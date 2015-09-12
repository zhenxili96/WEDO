using UnityEngine;
using System.Collections;

public class InstanceType : MonoBehaviour
{

    public int Type = 0;
    public string MyGuid = RoomStatic.UNSETGUID;
    public string LayerGuid = RoomStatic.UNSETGUID;
    public string colorString = "C7";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("my colot " + colorString);
        transform.GetChild(0).renderer.material.color = ColorTable.getColor(colorString);
    }
}
