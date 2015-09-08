using UnityEngine;
using System.Collections;
using Wedo_ClientSide;

public class Projection_enterbutton : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 2;
    public float originZ;
    public float hoverZ;
    public string ProjectionNPCName = "Projection_NPC";

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
                //判断当前项目设计页活动状态
                if (ProxyInterface.Project_GetInfo(WholeStatic.curProject.Guid).IsInRoom)
                {
                    //设计页面已开启，直接进入
                    Debug.Log("设计页面已开启，直接进入");
                    WholeStatic.curRoomInterface = RoomInterface.EnterRoom(WholeStatic.curUser.Guid,
                        WholeStatic.curProject.Guid);
                    ProjectionStatic.isTransPage = true;
                    Application.LoadLevel(Name.DESIGNROOMPAGENAME);
                }
                else
                {
                    if (WholeStatic.curProject.OwnerGuid == null
                        || WholeStatic.curProject.OwnerGuid == ""
                        || WholeStatic.curProject.OwnerGuid.Equals(WholeStatic.curUser.Guid))
                    {
                        //设计页面未开启，拥有者开启并进入
                        Debug.Log("设计页面经拥有者开启");
                        WholeStatic.curRoomInterface = RoomInterface.CreateRoom(WholeStatic.curUser.Guid,
                            WholeStatic.curProject.Guid);
                        //若开启时无图层（第一次开启）,则默认添加一个图层
                        if (WholeStatic.curRoomInterface.RoomLayers.Count == 0)
                        {
                            WholeStatic.curRoomInterface.AddLayer(1, 0, 0, 35);
                        }
                        ProjectionStatic.isTransPage = true;
                        Application.LoadLevel(Name.DESIGNROOMPAGENAME);
                    }
                    else
                    {
                        AttentionStatic.callAttention(ProjectionNPCName, "当前项目设计未开启，请等待管理员开启后进入！");
                    }
                }
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                ProjectionStatic.isTransPage = true;
                Application.LoadLevel(Name.DESIGNROOMPAGENAME);
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
