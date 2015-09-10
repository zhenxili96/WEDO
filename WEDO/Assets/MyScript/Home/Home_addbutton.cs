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
    public bool prepareClick = false;

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
        checkPress();
        checkClick();
        checkDrag();
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

    private void checkClick()
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
                        prepareClick = true;
                    }
                    else
                    {
                        prepareClick = false;
                        if (!isDrag)
                        {
                            dragBeginPos = GameObject.Find(LeftHandProperty.HANDNAME).transform.position;
                            barBeginPos = GameObject.Find(ProjBarName).transform.position;
                            dragHand = HAND.LEFTHAND;
                        }
                        isDrag = true;
                    }
                    break;
                case HAND.RIGHTHAND:
                    Vector3 handCurPos_ = GameObject.Find(RightHandProperty.HANDNAME).transform.position;
                    if (((handCurPos_.x - pressPos.x) * (handCurPos_.x - pressPos.x)
                        + (handCurPos_.y - pressPos.y) * (handCurPos_.y - pressPos.y))
                        < posChangeThreshold)
                    {
                        prepareClick = true;
                    }
                    else
                    {
                        prepareClick = false;
                        if (!isDrag)
                        {
                            dragBeginPos = GameObject.Find(RightHandProperty.HANDNAME).transform.position;
                            barBeginPos = GameObject.Find(ProjBarName).transform.position;
                            dragHand = HAND.RIGHTHAND;
                        }
                        isDrag = true;
                    }
                    break;
            }
        }
        else
        {
            isDrag = false;
            if (prepareClick)
            {
                prepareClick = false;
                GameObject.Find(HOMENPCNAME).transform.FindChild(AddprojectName).gameObject.SetActive(true);
                Keyboard.isOut = true;
            }
        }
    }

    private void checkPress()
    {
        if (RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed)
        {
            //Debug.Log("isclosed + " + LeftHandProperty.isClosed);
            if (!isPress)
            {
                pressHand = HAND.LEFTHAND;
                pressPos = GameObject.Find(LeftHandProperty.HANDNAME).transform.position;
            }
            isPress = true;
        }
        else if (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed)
        {
            //Debug.Log("isclosed__ + " + RightHandProperty.isClosed);
            if (!isPress)
            {
                pressHand = HAND.RIGHTHAND;
                pressPos = GameObject.Find(RightHandProperty.HANDNAME).transform.position;
            }
            isPress = true;
        }
        else
        {
            isPress = false;
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
