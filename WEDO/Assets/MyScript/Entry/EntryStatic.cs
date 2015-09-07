using UnityEngine;
using System.Collections;
using Wedo_ClientSide;

public class EntryStatic : MonoBehaviour
{

    public string EntryNPCName = "Entry_NPC";
    public string InvitationName = "Invitation";

    // Use this for initialization
    void Start()
    {
        LayRay.rayStyle = RayStyle.Ortho; 
        LeftHandProperty.HandInit();
        RightHandProperty.HandInit();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void checkInvitation()
    {
        //TODO检查是否有未读申请

        //如果存在未读申请,【逐个】弹出
        InvitationStatic.showInvitation(EntryNPCName, "xxxx");

    }
}
