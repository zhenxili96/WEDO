using UnityEngine;
using System.Collections;

public class ShapeInstance : MonoBehaviour
{
    public bool isFocus = false;
    private bool isHover = false;
    private Color originColor;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
        RoomStatic.curFocus = transform.parent.name;
        isFocus = true;
        ColorItem.curColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkDrag();
        checkFocus();
    }

    private void checkFocus()
    {
        if (RoomStatic.curFocus.Equals(transform.parent.name))
        {
            isFocus = true;
            renderer.material.color = ColorItem.curColor;
        } 
        else if ((RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed)
            || (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed))
        {
            isFocus = true;
            RoomStatic.curFocus = transform.parent.name;
            ColorItem.curColor = renderer.material.color;
        }
        else
        {
            isFocus = false;
        }
    }

    private void checkDrag()
    {
        HAND hand = HAND.LEFTHAND;
        bool isDrag = false;
        if (isFocus)
        {
            if (RayHit.LeftHitName.Equals(gameObject.name) && LeftHandProperty.isClosed)
            {
                isDrag = true;
                hand = HAND.LEFTHAND;
            }
            if (RayHit.RightHitName.Equals(gameObject.name) && RightHandProperty.isClosed)
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

    private void checkHover()
    {
        if (RayHit.LeftHitName.Equals(gameObject.name)
            || RayHit.RightHitName.Equals(gameObject.name))
        {
            isHover = true;
            renderer.material.color = Color.red;
        }
        else
        {
            isHover = false;
            renderer.material.color = originColor;
        }
    }
}
