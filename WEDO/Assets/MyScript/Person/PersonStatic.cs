using UnityEngine;
using System.Collections;

public class PersonStatic : MonoBehaviour
{

    public static string account = "";
    public static string userimage = "";
    public static string email = "";
    public static string sex = "";

    // Use this for initialization
    void Start()
    {
        getUserInfomation();
        LayRay.rayStyle = RayStyle.Ortho;
    }

    private void getUserInfomation()
    {
        //TODO 获取当前用户资料信息
    }

    // Update is called once per frame
    void Update()
    {

    }
}

