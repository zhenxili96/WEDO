using UnityEngine;
using System.Collections;

public class ShapeInstance : MonoBehaviour
{
    public bool isFocus = false;
    public Color originColor;
    public static float warnHigh = 70f;
    public static float deleteHigh = 80f;
    //private static string DELETEBUTTONNAME = "Room_delete"; 
    public int belongLayer;
    public float layerZ;

    // Use this for initialization
    void Start()
    {
        originColor = transform.GetChild(0).renderer.material.color;
        RoomStatic.curFocus = "";
        isFocus = true;
        ColorItem.curColor = transform.GetChild(0).renderer.material.color;
        ColorItem.curColorString = GetComponent<InstanceType>().colorString;
        belongLayer = RoomStatic.curLayer;
        ((Layer)RoomStatic.layerArray[belongLayer]).ObjectCount++;
        int objcount = ((Layer)RoomStatic.layerArray[belongLayer]).ObjectCount;
        layerZ = ((Layer)RoomStatic.layerArray[belongLayer]).ZMINPos
            - objcount * ((Layer)RoomStatic.layerArray[belongLayer]).ZSPACE;
        transform.localPosition = new Vector3(transform.localPosition.x,
            transform.localPosition.y, layerZ);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<ScaleAction>().enabled = true;
        GetComponent<RotationAction>().enabled = true;
        if (RoomStatic.curLayer != belongLayer)
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
        float zRotate = transform.localEulerAngles.z;
        int zRate = (int)zRotate / 15;
        transform.localEulerAngles = new Vector3(transform.localRotation.x, transform.localRotation.y, zRate);
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
            GetComponent<InstanceType>().colorString = ColorItem.curColorString;
        }
        else if ((RayHit.LeftHitName.Equals(transform.GetChild(0).name) && LeftHandProperty.isClosed)
            || (RayHit.RightHitName.Equals(transform.GetChild(0).name) && RightHandProperty.isClosed))
        {
            isFocus = true;
            RoomStatic.curFocus = name;
            ColorItem.curColor = transform.GetChild(0).renderer.material.color;
            ColorItem.curColorString = GetComponent<InstanceType>().colorString;
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
