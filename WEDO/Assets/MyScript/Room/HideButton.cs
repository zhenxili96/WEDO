using UnityEngine;
using System.Collections;

public class HideButton : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.3f;
    public float originZ;
    public float hoverZ;
    public Vector3 NormalRotate = new Vector3(90, 180, 0);
    public Vector3 HideRotate = new Vector3(-90, 0, 0);
    public bool isHide = false;
    public string HideBarName = "room_topbar";
    public string RoomNPCName = "Room_NPC";

    // Use this for initialization
    void Start()
    {
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = transform.position.z;
        hoverZ = originZ - 1;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkClick();
        checkHide();
    }

    private void checkHide()
    {
        if (isHide)
        {
            transform.localEulerAngles = HideRotate;
            GameObject.Find(RoomNPCName).transform.FindChild(HideBarName).gameObject.SetActive(false);
        }
        else
        {
            transform.localEulerAngles = NormalRotate;
            GameObject.Find(RoomNPCName).transform.FindChild(HideBarName).gameObject.SetActive(true);
        }
    }

    private void checkClick()
    {
        if (isHover)
        {
            if (LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                LeftHandProperty.clickUsed = true;
                isHide = !isHide;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                isHide = !isHide;
            }
        }
    }

    private void checkHover()
    {
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            isHover = true;
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            isHover = false;
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }
}
