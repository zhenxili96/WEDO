using UnityEngine;
using System.Collections;

public class InvitationStatic
{
    public static string InvitationName = "Invitation";
    public static string InviteTextName = "invitetext";
    public static string curGUID = "";

    public static void showInvitation(string parent, string content, string guid)
    {
        curGUID = guid;
        GameObject invitationObject = GameObject.Find(parent).transform.FindChild(InvitationName).gameObject;
        invitationObject.SetActive(true);
        invitationObject.transform.FindChild(InviteTextName).GetComponent<TextMesh>().text = content;
    }
}
