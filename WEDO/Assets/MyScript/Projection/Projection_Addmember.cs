using UnityEngine;
using System.Collections;
using Wedo_ClientSide;

public class Projection_Addmember : MonoBehaviour
{

    public string CancelButton = "cancelbutton";
    public string ConfirmButton = "confirmbutton";
    public string InputLine = "inputline";
    public string SearchIcon = "searchicon";

    public Vector3 confirmOriginScale;
    public Vector3 confirmHoverScale;
    public float confirmScaleRate = 1.3f;
    public float confirmOriginZ;
    public float confirmHoverZ;
    public GameObject confirmbutton;

    public Vector3 cancelOriginScale;
    public Vector3 cancelHoverScale;
    public float cancelScaleRate = 1.3f;
    public float cancelOriginZ;
    public float cancelHoverZ;
    public GameObject cancelbutton;

    public Vector3 searchOriginScale;
    public Vector3 searchHoverScale;
    public float searchScaleRate = 1.3f;
    public float searchOriginZ;
    public float searchHoverZ;
    public GameObject searchicon;
    public string InviteMemberPrefab = "ProjectionPrefab/invitememberprefab";
    public string MemberNameName = "Username";
    public string ProjectionNPCName = "Projection_NPC";

    public string inputAccount = "";
    public ClientUser tempInvite = null;

    // Use this for initialization
    void Start()
    {
        Keyboard.curSentence = "";
        Keyboard.isOut = true;
        confirmbutton = transform.FindChild(ConfirmButton).gameObject;
        cancelbutton = transform.FindChild(CancelButton).gameObject;
        searchicon = transform.FindChild(SearchIcon).gameObject;
        confirmOriginScale = confirmbutton.transform.localScale;
        confirmHoverScale = confirmOriginScale * confirmScaleRate;
        confirmOriginZ = confirmbutton.transform.position.z;
        confirmHoverZ = confirmOriginZ - 1;
        cancelOriginScale = cancelbutton.transform.localScale;
        cancelHoverScale = cancelOriginScale * cancelScaleRate;
        cancelOriginZ = cancelbutton.transform.position.z;
        cancelHoverZ = cancelOriginZ - 1;
        searchOriginScale = searchicon.transform.localScale;
        searchHoverScale = searchOriginScale * searchScaleRate;
        searchOriginZ = searchicon.transform.position.z;
        searchHoverZ = searchOriginZ - 1;
    }

    // Update is called once per frame
    void Update()
    {
        checkInputContent();
        checkSearchClick();
        checkSearchHover();
        checkCancelClick();
        checkCancelHover();
        checkConfirmClick();
        checkConfirmHover();
    }

    private void checkInputContent()
    {
        transform.FindChild(InputLine).GetChild(0).GetComponent<TextMesh>().text = Keyboard.curSentence;
        inputAccount = Keyboard.curSentence;
    }

    private void checkSearchClick()
    {
        if (RayHit.LeftHitName.Equals(SearchIcon) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            LeftHandProperty.clickUsed = true;
            tempInvite = null;
            //TODO 根据输入账号查找用户
            tempInvite = ProxyInterface.User_GetDetailByAccount(inputAccount);
            if (tempInvite != null)
            {
                GameObject tempMember = (GameObject)Instantiate(Resources.Load(InviteMemberPrefab));
                tempMember.transform.FindChild(MemberNameName).GetComponent<TextMesh>().text = tempInvite.Account;
                tempMember.transform.parent = gameObject.transform;
            }
        }
        if (RayHit.RightHitName.Equals(SearchIcon) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            tempInvite = null;
            //TODO 根据输入账号查找用户
            tempInvite = ProxyInterface.User_GetDetailByAccount(inputAccount);
            if (tempInvite != null)
            {
                GameObject tempMember = (GameObject)Instantiate(Resources.Load(InviteMemberPrefab));
                tempMember.transform.FindChild(MemberNameName).GetComponent<TextMesh>().text = tempInvite.Account;
                tempMember.transform.parent = gameObject.transform;
            }
        }
    }

    private void checkSearchHover()
    {
        if (RayHit.LeftHitName.Equals(SearchIcon) || RayHit.RightHitName.Equals(SearchIcon))
        {
            searchicon.transform.localScale = searchHoverScale;
            searchicon.transform.position = new Vector3(searchicon.transform.position.x,
                searchicon.transform.position.y, searchHoverZ);
        }
        else
        {
            searchicon.transform.localScale = searchOriginScale;
            searchicon.transform.position = new Vector3(searchicon.transform.position.x,
                searchicon.transform.position.y, searchOriginZ);
        }
    }

    private void checkCancelClick()
    {
        if (RayHit.LeftHitName.Equals(CancelButton) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            gameObject.SetActive(false);
            Keyboard.isOut = false;
            LeftHandProperty.clickUsed = true;
        }
        if (RayHit.RightHitName.Equals(CancelButton) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            gameObject.SetActive(false);
            Keyboard.isOut = false;
            RightHandProperty.clickUsed = true;
        }
    }

    private void checkConfirmClick()
    {
        if (RayHit.LeftHitName.Equals(ConfirmButton) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            LeftHandProperty.clickUsed = true;
            if (!ProxyInterface.Invitation_Send(WholeStatic.curUser.Guid, WholeStatic.curProject.Guid, tempInvite.Guid))
            {
                AttentionStatic.callAttention(ProjectionNPCName, "邀请失败！");
            }
            gameObject.SetActive(false);
            Keyboard.isOut = false;
        }
        if (RayHit.RightHitName.Equals(ConfirmButton) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            if (!ProxyInterface.Invitation_Send(WholeStatic.curUser.Guid, WholeStatic.curProject.Guid, tempInvite.Guid))
            {
                AttentionStatic.callAttention(ProjectionNPCName, "邀请失败！");
            }
            gameObject.SetActive(false);
            Keyboard.isOut = false;
        }
    }

    private void checkConfirmHover()
    {
        if (RayHit.LeftHitName.Equals(ConfirmButton) || RayHit.RightHitName.Equals(ConfirmButton))
        {
            confirmbutton.transform.localScale = confirmHoverScale;
            confirmbutton.transform.position = new Vector3(confirmbutton.transform.position.x,
                confirmbutton.transform.position.y, confirmHoverZ);
        }
        else
        {
            confirmbutton.transform.localScale = confirmOriginScale;
            confirmbutton.transform.position = new Vector3(confirmbutton.transform.position.x,
                confirmbutton.transform.position.y, confirmOriginZ);
        }
    }

    private void checkCancelHover()
    {
        if (RayHit.LeftHitName.Equals(CancelButton) || RayHit.RightHitName.Equals(CancelButton))
        {
            cancelbutton.transform.localScale = cancelHoverScale;
            cancelbutton.transform.position = new Vector3(cancelbutton.transform.position.x,
                cancelbutton.transform.position.y, cancelHoverZ);
        }
        else
        {
            cancelbutton.transform.localScale = cancelOriginScale;
            cancelbutton.transform.position = new Vector3(cancelbutton.transform.position.x,
                cancelbutton.transform.position.y, cancelOriginZ);
        }
    }
}
