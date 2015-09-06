using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Wedo_ClientSide;

public enum RoomMode { Mode1, Mode2};

public class RoomStatic : MonoBehaviour
{

    public static string curFocus = "";
    public static int LayerCount = 1;
    public static int curLayer = 1;
    public static ArrayList layerArray = new ArrayList();
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


    public static string WEDOGUID = "d7be4e74-ae0b-4b22-a68d-c29b978ccb48";
    public static string ADESIGNGUID = "f0d154e4-4b37-49f8-8526-288f64937a74";

    // Use this for initialization
    void Start()
    {
        layerArray.Add(new Layer());
        layerArray.Add(new Layer(35, 34, 0, true));
        LayRay.rayStyle = RayStyle.Ortho;
        Keyboard.init();
        LeftHandProperty.HandInit();
        RightHandProperty.HandInit();
        initInformation();
    }

    private void initInformation()
    {
        notices = ProxyInterface.Project_GetInfo("f0d154e4-4b37-49f8-8526-288f64937a74").Announcements;
        records = ProxyInterface.Project_GetInfo("f0d154e4-4b37-49f8-8526-288f64937a74").Records;
        members = ProxyInterface.Project_GetInfo("f0d154e4-4b37-49f8-8526-288f64937a74").Menbers;
        //notices = ProxyInterface.Project_GetInfo(WholeStatic.curProject.Guid).Announcements;
        //records = ProxyInterface.Project_GetInfo(WholeStatic.curProject.Guid).Records;
        //members = ProxyInterface.Project_GetInfo(WholeStatic.curProject.Guid).Menbers;
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
        Debug.Log("noticeCount + " + noticeCount);
        Debug.Log("recordCount + " + recordCount);
        Debug.Log("memberCount + " + memberCount);
    }
}

public class Layer
{
    public float ZMINPos = 25;
    public float ZMAXPos = 35;
    public int ObjectCount = -1;
    public bool isActive = true;
    public float ZSPACE = 0.01f;

    public Layer()
    {
        //空Layer
    }

    public Layer(float zmin, float zmax, int objectcount = 0, bool isactive = true)
    {
        ZMINPos = zmin;
        ZMAXPos = zmax;
        ObjectCount = objectcount;
        isActive = isactive;
    }
}
