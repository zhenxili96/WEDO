using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.5f;
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
                Debug.Log("exit room");
                if (WholeStatic.curRoomInterface == null)
                {
                    Debug.Log("ERROR room interface null & 退出房间失败");
                }
                else
                {
                    if (!WholeStatic.curRoomInterface.ExitRoom())
                    {
                        Debug.Log("ERROR 退出房间失败");
                    }
                    //if (!WholeStatic.curRoomInterface.CloseRoom())
                    //{
                    //    Debug.Log("ERROR close room fail");
                    //}
                    if (WholeStatic.curRoomInterface.RoomUsers.Count == 0)
                    {
                        if (!WholeStatic.curRoomInterface.CloseRoom())
                        {
                            Debug.Log("ERROR close room fail");
                        }
                        else
                        {
                            Debug.Log("room 无人， 关闭room");
                        }
                    }
                }
                Application.LoadLevel(Name.MAINPROJECTIONPAGENAME);
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                RoomStatic.isTransPage = true;
                Debug.Log("exit room");
                if (WholeStatic.curRoomInterface == null)
                {
                    Debug.Log("ERROR room interface null & 退出房间失败");
                }
                else
                {
                    if (!WholeStatic.curRoomInterface.ExitRoom())
                    {
                        Debug.Log("ERROR 退出房间失败");
                    }
                    if (!WholeStatic.curRoomInterface.CloseRoom())
                    {
                        Debug.Log("ERROR close room fail");
                    }
                    //if (WholeStatic.curRoomInterface.RoomUsers.Count == 0)
                    //{
                    //    if (!WholeStatic.curRoomInterface.CloseRoom())
                    //    {
                    //        Debug.Log("ERROR close room fail");
                    //    }
                    //    else
                    //    {
                    //        Debug.Log("room 无人， 关闭room");
                    //    }
                    //}
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
