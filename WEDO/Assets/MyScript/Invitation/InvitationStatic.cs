using UnityEngine;
using System.Collections;

public class InvitationStatic
{
    public static string InvitationName = "Invitation";
    public static string InviteTextName = "invitetext";

    public static void showInvitation(string parent, string content)
    {
        GameObject invitationObject = GameObject.Find(parent).transform.FindChild(InvitationName).gameObject;
        invitationObject.SetActive(true);
        invitationObject.transform.FindChild(InviteTextName).GetComponent<TextMesh>().text = content;
    }
}
