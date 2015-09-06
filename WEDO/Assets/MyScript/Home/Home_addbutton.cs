using UnityEngine;
using System.Collections;
using Wedo_ClientSide;

public class Home_addbutton : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.3f;
    public float originZ;
    public float hoverZ;
    public string HOMENPCNAME = "Home_NPC";
    public string AddprojectName = "Addproject";
    public bool isPress = false;
    public HAND pressHand;
    public Vector3 pressPos;
    public float posChangeThreshold = 100;
    public bool isDrag = false;
    public HAND dragHand;
    public Vector3 dragBeginPos;
    public Vector3 barBeginPos;
    public string ProjBarName = "ProjBar";

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
        checkPress();
        checkDrag();
        Debug.Log("ispress + " + isPress);
        Debug.Log("isdrag + " + isDrag);
    }

    private void checkDrag()
    {
        if (isDrag)
        {
            switch (dragHand)
            {
                case HAND.LEFTHAND:
                    Vector3 handMove = GameObject.Find(LeftHandProperty.HANDNAME).transform.position
                        - dragBeginPos;
                    GameObject.Find(ProjBarName).transform.position = new Vector3(barBeginPos.x + handMove.x,
                        barBeginPos.y, barBeginPos.z);
                    break;
                case HAND.RIGHTHAND:
                    Vector3 handMove_ = GameObject.Find(RightHandProperty.HANDNAME).transform.position
                        - dragBeginPos;
                    GameObject.Find(ProjBarName).transform.position = new Vector3(barBeginPos.x + handMove_.x,
                        barBeginPos.y, barBeginPos.z);
                    break;
            }
        }
    }

    private void checkPress()
    {
        if (isPress)
        {
            switch (pressHand)
            {
                case HAND.LEFTHAND:
                    Vector3 handCurPos = GameObject.Find(LeftHandProperty.HANDNAME).transform.position;
                    if (((handCurPos.x - pressPos.x) * (handCurPos.x - pressPos.x)
                        + (handCurPos.y - pressPos.y) * (handCurPos.y - pressPos.y))
                        < posChangeThreshold)
                    {
                        if (!LeftHandProperty.isClosed && !isDrag)
                        {
                            GameObject.Find(HOMENPCNAME).transform.FindChild(AddprojectName).gameObject.SetActive(true);
                            isPress = false;
                            Debug.Log("A");
                        }
                        Debug.Log("B");
                    }
                    else
                    {
                        if (LeftHandProperty.isClosed)
                        {
                            if (!isDrag)
                            {
                                dragBeginPos = GameObject.Find(LeftHandProperty.HANDNAME).transform.position;
                                barBeginPos = GameObject.Find(ProjBarName).transform.position;
                                dragHand = HAND.LEFTHAND;
                            }
                            isDrag = true;
                            Debug.Log("C");
                        }
                        else
                        {
                            isDrag = false;
                            isPress = false;
                            Debug.Log("D");
                        }
                    }
                    break;
                case HAND.RIGHTHAND:
                    Vector3 handCurPos_ = GameObject.Find(RightHandProperty.HANDNAME).transform.position;
                    if (((handCurPos_.x - pressPos.x) * (handCurPos_.x - pressPos.x)
                        + (handCurPos_.y - pressPos.y) * (handCurPos_.y - pressPos.y))
                        < posChangeThreshold)
                    {
                        if (!RightHandProperty.isClosed && !isDrag)
                        {
                            GameObject.Find(HOMENPCNAME).transform.FindChild(AddprojectName).gameObject.SetActive(true);
                            isPress = false;
                        }
                    }
                    else
                    {
                        if (RightHandProperty.isClosed)
                        {
                            if (!isDrag)
                            {
                                dragBeginPos = GameObject.Find(RightHandProperty.HANDNAME).transform.position;
                                barBeginPos = GameObject.Find(ProjBarName).transform.position;
                                dragHand = HAND.RIGHTHAND;
                            }
                            isDrag = true;
                        }
                        else
                        {
                            isDrag = false;
                            isPress = false;
                        }
                    }
                    break;
            }
        }
        else
        {
            isDrag = false;
        }
    }

    private void checkClick()
    {
        if (RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            Debug.Log("isclosed + " + LeftHandProperty.isClosed);
            if (!isPress)
            {
                pressHand = HAND.LEFTHAND;
                pressPos = GameObject.Find(LeftHandProperty.HANDNAME).transform.position;
            }
            isPress = true;
            LeftHandProperty.clickUsed = true;
        }
        if (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            Debug.Log("isclosed__ + " + RightHandProperty.isClosed);
            if (!isPress)
            {
                pressHand = HAND.RIGHTHAND;
                pressPos = GameObject.Find(RightHandProperty.HANDNAME).transform.position;
            }
            isPress = true;
            RightHandProperty.clickUsed = true;
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
