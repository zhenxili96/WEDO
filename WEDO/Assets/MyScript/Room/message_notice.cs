using UnityEngine;
using System.Collections;

public class message_notice : MonoBehaviour
{

    public bool isHover = false;
    public bool isOpen = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.3f;
    public float originZ;
    public float hoverZ;
    public string normalImagePath = "RoomMessage/noticenormalprefab";
    public string pressImagePath = "RoomMessage/noticepressprefab";
    public GameObject noticeObject;
    public string noticeContentName = "notice_content";
    public string notice = "";

    // Use this for initialization
    void Start()
    {
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = transform.position.z;
        hoverZ = originZ - 1;
    }

    public void initNotice()
    {
        noticeObject = transform.GetChild(0).gameObject;
        for (int i = 0; i < RoomStatic.noticeCount; i++)
        {
            notice += (RoomStatic.notices[i].Cont + "\n");
        }
        noticeObject.transform.FindChild(noticeContentName).GetComponent<TextMesh>().text
            = notice;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkClick();
        checkOpen();
    }

    private void checkOpen()
    {
        if (MessageBar.curFocus.Equals(name) && MessageBar.isOpen)
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }
        if (isOpen)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void checkClick()
    {
        if (isHover)
        {
            if (LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                LeftHandProperty.clickUsed = true;
                MessageBar.curFocus = name;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                MessageBar.curFocus = name;
            }
        }
    }

    private void checkHover()
    {
        if (MessageBar.isOpen && (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name)))
        {
            isHover = true;
        }
        else
        {
            isHover = false;
        }
        if (isHover && !isOpen)
        {
            renderer.material = (Material)Resources.Load(pressImagePath);
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            renderer.material = (Material)Resources.Load(normalImagePath);
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }
}
