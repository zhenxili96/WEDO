using UnityEngine;
using System.Collections;
using Wedo_ClientSide;

public class Projection_subadd : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.1f;
    public float originZ;
    public float hoverZ;
    public string ProjectionNPCName = "Projection_NPC";
    public string AddprojectName = "Addproject";

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
                //权限检查
                if (!WholeStatic.curUser.Account.Equals(ProjectionStatic.curProjectionLeader))
                {
                    AttentionStatic.callAttention(ProjectionNPCName, "非项目发起人无权限添加子项目！");
                    return;
                } 
                GameObject addProjectWindows = GameObject.Find(ProjectionNPCName).transform.FindChild(AddprojectName).gameObject;
                addProjectWindows.SetActive(true);
                addProjectWindows.GetComponent<Projection_Addproject>().ProjectionNPCName = "";
                Keyboard.isOut = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                //权限检查
                if (!WholeStatic.curUser.Account.Equals(ProjectionStatic.curProjectionLeader))
                {
                    AttentionStatic.callAttention(ProjectionNPCName, "非项目发起人无权限添加子项目！");
                    return;
                }
                GameObject addProjectWindows = GameObject.Find(ProjectionNPCName).transform.FindChild(AddprojectName).gameObject;
                addProjectWindows.SetActive(true);
                addProjectWindows.GetComponent<Projection_Addproject>().ProjectionNPCName = "";
                Keyboard.isOut = true;
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
