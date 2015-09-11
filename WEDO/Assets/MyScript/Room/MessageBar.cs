using UnityEngine;
using System.Collections;

public class MessageBar : MonoBehaviour
{

    public bool isHover = false;
    public static bool isOut = false;
    public static bool isOpen = false;
    public Vector3 outPos = new Vector3(-144f, 0, 40);
    public Vector3 inPos = new Vector3(-190f, 0, 40);
    public float outSpeed = 40f;
    public float inSpeed = 50f;
    public string planeName = "messageplane";
    public Color originColor;
    public static string curFocus = "";


    // Use this for initialization
    void Start()
    {
        originColor = GameObject.Find(planeName).renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //checkHover();
        checkClick();
        checkOut();
    }

    private void checkClick()
    {
        bool isClick = false;
        if ((RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed)
            || (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed))
        {
            isClick = true;
        }
        else
        {
            foreach (Transform child in transform)
            {
                if ((RayHit.LeftHitName.Equals(child.name) && LeftHandProperty.isClosed) 
                    || RayHit.RightHitName.Equals(child.name) && RightHandProperty.isClosed)
                {
                    isClick = true;
                }
                else
                {
                    foreach (Transform grandchild in child.transform)
                    {
                        if ((RayHit.LeftHitName.Equals(grandchild.name) && LeftHandProperty.isClosed)
                            || (RayHit.RightHitName.Equals(grandchild.name) && RightHandProperty.isClosed))
                        {
                            isClick = true;
                        }
                        else
                        {
                            foreach (Transform grandgrandchild in grandchild.transform)
                            {
                                if ((RayHit.LeftHitName.Equals(grandgrandchild.name) && LeftHandProperty.isClosed)
                                    || RayHit.RightHitName.Equals(grandgrandchild.name) && RightHandProperty.isClosed)
                                {
                                    isClick = true;
                                }
                                else
                                {
                                    foreach (Transform grandgrandgrandchild in grandgrandchild.transform)
                                    {
                                        if ((RayHit.LeftHitName.Equals(grandgrandgrandchild.name) && LeftHandProperty.isClosed)
                                            || (RayHit.RightHitName.Equals(grandgrandgrandchild.name)) && RightHandProperty.isClosed)
                                        {
                                            isClick = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        if (isClick)
        {
            isOut = true;
        }
        else
        {
            if (LeftHandProperty.isClosed || RightHandProperty.isClosed)
            {
                isOut = false;
            }
        }
    }

    private void checkOut()
    {
        if (isOut)
        {
            if (transform.position.x > outPos.x)
            {
                transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * outSpeed);
                curFocus = "";
            }
            else
            {
                isOpen = true;
            }
        }
        else
        {
            if (transform.position.x < inPos.x)
            {
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * inSpeed);
                curFocus = "";
            }
        }
    }

    private void checkHover()
    {
        isHover = false;
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            isHover = true;
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (RayHit.LeftHitName.Equals(child.name) || RayHit.RightHitName.Equals(child.name))
                {
                    isHover = true;
                }
                else
                {
                    foreach (Transform grandchild in child.transform)
                    {
                        if (RayHit.LeftHitName.Equals(grandchild.name) || RayHit.RightHitName.Equals(grandchild.name))
                        {
                            isHover = true;
                        }
                        else
                        {
                            foreach (Transform grandgrandchild in grandchild.transform)
                            {
                                if (RayHit.LeftHitName.Equals(grandgrandchild.name) || RayHit.RightHitName.Equals(grandgrandchild.name))
                                {
                                    isHover = true;
                                }
                                else
                                {
                                    foreach (Transform grandgrandgrandchild in grandgrandchild.transform)
                                    {
                                        if (RayHit.LeftHitName.Equals(grandgrandgrandchild.name) || RayHit.RightHitName.Equals(grandgrandgrandchild.name))
                                        {
                                            isHover = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (isHover)
        {
            isOut = true;
            if (!isOpen)
            {
                //GameObject.Find(planeName).renderer.material.color = hoverColor;
            }
        }
        else
        {
            GameObject.Find(planeName).renderer.material.color = originColor;
            isOut = false;
        }
    }

}
