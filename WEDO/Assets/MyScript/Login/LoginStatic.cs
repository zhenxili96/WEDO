using UnityEngine;
using System.Collections;

public class LoginStatic : MonoBehaviour
{

    public static string curFocus = "";

    // Use this for initialization
    void Start()
    {
        curFocus = "";
        LayRay.rayStyle = RayStyle.Ortho;
        LeftHandProperty.HandInit();
        RightHandProperty.HandInit();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
