using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Wedo_ClientSide;
using System.Reflection;
using System;
using System.Threading;

public enum RoomMode { Mode1, Mode2};
public class MyLayerInt
{
    public Layer key = null;
    public int value = 0;
}

public class MyLayerClientMaterial
{
    public Layer key = null;
    public ClientMaterial value = null;
}

public class MyLayerGameObject
{
    public Layer key = null;
    public GameObject value = null;
}

public class MyHandServerUser
{
    public UserHand key = null;
    public RoomUser value = null;
}

public class MyLayerRoomLayer
{
    public Layer key = null;
    public RoomLayer value = null;
}

public class RoomStatic : MonoBehaviour
{

    public static string curFocus = "";
    public static int curLayer = 0;
    public static List<Layer> layerArray = new List<Layer>();
    public static RoomMode curMode = RoomMode.Mode1;
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
    public static Queue<RoomLayer> UnAddLayer = new Queue<RoomLayer>();
    public static Queue<Layer> UnDeleteLayer = new Queue<Layer>();
    public static Queue<MyLayerInt> UnAddRawMaterial = new Queue<MyLayerInt>();
    public static Queue<MyLayerClientMaterial> UnAddServerMaterial = new Queue<MyLayerClientMaterial>();
    public static Queue<MyLayerGameObject> UnDeleteMaterial = new Queue<MyLayerGameObject>();
    public string OtherHandsName = "OtherHandsName";
    public List<UserHand> curUserHands = new List<UserHand>();  //本地users
    public static Queue<RoomUser> UnAddUser = new Queue<RoomUser>();
    public static Queue<UserHand> UnDeleteUser = new Queue<UserHand>();
    public static Queue<MyHandServerUser> UnRefreshUser = new Queue<MyHandServerUser>();
    public GameObject curFocusObject = null;
    public GameObject curFocusChild = null;
    public int curFocusRefresh = 0;
    public static Queue<MyLayerRoomLayer> UnRefreshLayer = new Queue<MyLayerRoomLayer>();

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
        //uploadTimer = new Timer(uploadData, null, 0, 300);
        downloadTimer = new Timer(downloadData, null, 0, 300);
    }

    private void uploadData(object data)
    {
        //一直同步上传手势
        int tempstate = checkHandState();
        if (WholeStatic.curRoomInterface == null)
        {
            Debug.Log("ERROR curRommInterface null");
        }
        WholeStatic.curRoomInterface.EditUser(LeftHandProperty.curPos.x, LeftHandProperty.curPos.y, LeftHandProperty.curPos.z,
            RightHandProperty.curPos.x, RightHandProperty.curPos.y, RightHandProperty.curPos.z, tempstate);
        //Debug.Log("upload MyLeftHand: " + LeftHandProperty.curPos);
        //Debug.Log("upload MyRightHand: " + RightHandProperty.curPos);

        //同步当前focus物体动态
        curFocusRefresh++;
    }

    private void refreshObject()
    {
        //同步当前focus物体动态
        if (curFocusRefresh > 0)
        {
            curFocusRefresh--;
        }
        else
        {
            return;
        }
        if (curFocusObject != null)
        {
            if (curFocusObject.GetComponent<InstanceType>().MyGuid.Equals(UNSETGUID))
            {
                Debug.Log("error! un set guid object occur!" + curFocusObject.name);
                return;
            }
            if (curFocusObject.GetComponent<InstanceType>().Type == TEXT)
            {
                string a = curFocusObject.GetComponent<InstanceType>().MyGuid;
                string b = curFocusObject.GetComponent<InstanceType>().LayerGuid;
                float c = curFocusObject.transform.localPosition.x;
                string d = curFocusObject.GetComponent<InstanceType>().colorString;
                string e = curFocusObject.transform.GetChild(0).GetComponent<TextMesh>().text;
                WholeStatic.curRoomInterface.EditBoardMaterial(
                    curFocusObject.GetComponent<InstanceType>().MyGuid, curFocusObject.GetComponent<InstanceType>().LayerGuid,
                    curFocusObject.transform.localPosition.x, curFocusObject.transform.localPosition.y, curFocusObject.transform.localPosition.z,
                    curFocusObject.transform.localScale.x, curFocusObject.transform.localScale.y, curFocusObject.transform.localScale.z,
                    curFocusObject.transform.localEulerAngles.x, curFocusObject.transform.localEulerAngles.y, curFocusObject.transform.localEulerAngles.z,
                    curFocusObject.GetComponent<InstanceType>().colorString, TEXT,
                    curFocusChild.GetComponent<TextMesh>().text,
                    curFocusChild.GetComponent<TextMesh>().fontSize, "");
            }
            else
            {
                WholeStatic.curRoomInterface.EditBoardMaterial(
                    curFocusObject.GetComponent<InstanceType>().MyGuid, curFocusObject.GetComponent<InstanceType>().LayerGuid,
                    curFocusObject.transform.localPosition.x, curFocusObject.transform.localPosition.y, curFocusObject.transform.localPosition.z,
                    curFocusObject.transform.localScale.x, curFocusObject.transform.localScale.y, curFocusObject.transform.localScale.z,
                    curFocusObject.transform.localEulerAngles.x, curFocusObject.transform.localEulerAngles.y, curFocusObject.transform.localEulerAngles.z,
                    curFocusObject.GetComponent<InstanceType>().colorString, curFocusObject.GetComponent<InstanceType>().Type,
                    "", 0, "");
            }
        }
    }

    private void downloadData(object data)
    {
        downloadHandData();
        downloadLayerData();
    }

    private void downloadHandData()
    {
        //server hands 信息同步处理
        int[] isFindArray = new int[curUserHands.Count];
        for (int i = 0; i < WholeStatic.curRoomInterface.RoomUsers.Count; i++)
        {
            if (WholeStatic.curRoomInterface.RoomUsers[i].UserGuid.Equals(
                WholeStatic.curUser.Guid))
            {
                continue;
            }
            bool isFind = false;
            for (int j = 0; j < curUserHands.Count; j++)
            {
                if (WholeStatic.curRoomInterface.RoomUsers[i].UserGuid.Equals(
                    curUserHands[j].userGuid))
                {
                    isFind = true;
                    isFindArray[j] = 1;
                    MyHandServerUser temp = new MyHandServerUser();
                    temp.key = curUserHands[i];
                    temp.value = WholeStatic.curRoomInterface.RoomUsers[i];
                    Debug.Log("refreshing...");
                    UnRefreshUser.Enqueue(temp);
                }
            }
            //server 有 新的手 加入本地
            if (!isFind)
            {
                Debug.Log("new user find" + WholeStatic.curRoomInterface.RoomUsers[i].UserAvatar);
                UnAddUser.Enqueue(WholeStatic.curRoomInterface.RoomUsers[i]);
            }
        }
        //服务器中不存在的手删除
        for (int i = 0; i < isFindArray.Length; i++)
        {
            if (isFindArray[i] == 0)
            {
                Debug.Log("old user delete" + curUserHands[i].userName);
                UnDeleteUser.Enqueue(curUserHands[i]);
            }
        }

    }

    private void downloadLayerData()
    {
        //server layer 信息同步处理
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
                    MyLayerRoomLayer temp = new MyLayerRoomLayer();
                    temp.key = RoomStatic.layerArray[j];
                    temp.value = WholeStatic.curRoomInterface.RoomLayers[i];
                    UnRefreshLayer.Enqueue(temp);
                    break;
                }
            }
            //服务器有新Layer 添加到场景中
            if (!isFind)
            {
                Debug.Log("将服务器中的新layer添加到场景中 " + WholeStatic.curRoomInterface.RoomLayers.Count
                    + " , " + layerArray.Count);
                //addLayer(WholeStatic.curRoomInterface.RoomLayers[i]);
                UnAddLayer.Enqueue(WholeStatic.curRoomInterface.RoomLayers[i]);
            }
        }
        //服务器中不存在的Layer 删除
        for (int i = 1; i < isFindArray.Length; i++)
        {
            if (isFindArray[i] == 0)
            {
                Debug.Log("删除服务器中不存在的layer" + WholeStatic.curRoomInterface.RoomLayers.Count
                    + " , " + layerArray.Count);
                //Destroy(RoomStatic.layerArray[i].layerObject);
                UnDeleteLayer.Enqueue(RoomStatic.layerArray[i]);
                //***RoomStatic.layerArray.RemoveAt(i);
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
        if (WholeStatic.curRoomInterface == null)
        {
            Debug.Log("ERROR curroominterface null");
            return;
        }
        if (WholeStatic.curRoomInterface.RoomLayers == null)
        {
            Debug.Log("ERROR curRoomInterface Roomlayers null");
            return;
        }
        Debug.Log("curRoomInterface Roomlayers count " + WholeStatic.curRoomInterface.RoomLayers.Count);
        for (int i = 0; i < WholeStatic.curRoomInterface.RoomLayers.Count; i++)
        {
            addLayer(WholeStatic.curRoomInterface.RoomLayers[i]);
        }
    }

    private void addLayer(RoomLayer tempLayer)
    {
        Layer newLayer = new Layer(LAYERZMIN - RoomStatic.layerArray.Count, tempLayer.NowLayer.Guid);
        foreach (ClientMaterial cm in tempLayer.BoardMaterials)
        {
            newLayer.AddInstance(cm);
        }
        layerArray.Add(newLayer);
    }

    private void initInformation()
    {
        //notices = ProxyInterface.Project_GetInfo("f0d154e4-4b37-49f8-8526-288f64937a74").Announcements;
        //records = ProxyInterface.Project_GetInfo("f0d154e4-4b37-49f8-8526-288f64937a74").Records;
        //members = ProxyInterface.Project_GetInfo("f0d154e4-4b37-49f8-8526-288f64937a74").Menbers;
        noticeCount = WholeStatic.curAnnouncements.Count;
        recordCount = WholeStatic.curRecords.Count;
        memberCount = WholeStatic.curMembers.Count;
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
        checkLayerChange();
        checkMaterialChange();
        checkUserHandChange();
        checkRefreshHand();
        checkRefreshLayer();
        curFocusObject = GameObject.Find(curFocus);
        if (curFocusObject != null)
        {
            curFocusChild = curFocusObject.transform.GetChild(0).gameObject;
        }
        refreshObject();
    }

    private void checkRefreshLayer()
    {
        while (UnRefreshLayer.Count != 0)
        {
            MyLayerRoomLayer temp = UnRefreshLayer.Dequeue();
            temp.key.refreshLayer(temp.value);
        }
    }

    private void checkRefreshHand()
    {
        while (UnRefreshUser.Count != 0)
        {
            MyHandServerUser temp = UnRefreshUser.Dequeue();
            temp.key.refreshHand(temp.value);
        }
    }

    private void checkUserHandChange()
    {
        while (UnAddUser.Count != 0)
        {
            UserHand temp = new UserHand();
            temp.initHand(UnAddUser.Dequeue());
            curUserHands.Add(temp);
        }
        while (UnDeleteUser.Count != 0)
        {
            curUserHands.Remove(UnDeleteUser.Dequeue());
        }
    }

    private void checkMaterialChange()
    {
        while (UnAddRawMaterial.Count != 0)
        {
            MyLayerInt temp = UnAddRawMaterial.Dequeue();
            temp.key.AddInstancePrivate(temp.value);
            Debug.Log("shape create  raw material");
        }
        while (UnAddServerMaterial.Count != 0)
        {
            MyLayerClientMaterial temp = UnAddServerMaterial.Dequeue();
            temp.key.AddInstancePrivate(temp.value);
            Debug.Log("shape create server material");
        }
        while (UnDeleteMaterial.Count != 0)
        {
            MyLayerGameObject temp = UnDeleteMaterial.Dequeue();
            MonoBehaviour.Destroy(temp.value);
            temp.key.instanceArray.Remove(temp.value);
            Debug.Log("shape delete in raw");
        }
    }

    private void checkLayerChange()
    {
        while (UnAddLayer.Count != 0)
        {
            Debug.Log("服务器layer添加到场景ing...");
            addLayer(UnAddLayer.Dequeue());
        }
        while (UnDeleteLayer.Count != 0)
        {
            Debug.Log("场景内无效layer删除ing...");
            Layer temp = UnDeleteLayer.Dequeue();
            Destroy(temp.layerObject);
            RoomStatic.layerArray.Remove(temp);
        }
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
        return (10000 + LeftHand * 100 + RightHand);
    }

    void OnDestroy()
    {
        if (!isTransPage)
        {
            Debug.Log("exit room");
            if (WholeStatic.curRoomInterface == null)
            {
                Debug.Log("ERROR room interface null & 退出房间失败");
            }
            else
            {
                if (!WholeStatic.curRoomInterface.ExitRoom())
                {
                    Debug.Log("ERROR 退出房间失败");
                }
                if (!WholeStatic.curRoomInterface.CloseRoom())
                {
                    Debug.Log("ERROR close room fail");
                }
                //if (WholeStatic.curRoomInterface.RoomUsers.Count == 0)
                //{
                //    if (!WholeStatic.curRoomInterface.CloseRoom())
                //    {
                //        Debug.Log("ERROR close room fail");
                //    }
                //    else
                //    {
                //        Debug.Log("room 无人， 关闭room");
                //    }
                //}
            }
            ProxyInterface.Connect_End();
        }
        else
        {
            isTransPage = false;
        }
    }
}
