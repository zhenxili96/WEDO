using UnityEngine;
using System.Collections;
using System;

public abstract class BaseButton : MonoBehaviour
{

    protected bool isHover = false;
    private float timeThreshold = 3.0f;
    private bool drawing = false;
    private float tmpValue = 0.0f;
    private Rect bar = new Rect(0, 0, 100, 10);
    private DateTime startTime = new DateTime();
    private DateTime curTime = new DateTime();

    // Use this for initialization
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{
    //    DrawManage();
    //}

    protected void DrawManage()
    {
        if (!HandProperty.isClosed && !isHover && checkHover())
        {
            beginDraw();
        }

        if (!HandProperty.isClosed && isHover && !drawing)
        {
            beginDraw();
        }

        if (HandProperty.isClosed || (isHover && !checkHover()))
        {
            stopDraw();
        }

        if (!HandProperty.isClosed && isHover)
        {
            curTime = DateTime.Now;
            double d = curTime.Subtract(startTime).TotalMilliseconds;
            tmpValue = (float)d / 1000;
        }
    }

    void OnGUI()
    {
        if (drawing)
        {
            GUI.HorizontalScrollbar(bar, 0.0f, tmpValue, 0.0f, timeThreshold);
            if (timeThreshold - tmpValue <= 0.1f)
            {
                run();
                beginDraw();
            }
        }

    }

    public abstract void run();

    private void beginDraw()
    {
        drawing = true;
        tmpValue = 0.0f;
        startTime = DateTime.Now;
    }

    private void stopDraw()
    {
        drawing = false;
        tmpValue = 0.0f;
    }

    private bool checkHover()
    {
        if (RayHit.hitName.Equals(name))
        {
            isHover = true;
            return true;
        }
        else
        {
            isHover = false;
            return false;
        }
    }
}

