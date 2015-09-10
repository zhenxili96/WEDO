using UnityEngine;
using System.Collections;
using Wedo_ClientSide;

public class Person_imageplaneconfirm : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.1f;
    public float originZ;
    public float hoverZ;
    public string PersonNPCName = "Person_NPC";

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
                if (!ProxyInterface.User_EditBaseInfo(WholeStatic.curUser.Guid, Person_userimage.tempImage,
                    WholeStatic.curUser.MailBox, WholeStatic.curUser.Sex))
                {
                    AttentionStatic.callAttention(PersonNPCName, "修改失败！");
                }
                else
                {
                    PersonStatic.userimage = Person_userimage.tempImage;
                    transform.parent.gameObject.SetActive(false);
                }
                LeftHandProperty.clickUsed = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                if (!ProxyInterface.User_EditBaseInfo(WholeStatic.curUser.Guid, Person_userimage.tempImage,
                    WholeStatic.curUser.MailBox, WholeStatic.curUser.Sex))
                {
                    AttentionStatic.callAttention(PersonNPCName, "修改失败！");
                }
                else
                {
                    PersonStatic.userimage = Person_userimage.tempImage;
                    transform.parent.gameObject.SetActive(false);
                }
                RightHandProperty.clickUsed = true;
            }
        }
        else
        {
            //if (LeftHandProperty.isClosed || RightHandProperty.isClosed)
            //{
            //    transform.parent.gameObject.SetActive(false);
            //}
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
