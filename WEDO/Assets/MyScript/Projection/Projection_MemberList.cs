using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Wedo_ClientSide;

public class Projection_MemberList : MonoBehaviour
{
    public string OKButton = "okbutton";
    public string InviteButton = "invitebutton";
    public GameObject okbutton;
    public GameObject invitebutton;
    public Vector3 OKOriginScale;
    public Vector3 OKHoverScale;
    public float OKScaleRate = 1.3f;
    public float OKOriginZ;
    public float OKHoverZ;
    public Vector3 InviteOriginScale;
    public Vector3 InviteHoverScale;
    public float InviteScaleRate = 1.3f;
    public float InviteOriginZ;
    public float InviteHoverZ;
    public Vector3 firstPos = new Vector3(-20, 49, -0.3f);
    public Vector3 memberSpace = new Vector3(0, -22, 0);
    public string MemberPrefab = "ProjectionPrefab/memberprefab";
    public string ProjectionNPCName = "Projection_NPC";
    public string AddMemberName = "Addmember";
    public List<ClientMenber> memberList = new List<ClientMenber>();
    public string MemberNameName = "Username";
    public Vector3 memberRotation = new Vector3(0, 0, 0);
    public Vector3 memberScale = new Vector3(1, 1, 1);

    // Use this for initialization
    void Start()
    {
        okbutton = transform.FindChild(OKButton).gameObject;
        invitebutton = transform.FindChild(InviteButton).gameObject;
        OKOriginScale = okbutton.transform.localScale;
        OKHoverScale = OKOriginScale * OKScaleRate;
        OKOriginZ = okbutton.transform.localPosition.z;
        OKHoverZ = OKOriginZ - 1;
        InviteOriginScale = invitebutton.transform.localScale;
        InviteHoverScale = InviteOriginScale * InviteScaleRate;
        InviteOriginZ = invitebutton.transform.localPosition.z;
        InviteHoverZ = InviteOriginZ - 1;
        initMemberList();
    }

    public void initMemberList()
    {
        Debug.Log("init member list");
        ClientProjectDetail tempDetail = ProxyInterface.Project_GetInfo(WholeStatic.curProject.Guid);
        if (tempDetail != null)
        {
            memberList = tempDetail.Menbers;
        }
        Debug.Log("MEMBERLIST " + memberList.Count);
        for (int i = 0; i < memberList.Count; i++)
        {
            GameObject memberObject = (GameObject)Instantiate(Resources.Load(MemberPrefab));
            memberObject.transform.parent = gameObject.transform;
            memberObject.transform.FindChild(MemberNameName).GetComponent<TextMesh>().text = memberList[i].NowUser.Account;
            memberObject.transform.localPosition = firstPos + i * memberSpace;
            memberObject.transform.localEulerAngles = memberRotation;
            memberObject.transform.localScale = memberScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkInviteClick();
        checkInviteHover();
        checkOKClick();
        checkOKHover();
    }

    private void checkInviteHover()
    {
        if (RayHit.LeftHitName.Equals(InviteButton) || RayHit.RightHitName.Equals(InviteButton))
        {
            invitebutton.transform.localScale = InviteHoverScale;
            invitebutton.transform.localPosition = new Vector3(invitebutton.transform.localPosition.x,
                invitebutton.transform.localPosition.y, InviteHoverZ);
        }
        else
        {
            invitebutton.transform.localScale = InviteOriginScale;
            invitebutton.transform.localPosition = new Vector3(invitebutton.transform.localPosition.x,
                invitebutton.transform.localPosition.y, InviteOriginZ);
        }
    }

    private void checkInviteClick()
    {
        if (RayHit.LeftHitName.Equals(InviteButton) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            LeftHandProperty.clickUsed = true;
            if (WholeStatic.curProject.OwnerAccount == null
                || WholeStatic.curProject.OwnerAccount.Equals("")
                || WholeStatic.curProject.OwnerAccount.Equals(WholeStatic.curUser.Account))
            {
                gameObject.SetActive(false);
                GameObject.Find(ProjectionNPCName).transform.FindChild(AddMemberName).gameObject.SetActive(true);
                Keyboard.isOut = true;
            }
            else
            {
                AttentionStatic.callAttention(ProjectionNPCName, "无邀请权限！");
            }
        }
        if (RayHit.RightHitName.Equals(InviteButton) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            if (WholeStatic.curProject.OwnerAccount == null
                || WholeStatic.curProject.OwnerAccount.Equals("")
                || WholeStatic.curProject.OwnerAccount.Equals(WholeStatic.curUser.Account))
            {
                gameObject.SetActive(false);
                GameObject.Find(ProjectionNPCName).transform.FindChild(AddMemberName).gameObject.SetActive(true);
                Keyboard.isOut = true;
            }
            else
            {
                AttentionStatic.callAttention(ProjectionNPCName, "无邀请权限！"); 
            }
        }
    }

    private void checkOKHover()
    {
        if (RayHit.LeftHitName.Equals(OKButton) || RayHit.RightHitName.Equals(OKButton))
        {
            okbutton.transform.localScale = OKHoverScale;
            okbutton.transform.localPosition = new Vector3(okbutton.transform.localPosition.x,
                okbutton.transform.localPosition.y, OKHoverZ);
        }
        else
        {
            okbutton.transform.localScale = OKOriginScale;
            okbutton.transform.localPosition = new Vector3(okbutton.transform.localPosition.x,
                okbutton.transform.localPosition.y, OKOriginZ);
        }
    }

    private void checkOKClick()
    {
        if (RayHit.LeftHitName.Equals(OKButton) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            LeftHandProperty.clickUsed = true;
            gameObject.SetActive(false);
        }
        if (RayHit.RightHitName.Equals(OKButton) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            gameObject.SetActive(false);
        }
    }
}
