using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Wedo_ClientSide;

public class EntryStatic : MonoBehaviour
{

    public string EntryNPCName = "Entry_NPC";
    public string InvitationName = "Invitation";
    public List<ClientNotification> curNotifications = new List<ClientNotification>();
    public static bool isTransPage = false;

    // Use this for initialization
    void Start()
    {
        LayRay.rayStyle = RayStyle.Ortho; 
        LeftHandProperty.HandInit();
        RightHandProperty.HandInit();
        checkInvitation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        if (!isTransPage)
        {
            Debug.Log("exit");
            ProxyInterface.Connect_End();
        }
        else
        {
            isTransPage = false;
        }
    }

    private void checkInvitation()
    {
        curNotifications = WholeStatic.curUser.Notifications;
        for (int i = 0; i < curNotifications.Count; i++)
        {
            Debug.Log(curNotifications[i].Message + "  " + curNotifications[i].GetType() + "  " + curNotifications[i].IsRead);
            if (curNotifications[i].IsRead.Equals(WholeStatic.ISREAD))
            {
                continue;
            }
            if (curNotifications[i].NotificationType.Equals(WholeStatic.INVITENOTIF))
            {
                InvitationStatic.showInvitation(EntryNPCName, formatMessage(curNotifications[i].Message), curNotifications[i].Guid);
            }
        }
    }

    private string formatMessage(string str)
    {
        string result = "";
        int tempCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            result += str[i];
            if (str[i] != '\n')
            {
                tempCount++;
            }
            if (tempCount % 15 == 0)
            {
                result += "\n";
            }
        }
        return result;
    }
}
