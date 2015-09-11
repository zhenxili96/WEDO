using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Wedo_ClientSide;

public class Projection_detailnotice : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.1f;
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
                //TODO 呼出展示公告窗
                List<ClientMessage> notice = ProxyInterface.Project_GetInfo(WholeStatic.curProject.Guid).Announcements;
                AttentionStatic.callAttention(ProjectionNPCName, transformNotice(notice));
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                //TODO 呼出展示公告窗
                List<ClientMessage> notice = ProxyInterface.Project_GetInfo(WholeStatic.curProject.Guid).Announcements;
                AttentionStatic.callAttention(ProjectionNPCName, transformNotice(notice));
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

    private string transformNotice(List<ClientMessage> notice)
    {
        string result = "";

        foreach (ClientMessage cm in notice)
        {
            result += (cm.Cont + "\n");
        }

        if (result.Equals(""))
        {
            result = "暂无公告"; 
        }
        return result;
    }
}
