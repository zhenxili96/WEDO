using UnityEngine;
using System.Collections;

public class TextInstance : MonoBehaviour
{

    private bool isFocus = true;
    private bool isDraging = false;
    private static float warnHigh = 70f;
    private static float deleteHigh = 80f;
    private HAND dragHand;
    private int belongLayer;
    private float layerZ;

    // Use this for initialization
    void Start()
    {
        RoomKey.curSentence = GetComponent<TextMesh>().text;
        name = transform.parent.name + "_prefab";
        RoomStatic.curFocus = name;
        isFocus = true;
        ColorItem.curColor = renderer.material.color;
        gameObject.AddComponent<BoxCollider>();
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
        checkFocus();
        checkDrag();
        checkScale();
        checkCollider();
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


    private void checkScale()
    {
        GetComponent<TextMesh>().fontSize = (int)(100 * transform.localScale.x);
        transform.parent.localScale = new Vector3(1 / transform.localScale.x,
            1 / transform.localScale.y, 1 / transform.localScale.z);
    }

    private void checkCollider()
    {
        GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
        int charCount = GetComponent<TextMesh>().text.Length;
        int fontSize = GetComponent<TextMesh>().fontSize / 10;
        GetComponent<BoxCollider>().size = new Vector3(transform.localScale.x * charCount * fontSize, transform.localScale.y * 2 * fontSize, transform.localScale.z);
    }

    private void checkFocus()
    {
        if (RoomStatic.curFocus.Equals(name))
        {
            isFocus = true;
            renderer.material.color = ColorItem.curColor;
            GetComponent<TextMesh>().text = RoomKey.curSentence;
        }
        else
        {
            isFocus = false;
        }
    }

    private void checkDrag()
    {
        if (RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            isDraging = true;
            dragHand = HAND.LEFTHAND;
            LeftHandProperty.clickUsed = true;
            RoomStatic.curFocus = name;
        }
        if (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            isDraging = true;
            dragHand = HAND.RIGHTHAND;
            RightHandProperty.clickUsed = true;
            RoomStatic.curFocus = name;
        }
        if (isDraging)
        {
            switch (dragHand)
            {
                case HAND.LEFTHAND:
                    if (!LeftHandProperty.isClosed)
                    {
                        isDraging = false;
                    }
                    else
                    {
                        GameObject leftHand = GameObject.Find(LeftHandProperty.HANDNAME);
                        transform.position = new Vector3(leftHand.transform.position.x, leftHand.transform.position.y,
                            layerZ);
                    }
                    break;
                case HAND.RIGHTHAND:
                    if (!RightHandProperty.isClosed)
                    {
                        isDraging = false;
                    }
                    else
                    {
                        GameObject rightHand = GameObject.Find(RightHandProperty.HANDNAME);
                        transform.position = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y,
                            layerZ);
                    }
                    break;
            }
        }
    }
}
