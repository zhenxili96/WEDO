using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.3f;
    public float originZ;
    public float hoverZ;

    // Use this for initialization
    void Start()
    {
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = transform.position.z;
        hoverZ = originZ - 1;
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
                LeftHandProperty.clickUsed = true;
                RoomStatic.isTransPage = true;
                WholeStatic.curRoomInterface.ExitRoom();
                if (WholeStatic.curRoomInterface.RoomUsers.Count == 0)
                {
                    WholeStatic.curRoomInterface.CloseRoom();
                }
                Application.LoadLevel(Name.MAINPROJECTIONPAGENAME);
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                RoomStatic.isTransPage = true;
                WholeStatic.curRoomInterface.ExitRoom();
                if (WholeStatic.curRoomInterface.RoomUsers.Count == 0)
                {
                    WholeStatic.curRoomInterface.CloseRoom();
                }
                Application.LoadLevel(Name.MAINPROJECTIONPAGENAME);
            }
        }
    }

    private void checkHover()
    {
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            isHover = true;
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            isHover = false;
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }
}
