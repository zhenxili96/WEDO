using UnityEngine;
using System.Collections;

public class KeyBoardBackButton : MonoBehaviour
{

    private bool isHover = false;
    private Color originColor;
    private Color newColor = Color.red;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
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
                RoomKeyBoard.isOut = false;
                LeftHandProperty.clickUsed = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RoomKeyBoard.isOut = false;
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
