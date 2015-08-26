using UnityEngine;
using System.Collections;

public class TextInstance : MonoBehaviour
{

    private bool isFocus = true;
    private bool isDraging = false;
    private HAND dragHand;

    // Use this for initialization
    void Start()
    {
        RoomKey.curSentence = GetComponent<TextMesh>().text;
        name = transform.parent.name + "_prefab";
        RoomStatic.curFocus = name;
        isFocus = true;
        ColorItem.curColor = renderer.material.color;
        gameObject.AddComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        checkFocus();
        checkDrag();
        checkScale();
        checkCollider();
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
        GetComponent<BoxCollider>().size = new Vector3(transform.localScale.x * charCount, transform.localScale.y * 2, transform.localScale.z);
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
                            transform.position.z);
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
                            transform.position.z);
                    }
                    break;
            }
        }
    }
}
