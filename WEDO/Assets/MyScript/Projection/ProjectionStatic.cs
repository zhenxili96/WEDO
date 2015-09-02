using UnityEngine;
using System.Collections;

public class ProjectionStatic : MonoBehaviour
{

    public static string curProjectionName = "";
    public static string curProjectionLeader = "";
    public static string curProjectionNotice = "";
    public static string curProjectionProgress = "";
    public static string curProjecitonID = "";

    // Use this for initialization
    void Start()
    {
        LayRay.rayStyle = RayStyle.Perspect;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
