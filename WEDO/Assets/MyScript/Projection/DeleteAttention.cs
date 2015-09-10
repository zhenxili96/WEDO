using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Wedo_ClientSide;

public class DeleteAttention : MonoBehaviour
{
    public string OKButtonName = "okbutton";
    public string CancelButtonName = "cancelbutton";
    public GameObject okbutton;
    public GameObject cancelbutton;
    public Vector3 okHoverScale;
    public Vector3 okOriginScale;
    public Vector3 cancelHoverScale;
    public Vector3 cancelOriginScale;
    public float okScaleRate = 1.3f;
    public float cancelScaleRate = 1.3f;
    public float okHoverZ;
    public float okOriginZ;
    public float cancelHoverZ;
    public float cancelOriginZ;
    public string AttentTextName = "attentiontext";
    public string ProjectionNPCName = "Projection_NPC";

    // Use this for initialization
    void Start()
    {
        okbutton = transform.FindChild(OKButtonName).gameObject;
        cancelbutton = transform.FindChild(CancelButtonName).gameObject;
        okOriginScale = okbutton.transform.localScale;
        cancelOriginScale = cancelbutton.transform.localScale;
        okHoverScale = okScaleRate * okOriginScale;
        cancelHoverScale = cancelScaleRate * cancelOriginScale;
        okOriginZ = okbutton.transform.localPosition.z;
        okHoverZ = okOriginZ - 1;
        cancelOriginZ = cancelbutton.transform.localPosition.z;
        cancelHoverZ = cancelOriginZ - 1;
        transform.FindChild(AttentTextName).GetComponent<TextMesh>().text = "确定删除项目？";
    }

    // Update is called once per frame
    void Update()
    {
        checkCancelClick();
        checkCancelHover();
        checkOkClick();
        checkOkHover();
    }


    private void checkOkHover()
    {
        if (RayHit.LeftHitName.Equals(OKButtonName) || RayHit.RightHitName.Equals(OKButtonName))
        {
            okbutton.transform.localScale = okHoverScale;
            okbutton.transform.localPosition = new Vector3(okbutton.transform.localPosition.x,
                okbutton.transform.localPosition.y, okHoverZ);
        }
        else
        {
            okbutton.transform.localScale = okOriginScale;
            okbutton.transform.localPosition = new Vector3(okbutton.transform.localPosition.x,
                okbutton.transform.localPosition.y, okOriginZ);
        }
    }

    private void checkOkClick()
    {
        if (RayHit.LeftHitName.Equals(OKButtonName)
            && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            LeftHandProperty.clickUsed = true;
            //TODO 删除项目
            if (!ProxyInterface.Project_Delete(WholeStatic.curProject.Guid, WholeStatic.curUser.Guid))
            {
                AttentionStatic.callAttention(ProjectionNPCName, "删除项目失败，非项目创建者无权限删除项目！");
                Debug.Log("删除项目失败，非项目创建者无权限删除项目！");
            }
            else
            {
                Application.LoadLevel(Name.HOMEPAGENAME);
            }
        }
        if (RayHit.RightHitName.Equals(OKButtonName)
            && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            LeftHandProperty.clickUsed = true;
            //TODO 删除项目
            if (!ProxyInterface.Project_Delete(WholeStatic.curProject.Guid, WholeStatic.curUser.Guid))
            {
                AttentionStatic.callAttention(ProjectionNPCName, "删除项目失败，非项目创建者无权限删除项目！");
                Debug.Log("删除项目失败，非项目创建者无权限删除项目！");
            }
            else
            {
                Application.LoadLevel(Name.HOMEPAGENAME);
            }
        }
    }

    private void checkCancelHover()
    {
        if (RayHit.LeftHitName.Equals(CancelButtonName)
            || RayHit.RightHitName.Equals(CancelButtonName))
        {
            cancelbutton.transform.localScale = cancelHoverScale;
            cancelbutton.transform.localPosition = new Vector3(cancelbutton.transform.localPosition.x,
                cancelbutton.transform.localPosition.y, cancelHoverZ);
        }
        else
        {
            cancelbutton.transform.localScale = cancelOriginScale;
            cancelbutton.transform.localPosition = new Vector3(cancelbutton.transform.localPosition.x,
                cancelbutton.transform.localPosition.y, cancelOriginZ);
        }
    }

    private void checkCancelClick()
    {
        if (RayHit.LeftHitName.Equals(CancelButtonName)
            && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            LeftHandProperty.clickUsed = true;
            gameObject.SetActive(false);
        }
        if (RayHit.RightHitName.Equals(CancelButtonName)
            && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            gameObject.SetActive(false);
        }
    }
}
