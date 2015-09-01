using UnityEngine;
using System.Collections;
using System;

public class Login_Button : MonoBehaviour
{

    public bool isHover = false;
	private float timeThreshold = 3.0f;
    private bool drawing = false;
    private float tmpValue = 0.0f;
    private Rect bar = new Rect(0, 0, 100, 10);
    private DateTime startTime = new DateTime();
    private DateTime curTime = new DateTime();
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 2;
    public float originZ;
    public float hoverZ;

    // Use this for initialization
    void Start()
    {
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = transform.position.z;
        hoverZ = originZ - 1;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isHover && checkHover())
        //{
        //    beginDraw();
        //}

        //if (isHover && !checkHover())
        //{
        //    stopDraw();
        //}

        //if (isHover)
        //{
        //    curTime = DateTime.Now;
        //    double d = curTime.Subtract(startTime).TotalMilliseconds;
        //    tmpValue = (float)d / 1000;
        //}
        checkHover();
        checkClick();
    }

    private void checkClick()
    {
        if (isHover)
        {
            if (LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                Application.LoadLevel(Name.ENTRYPAGENAME);
                LeftHandProperty.clickUsed = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                Application.LoadLevel(Name.ENTRYPAGENAME);
                RightHandProperty.clickUsed = true;
            }
        }
    }

    private void checkHover()
    {
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            isHover = true;
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            isHover = false;
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }

    //void OnGUI()
    //{
    //    if (drawing)
    //    {
    //        GUI.HorizontalScrollbar(bar, 0.0f, tmpValue, 0.0f, timeThreshold);
    //        if (timeThreshold - tmpValue <= 0.1f)
    //        {
    //            run();
    //        }
    //    }
		
    //}

    //private void run()
    //{
    //    Application.LoadLevel(Name.ENTRYPAGENAME);
    //}

    //private void beginDraw()
    //{
    //    drawing = true;
    //    tmpValue = 0.0f;
    //    startTime = DateTime.Now;
    //}

    //private void stopDraw()
    //{
    //    drawing = false;
    //    tmpValue = 0.0f;
    //}

    //private bool checkHover()
    //{
    //    if (RayHit.hitName.Equals(name)){
    //        isHover = true;
    //        return true;
    //    }
    //    else
    //    {
    //        isHover = false;
    //        return false;
    //    }
    //}
}

