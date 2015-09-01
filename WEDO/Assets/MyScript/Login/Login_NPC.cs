using UnityEngine;
using System.Collections;

public class Login_NPC : MonoBehaviour
{

    private float upSpeed = 16f;
    private float downSpeed = 20f;
    private float distance = 11;
    private float topBorder = 11;
    private float bottomBorder = 0;
    private bool isHover = false;
    public static bool isOut = true;
    public static bool isOpen = true;
    public Vector3 outPos = new Vector3(0, 27, -50);
    public Vector3 inPos = new Vector3(0, 75, -60);
    public float outSpeed = 40f;
    public float inSpeed = 50f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkOut();
    }

    private void checkOut()
    {
        isOpen = false;
        if (isOut)
        {
            if (transform.position.y > outPos.y)
            {
                transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * outSpeed);
            }
            else
            {
                isOpen = true;
            }
        }
        else
        {
            if (transform.position.y < inPos.y)
            {
                transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * inSpeed);
            }
        }
    }

    private void checkHover()
    {
        isHover = false;
        foreach (Transform child in transform)
        {
            if (child.name.Equals("Login_button") || child.name.Equals("Login_signtext"))
            {
                continue;
            }
            if (RayHit.LeftHitName.Equals(child.name) && !LeftHandProperty.isClosed
                || RayHit.RightHitName.Equals(child.name) && !RightHandProperty.isClosed)
            {
                isHover = true;
            }
        }
        if (isHover)
        {
            isOut = false;
            Login_Keyboard.isOut = true;
        }
    }
}
