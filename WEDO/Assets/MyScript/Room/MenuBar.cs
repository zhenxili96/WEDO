using UnityEngine;
using System.Collections;

public class MenuBar : MonoBehaviour
{

    public bool statue = false;
    public bool isHover = false;
    public static bool isOpen = false;
    public bool isOut = false;
    public Vector3 outPos = new Vector3(-144f, 0, 40);
    public Vector3 inPos = new Vector3(-185, 0, 40);
    public float outSpeed = 40f;
    public float inSpeed = 50f;
    public string planeName = "menuplane";
    public Color originBarColor;
    public Color barNewColor = Color.red;


    // Use this for initialization
    void Start()
    {
        originBarColor = GameObject.Find(planeName).renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        checkOut();
        checkClick();
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
                    || (RayHit.RightHitName.Equals(child.name) && RightHandProperty.isClosed))
                {
                    isClick = true;
                }
                else
                {
                    foreach (Transform grandChild in child.transform)
                    {
                        if ((RayHit.LeftHitName.Equals(grandChild.name) && LeftHandProperty.isClosed)
                            || RayHit.RightHitName.Equals(grandChild.name) && RightHandProperty.isClosed)
                        {
                            isClick = true;
                        }
                    }
                }
            }
        }
        if (isClick)
        {
            isOut = true;
            if ((RayHit.LeftHitName.Equals(planeName) && LeftHandProperty.isClosed)
                            || RayHit.RightHitName.Equals(planeName) && RightHandProperty.isClosed)
            {
                if (isOpen)
                {
                    isOut = false;
                }
            }
        }
        else
        {
            //if (LeftHandProperty.isClosed || RightHandProperty.isClosed)
            //{
            //    isOut = false;
            //}
        }
    }

    private void checkOut()
    {
        isOpen = false;
        if (isOut)
        {
            if (transform.position.x < outPos.x)
            {
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * outSpeed);
            }
            else
            {
                isOpen = true;
            }
        }
        else
        {
            if (transform.position.x > inPos.x)
            {
                transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * inSpeed);
            }
        }
    }

    //private void checkStatue()
    //{
    //    if (isHover && !statue)
    //    {
    //        statue = true;
    //    }
    //    if (!isHover)
    //    {
    //        statue = false;
    //    }
    //}

    //private void checkHover()
    //{
    //    isHover = false;

    //    if (RayHit.LeftHitName.Equals(name) && !LeftHandProperty.isClosed
    //        || RayHit.RightHitName.Equals(name) && !RightHandProperty.isClosed)
    //    {
    //        isHover = true;
    //    }
    //    else
    //    {
    //        foreach (Transform child in transform)
    //        {
    //            if (RayHit.LeftHitName.Equals(child.name) && !LeftHandProperty.isClosed
    //                || RayHit.RightHitName.Equals(child.name) && !RightHandProperty.isClosed)
    //            {
    //                isHover = true;
    //            }
    //            else
    //            {
    //                foreach (Transform grandChild in child.transform)
    //                {
    //                    if (RayHit.LeftHitName.Equals(grandChild.name))
    //                    {
    //                        if (statue)
    //                        {
    //                            isHover = true;
    //                        }
    //                    } 
    //                    else if (RayHit.RightHitName.Equals(grandChild.name))
    //                    {
    //                        if (statue)
    //                        {
    //                            isHover = true;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

}
