using UnityEngine;
using System.Collections;

public class ShapeInstance : MonoBehaviour
{
    public bool isFocus = false;
    private Color originColor;
    private static float warnHigh = 70f;
    private static float deleteHigh = 80f;
    private static string DELETEBUTTONNAME = "Room_delete";

    // Use this for initialization
    void Start()
    {
        originColor = transform.GetChild(0).renderer.material.color;
        RoomStatic.curFocus = name;
        isFocus = true;
        ColorItem.curColor = transform.GetChild(0).renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        checkDrag();
        checkFocus();
        checkDelete();
    }

    private void checkDelete()
    {
        if (!isFocus)
        {
            return;
        }
        if (DeleteButton.isOpen && transform.position.y >= deleteHigh)
        {
            DeleteButton.isPrepare = false;
            if (RayHit.LeftHitName.Equals(transform.GetChild(0).name) && LeftHandProperty.isClosed)
            {
                DeleteButton.isPrepare = true;
            }
            else if (RayHit.RightHitName.Equals(transform.GetChild(0).name) && RightHandProperty.isClosed)
            {
                DeleteButton.isPrepare = true;
            }
            else
            {
                
                DeleteButton.isOut = false;
                Destroy(gameObject);
                return;
            }
        }
        if (transform.position.y >= warnHigh)
        {
            DeleteButton.isOut = true;
        }
        if (transform.position.y < warnHigh)
        {
            DeleteButton.isOut = false;
        }
    }

    private void checkFocus()
    {
        if (RoomStatic.curFocus.Equals(name))
        {
            isFocus = true;
            transform.GetChild(0).renderer.material.color = ColorItem.curColor;
        }
        else if ((RayHit.LeftHitName.Equals(transform.GetChild(0).name) && LeftHandProperty.isClosed)
            || (RayHit.RightHitName.Equals(transform.GetChild(0).name) && RightHandProperty.isClosed))
        {
            isFocus = true;
            RoomStatic.curFocus = name;
            ColorItem.curColor = transform.GetChild(0).renderer.material.color;
        }
        else
        {
            isFocus = false;
        }
    }

    private void checkDrag()
    {
        if (!isFocus)
        {
            return;
        }
        HAND hand = HAND.LEFTHAND;
        bool isDrag = false;
        if (isFocus)
        {
            if (RayHit.LeftHitName.Equals(transform.GetChild(0).name) && LeftHandProperty.isClosed)
            {
                isDrag = true;
                hand = HAND.LEFTHAND;
            }
            if (RayHit.RightHitName.Equals(transform.GetChild(0).name) && RightHandProperty.isClosed)
            {
                isDrag = true;
                hand = HAND.RIGHTHAND;
            }
        }
        if (isDrag)
        {
            switch (hand)
            {
                case HAND.LEFTHAND:
                    transform.position = new Vector3(GameObject.Find(LeftHandProperty.HANDNAME).transform.position.x,
                        GameObject.Find(LeftHandProperty.HANDNAME).transform.position.y, transform.position.z);
                    break;
                case HAND.RIGHTHAND:
                    transform.position = new Vector3(GameObject.Find(RightHandProperty.HANDNAME).transform.position.x,
                        GameObject.Find(RightHandProperty.HANDNAME).transform.position.y, transform.position.z);
                    break;
                default:
                    break;
            }
        }
    }
}
