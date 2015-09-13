using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Wedo_ClientSide;
using System;
using System.Threading;

public class EntryStatic : MonoBehaviour
{

    public string EntryNPCName = "Entry_NPC";
    public string InvitationName = "Invitation";
    public List<ClientNotification> curNotifications = new List<ClientNotification>();
    public static bool isTransPage = false;
    public Timer invitationTimer;
    public bool needInvitate = false;

    // Use this for initialization
    void Start()
    {
        LayRay.rayStyle = RayStyle.Ortho; 
        LeftHandProperty.HandInit();
        RightHandProperty.HandInit();
        checkInvitation();
        invitationTimer = new Timer(checkInvitationTimer, null, 0, 1000);
    }

    // Update is called once per frame
    void Update()
    {
        //if (needInvitate)
        //{
        //    checkInvitation();
        //    needInvitate = false;
        //}
    }

    private void checkInvitationTimer(object data)
    {
        needInvitate = true;
    }

    void OnDestroy()
    {
        if (!isTransPage)
        {
            Debug.Log("connect end");
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
                InvitationStatic.showInvitation(EntryNPCName, formatInvitation(curNotifications[i]), curNotifications[i].Guid);
            }
        }
    }

    private string formatInvitation(ClientNotification cn)
    {
        string result = "";
        for (int i = 0; i < cn.Message.Length; i++)
        {
            result += cn.Message[i];
            if ((i + 1)%15 == 0)
            {
                result += "\n";
            }
        }
        return result;
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
