using UnityEngine;
using System.Collections;

public class ShapeInstance : MonoBehaviour
{
    public bool isFocus = false;
    public Color originColor;
    public static float warnHigh = 60f;
    public static float deleteHigh = 70f;
    //private static string DELETEBUTTONNAME = "Room_delete"; 
    public int belongLayer;
    public float layerZ;
    public Layer parentLayer = null;
    public bool isDelete = false;
    public string ShadowName = "shadow";
    public bool canTracking = false;

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
        if (isDelete)
        {
            return;
        }
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
        checkEditMode();
        //normalRotation();
    }

    private void checkEditMode()
    {
        switch (EditManager.curEditState)
        {
            case EditState.SCALE:
                GetComponent<ScaleAction>().enabled = true;
                GetComponent<ScaleAction>().Constraints.Freeze.X = false;
                GetComponent<ScaleAction>().Constraints.Freeze.Y = false;
                GetComponent<ScaleAction>().Constraints.Freeze.Z = true;
                GetComponent<RotationAction>().enabled = false;
                canTracking = false;
                break;
            case EditState.SCALEX:
                GetComponent<ScaleAction>().enabled = true;
                GetComponent<ScaleAction>().Constraints.Freeze.X = false;
                GetComponent<ScaleAction>().Constraints.Freeze.Y = true;
                GetComponent<ScaleAction>().Constraints.Freeze.Z = true;
                GetComponent<RotationAction>().enabled = false;
                canTracking = false;
                break;
            case EditState.SCALEY:
                GetComponent<ScaleAction>().enabled = true;
                GetComponent<ScaleAction>().Constraints.Freeze.X = true;
                GetComponent<ScaleAction>().Constraints.Freeze.Y = false;
                GetComponent<ScaleAction>().Constraints.Freeze.Z = true;
                GetComponent<RotationAction>().enabled = false;
                canTracking = false;
                break;
            case EditState.ROTATE:
                GetComponent<ScaleAction>().enabled = false;
                GetComponent<RotationAction>().enabled = true;
                canTracking = false;
                break;
            case EditState.MOVE:
                GetComponent<ScaleAction>().enabled = false;
                GetComponent<RotationAction>().enabled = false;
                canTracking = true;
                break;
        } 
    }

    private void normalRotation()
    {
        float zRotate = transform.localEulerAngles.z;
        int zRate = (int)zRotate / 15;
        transform.localEulerAngles = new Vector3(transform.localRotation.x, transform.localRotation.y, zRate);
    }

    private void checkDelete()
    {
        if (isDelete)
        {
            return;
        }
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
                //Destroy(gameObject);
                if (!gameObject.GetComponent<InstanceType>().LayerGuid.Equals(RoomStatic.UNSETGUID)
                    && !gameObject.GetComponent<InstanceType>().MyGuid.Equals(RoomStatic.UNSETGUID))
                {
                    WholeStatic.curRoomInterface.DeleteBoardMaterial(gameObject.GetComponent<InstanceType>().LayerGuid,
                    gameObject.GetComponent<InstanceType>().MyGuid);
                    isDelete = true;
                    Debug.Log("delete material");
                    return;
                }
                else
                {
                    Debug.Log("delete error " + gameObject.GetComponent<InstanceType>().LayerGuid
                        + "  " + gameObject.GetComponent<InstanceType>().MyGuid);
                }
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
        if (isDelete)
        {
            return;
        }
        if (RoomStatic.curFocus.Equals(name))
        {
            isFocus = true;
            //transform.GetChild(0).renderer.material.color = ColorItem.curColor;
            //GetComponent<InstanceType>().colorString = ColorItem.curColorString;
            WholeStatic.curRoomInterface.EditBoardMaterial(GetComponent<InstanceType>().MyGuid,
                GetComponent<InstanceType>().LayerGuid,
                transform.localPosition.x, transform.localPosition.y, transform.localPosition.z,
                transform.localScale.x, transform.localScale.y, transform.localScale.z,
                transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z,
                ColorItem.curColorString, GetComponent<InstanceType>().Type,
                    "", 0, "");
            transform.FindChild(ShadowName).gameObject.SetActive(true);
        }
        else if ((RayHit.LeftHitName.Equals(transform.GetChild(0).name) && LeftHandProperty.isClosed)
            || (RayHit.RightHitName.Equals(transform.GetChild(0).name) && RightHandProperty.isClosed))
        {
            isFocus = true;
            RoomStatic.curFocus = name;
            //ColorItem.curColor = transform.GetChild(0).renderer.material.color;
            //ColorItem.curColorString = GetComponent<InstanceType>().colorString;
            WholeStatic.curRoomInterface.EditBoardMaterial(GetComponent<InstanceType>().MyGuid,
                GetComponent<InstanceType>().LayerGuid,
                transform.localPosition.x, transform.localPosition.y, transform.localPosition.z,
                transform.localScale.x, transform.localScale.y, transform.localScale.z,
                transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z,
                ColorItem.curColorString, GetComponent<InstanceType>().Type,
                    "", 0, "");
            transform.FindChild(ShadowName).gameObject.SetActive(true);
        }
        else
        {
            isFocus = false;
            transform.FindChild(ShadowName).gameObject.SetActive(false);
        }
    }

    private void checkDrag()
    {
        if (isDelete)
        {
            return;
        }
        if (!canTracking)
        {
            return;
        }
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
