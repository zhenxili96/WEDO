using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Wedo_ClientSide;
using System.Reflection;
using System;
using System.Threading;

public enum RoomMode { Mode1, Mode2};

public class RoomStatic : MonoBehaviour
{

    public static string curFocus = "";
    public static int LayerCount = 0;
    public static int curLayer = 0;
    public static List<Layer> layerArray = new List<Layer>();
    public static RoomMode curMode = RoomMode.Mode1;
    public static List<ClientMessage> notices = new List<ClientMessage>();
    public static List<ClientMessage> records = new List<ClientMessage>();
    public static List<ClientMenber> members = new List<ClientMenber>();
    public static int noticeCount = 0;
    public static int recordCount = 0;
    public static int memberCount = 0;
    public static string Message_MemberName = "message_member";
    public static string Message_NoticeName = "message_notice";
    public static string Message_RecordName = "message_record";
    public static bool isTransPage = false;
    public static float LAYERZMIN = 35;
    public static int CircleInstanceCount = 0;
    public static int RectangleInstanceCount = 0;
    public static int TriangleInstanceCount = 0;
    public static int RoundRectangleInstanceCount = 0;
    public static int TextInstanceCount = 0;
    public const int SHAPE_CIRCLE = 1;
    public const int SHAPE_RECTANGLE = 2;
    public const int SHAPE_ROUNDRECTANGLE = 3;
    public const int SHAPE_TRIANGLE = 4;
    public const int TEXT = 5;
    public Timer uploadTimer;
    public Timer downloadTimer;
    public static string UNSETGUID = "---";

    // Use this for initialization
    void Start()
    {
        clearData();
        layerArray.Add(new Layer());
        /******enter 的时候已经同步添加了一个图层 ********/
        //layerArray.Add(new Layer(35, 34, 0, true));
        LayRay.rayStyle = RayStyle.Ortho;
        Keyboard.init();
        LeftHandProperty.HandInit();
        RightHandProperty.HandInit();
        initInformation();
        initLayer();
        curLayer = 1;
        uploadTimer = new Timer(uploadData, null, 0, 300);
        downloadTimer = new Timer(downloadData, null, 0, 300);
    }

    private void uploadData(object data)
    {
        //一直同步手势
        WholeStatic.curRoomInterface.EditUser(
           LeftHandProperty.curPos.x, LeftHandProperty.curPos.y, LeftHandProperty.curPos.z,
           RightHandProperty.curPos.x, RightHandProperty.curPos.y, RightHandProperty.curPos.z,
           checkHandState());

        //同步当前focus物体动态
        GameObject focusObject = GameObject.Find(curFocus);
        if (focusObject != null)
        {
            if (focusObject.GetComponent<InstanceType>().MyGuid.Equals(UNSETGUID))
            {
                Debug.Log("error! un set guid object occur!" + focusObject.name);
                return;
            }
            if (focusObject.GetComponent<InstanceType>().Type == TEXT)
            {
                WholeStatic.curRoomInterface.EditBoardMaterial(
                    focusObject.GetComponent<InstanceType>().MyGuid, focusObject.GetComponent<InstanceType>().LayerGuid,
                    focusObject.transform.localPosition.x, focusObject.transform.localPosition.y, focusObject.transform.localPosition.z,
                    focusObject.transform.localScale.x, focusObject.transform.localScale.y, focusObject.transform.localScale.z,
                    focusObject.transform.localEulerAngles.x, focusObject.transform.localEulerAngles.y, focusObject.transform.localEulerAngles.z,
                    focusObject.GetComponent<InstanceType>().colorString, TEXT,
                    focusObject.transform.GetChild(0).GetComponent<TextMesh>().text,
                    focusObject.transform.GetChild(0).GetComponent<TextMesh>().fontSize, "");
            }
            else
            {
                WholeStatic.curRoomInterface.EditBoardMaterial(
                    focusObject.GetComponent<InstanceType>().MyGuid, focusObject.GetComponent<InstanceType>().LayerGuid,
                    focusObject.transform.localPosition.x, focusObject.transform.localPosition.y, focusObject.transform.localPosition.z,
                    focusObject.transform.localScale.x, focusObject.transform.localScale.y, focusObject.transform.localScale.z,
                    focusObject.transform.localEulerAngles.x, focusObject.transform.localEulerAngles.y, focusObject.transform.localEulerAngles.z,
                    focusObject.GetComponent<InstanceType>().colorString, focusObject.GetComponent<InstanceType>().Type,
                    "", 0, "");
            }
        }
    }

