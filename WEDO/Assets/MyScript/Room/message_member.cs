using UnityEngine;
using System.Collections;

public class message_member : MonoBehaviour
{

    public bool isHover = false;
    public bool isOpen = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.3f;
    public float originZ;
    public float hoverZ;
    public string normalImagePath = "RoomMessage/membernormalprefab";
    public string pressImagePath = "RoomMessage/memberpressprefab";
    public GameObject memberObject;
    public string memberObjectPath = "RoomMessage/memberprefab";
    public Vector3 firstPos = new Vector3(-37, -19, 22);
    public Vector3 memberSpace = new Vector3(0, -3, 0);
    public Vector3 memberRotation = new Vector3(0, 0, 0);
    public Vector3 memberScale = new Vector3(0.18f, 0.18f, 1);

    // Use this for initialization
    void Start()
    {
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = transform.position.z;
        hoverZ = originZ - 1;
    }

    public void initMember()
    {
        memberObject = transform.GetChild(0).gameObject;
        for (int i = 0; i < RoomStatic.memberCount; i++)
        {
            GameObject tempObject = (GameObject)Instantiate(Resources.Load(memberObjectPath));
            tempObject.name = "member_" + RoomStatic.members[i].NowUser.Account;
            foreach(Transform child in tempObject.transform)
            {
                child.name = tempObject.name + "_" + child.name;
            }
            tempObject.transform.parent = memberObject.transform;
            tempObject.transform.FindChild(tempObject.name + "_name").GetComponent<TextMesh>().text
                = RoomStatic.members[i].NowUser.Account;
            tempObject.transform.localPosition = firstPos + i * memberSpace;
            tempObject.transform.eulerAngles = memberRotation;
            tempObject.transform.localScale = memberScale;
        }
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
