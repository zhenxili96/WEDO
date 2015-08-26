using UnityEngine;
using System.Collections;

public class RoomKey : MonoBehaviour
{

    private bool isHover = false;
    private Color originColor;
    private bool isPressed = false;
    private string MySelf = "";

    public static string curSentence;

    // Use this for initialization
    void Start()
    {
        originColor = gameObject.renderer.material.color;
        MySelf = name;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkClick();
    }

    private void checkClick()
    {
        if (isHover)
        {
            if (LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                curSentence += MySelf;
                LeftHandProperty.clickUsed = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                curSentence += MySelf;
                RightHandProperty.clickUsed = true;
            }
        }
    }

    private void checkHover()
    {
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            isHover = true;
            renderer.material.color = Color.red;
        }
        else
        {
            isHover = false;
            renderer.material.color = originColor;
        }
    }
}
