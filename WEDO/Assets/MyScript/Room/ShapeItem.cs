using UnityEngine;
using System.Collections;

public class ShapeItem : MonoBehaviour
{

    private bool isActive = true;
    private bool isHover = false;
    private Color originColor;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        DragManage();
    }

    private void DragManage()
    {
        HAND hand = HAND.LEFTHAND;
        bool isDrag = false;
        if (isActive)
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
        Debug.Log(RayHit.LeftHitName + " + " + name);
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