    private void downloadData(object data)
    {
        int[] isFindArray = new int[RoomStatic.layerArray.Count];
        for (int i = 0; i < WholeStatic.curRoomInterface.RoomLayers.Count; i++)
        {
            bool isFind = false;
            int j = 1;
            for (; j < RoomStatic.layerArray.Count; j++)
            {
                if (RoomStatic.layerArray[j].guid.Equals(WholeStatic.curRoomInterface.RoomLayers[i].NowLayer.Guid))
                {
                    isFind = true;
                    isFindArray[j] = 1;
                    RoomStatic.layerArray[j].refreshLayer(WholeStatic.curRoomInterface.RoomLayers[i]);
                    break;
                }
            }
            //服务器有新Layer 添加到场景中
            if (!isFind)
            {
                addLayer(WholeStatic.curRoomInterface.RoomLayers[i]);
            }
        }
        //服务器中不存在的Layer 删除
        for (int i = 0; i < isFindArray.Length; i++)
        {
            if (isFindArray[i] == 0)
            {
                Destroy(RoomStatic.layerArray[i].layerObject);
                RoomStatic.layerArray.RemoveAt(i);
                RoomStatic.LayerCount--;
            }
        }
    }

    private void clearData()
    {
        CircleInstanceCount = 0;
        RectangleInstanceCount = 0;
        TriangleInstanceCount = 0;
        RoundRectangleInstanceCount = 0;
        TextInstanceCount = 0;
    }

    private void initLayer()
    {
        for (int i = 0; i < WholeStatic.curRoomInterface.RoomLayers.Count; i++)
        {
            addLayer(WholeStatic.curRoomInterface.RoomLayers[i]);
        }
    }

    private void addLayer(RoomLayer tempLayer)
    {
        Layer newLayer = new Layer(LAYERZMIN - RoomStatic.LayerCount, tempLayer.NowLayer.Guid);
        foreach (ClientMaterial cm in tempLayer.BoardMaterials)
        {
            newLayer.AddInstance(cm);
        }
        LayerCount++;
        layerArray.Add(newLayer);
    }

    private void initInformation()
    {
        //notices = ProxyInterface.Project_GetInfo("f0d154e4-4b37-49f8-8526-288f64937a74").Announcements;
        //records = ProxyInterface.Project_GetInfo("f0d154e4-4b37-49f8-8526-288f64937a74").Records;
        //members = ProxyInterface.Project_GetInfo("f0d154e4-4b37-49f8-8526-288f64937a74").Menbers;
        notices = ProxyInterface.Project_GetInfo(WholeStatic.curProject.Guid).Announcements;
        records = ProxyInterface.Project_GetInfo(WholeStatic.curProject.Guid).Records;
        members = ProxyInterface.Project_GetInfo(WholeStatic.curProject.Guid).Menbers;
        noticeCount = notices.Count;
        recordCount = records.Count;
        memberCount = members.Count;
        GameObject.Find(Message_MemberName).GetComponent<message_member>().initMember();
        GameObject.Find(Message_NoticeName).GetComponent<message_notice>().initNotice();
        GameObject.Find(Message_RecordName).GetComponent<message_record>().initRecord();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(WholeStatic.curProject.Guid);
        //Debug.Log("noticeCount + " + noticeCount);
        //Debug.Log("recordCount + " + recordCount);
        //Debug.Log("memberCount + " + memberCount);

        
    }

    private int checkHandState()
    {
        int LeftHand = 0;
        if (LeftHandProperty.isClosed)
        {
            LeftHand = 1;
        }
        else
        {
            LeftHand = 0;
        }
        if (LeftHandProperty.isShow)
        {
            LeftHand += 10;
        }
        else
        {
            LeftHand += 00;
        }
        int RightHand = 0;
        if (RightHandProperty.isClosed)
        {
            RightHand = 1;
        }
        else
        {
            RightHand = 0;
        }
        if (RightHandProperty.isShow)
        {
            RightHand += 10;
        }
        else
        {
            RightHand += 00;
        }
        return (LeftHand * 100 + RightHand);
    }

    void OnDestroy()
    {
        if (!isTransPage)
        {
            Debug.Log("exit room");
            WholeStatic.curRoomInterface.ExitRoom();
            if (WholeStatic.curRoomInterface.RoomUsers.Count == 0)
            {
                WholeStatic.curRoomInterface.CloseRoom();
            }
            ProxyInterface.Connect_End();
        }
        else
        {
            isTransPage = false;
        }
    }
}
