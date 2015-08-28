using UnityEngine;
using System.Collections;

public class ShapeInstance : MonoBehaviour
{
    public bool isFocus = false;
    private Color originColor;
    private static float warnHigh = 70f;
    private static float deleteHigh = 80f;
    private static string DELETEBUTTONNAME = "Room_delete";
    private int belongLayer;
    private float layerZ;

    // Use this for initialization
    void Start()
    {
        originColor = transform.GetChild(0).renderer.material.color;
        RoomStatic.curFocus = name;
        isFocus = true;
        ColorItem.curColor = transform.GetChild(0).renderer.material.color;
        belongLayer = RoomStatic.curLayer;
        ((Layer)RoomStatic.layerArray[belongLayer]).ObjectCount++;
        int objcount = ((Layer)RoomStatic.layerArray[belongLayer]).ObjectCount;
        layerZ = ((Layer)RoomStatic.layerArray[belongLayer]).ZMAXPos
            - objcount * ((Layer)RoomStatic.layerArray[belongLayer]).ZSPACE;
        transform.position = new Vector3(transform.position.x,
            transform.position.y, layerZ);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<ScaleAction>().enabled = true;
        GetComponent<RotationAction>().enabled = true;
        if (!((Layer)RoomStatic.layerArray[belongLayer]).isActive)
        {
            GetComponent<ScaleAction>().enabled = false;
            GetComponent<RotationAction>().enabled = false;
            return;
        }
        if (!isFocus)
        {
            GetComponent<ScaleAction>().enabled = false;
            GetComponent<RotationAction>().enabled = false;
        }
        checkDrag();
        checkFocus();
        checkDelete();
        //normalRotation();
    }

    private void normalRotation()
    {
        float zRotate = transform.eulerAngles.z;
        int zRate = (int)zRotate / 15;
        transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, zRate);
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
                        GameObject.Find(LeftHandProperty.HANDNAME).transform.position.y, layerZ);
                    break;
                case HAND.RIGHTHAND:
                    transform.position = new Vector3(GameObject.Find(RightHandProperty.HANDNAME).transform.position.x,
                        GameObject.Find(RightHandProperty.HANDNAME).transform.position.y, layerZ);
                    break;
                default:
                    break;
            }
        }
    }
}
