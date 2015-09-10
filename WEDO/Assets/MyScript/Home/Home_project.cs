using UnityEngine;
using System.Collections;
using Wedo_ClientSide;

public class Home_project : MonoBehaviour
{

    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.1f;
    public float originZ;
    public float hoverZ;
    public ClientProject projectObject = null;
    public bool isPress = false;
    public HAND pressHand;
    public Vector3 pressPos;
    public float posChangeThreshold = 100;
    public bool isDrag = false;
    public HAND dragHand;
    public Vector3 dragBeginPos;
    public Vector3 barBeginPos;
    public string ProjBarName = "ProjBar";
    public bool isLHover = false;
    public bool isRHover = false;
    public bool prepareClick = false;
    public string DeleteName = "delete";
    public bool isDeleteHover = false;
    public GameObject DeleteObject;
    public Vector3 deleteOriginScale;
    public Vector3 deleteHoverScale;
    public float deleteScaleRate = 1.3f;

    // Use this for initialization
    void Start()
    {
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = transform.position.z;
        hoverZ = originZ - 1;
        foreach (Transform child in transform)
        {
            child.name = name + child.name;
        }
        DeleteName = name + DeleteName;
        DeleteObject = transform.FindChild(DeleteName).gameObject;
        deleteOriginScale = DeleteObject.transform.localScale;
        deleteHoverScale = deleteOriginScale * deleteScaleRate;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkClick();
        checkPress();
        checkDrag();
        checkDeleteHover();
        checkDeleteClick();
    }

    private void checkDeleteClick()
    {
        //if (RayHit.LeftHitName.Equals(DeleteName)
        //    && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        //{
        //    if (!ProxyInterface.Project_Delete())
        //}
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
                    float distance = ((handCurPos.x - pressPos.x) * (handCurPos.x - pressPos.x)
                        + (handCurPos.y - pressPos.y) * (handCurPos.y - pressPos.y));
                    if (distance < posChangeThreshold)
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
                        //Debug.Log("C");
                    }
                    break;
                case HAND.RIGHTHAND:
                    Vector3 handCurPos_ = GameObject.Find(RightHandProperty.HANDNAME).transform.position;
                    float distance_ = ((handCurPos_.x - pressPos.x) * (handCurPos_.x - pressPos.x)
                        + (handCurPos_.y - pressPos.y) * (handCurPos_.y - pressPos.y));
                    if (distance_ < posChangeThreshold)
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
                WholeStatic.curProject = projectObject;
                HomeStatic.isTransPage = true;
                Application.LoadLevel(Name.MAINPROJECTIONPAGENAME);
            }
        }
    }

    private void checkClick()
    {
        if (isLHover && LeftHandProperty.isClosed)
        {
            //Debug.Log("isclosed + " + LeftHandProperty.isClosed);
            if (!isPress)
            {
                pressHand = HAND.LEFTHAND;
                pressPos = GameObject.Find(LeftHandProperty.HANDNAME).transform.position;
            }
            isPress = true;
        } 
        else if (isRHover && RightHandProperty.isClosed)
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

    private void checkDeleteHover()
    {
        if (RayHit.LeftHitName.Equals(DeleteName) || RayHit.RightHitName.Equals(DeleteName))
        {
            isDeleteHover = true;
            DeleteObject.transform.localScale = deleteHoverScale;
        }
        else
        {
            isDeleteHover = false;
            DeleteObject.transform.localScale = originScale;
        }
    }

    private void checkHover()
    {
        isLHover = false;
        isRHover = false;
        if (RayHit.LeftHitName.Equals(name))
        {
            isLHover = true;
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (child.name.Equals(DeleteName))
                {
                    continue;
                }
                if (RayHit.LeftHitName.Equals(child.name))
                {
                    isLHover = true;
                }
            }
        }
        if (RayHit.RightHitName.Equals(name))
        {
            isRHover = true;
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (child.name.Equals(DeleteName))
                {
                    continue;
                }
                if (RayHit.RightHitName.Equals(child.name))
                {
                    isRHover = true;
                }
            }
        }
        if (isLHover || isRHover)
        {
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }
}
