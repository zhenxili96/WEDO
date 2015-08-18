using UnityEngine;
using System.Collections;
using System;

public class AddButton : MonoBehaviour
{

    private bool isHover = false;
    private float timeThreshold = 2.0f;
    private bool drawing = false;
    private float tmpValue = 0.0f;
    private Rect bar = new Rect(0, 0, 100, 10);
    private DateTime startTime = new DateTime();
    private DateTime curTime = new DateTime();
    public static int projCount = 0;
    private Vector3 delta_Pos = new Vector3(7, 0, 0);
    private string PARENTNAME = "ProjBar";
    private Vector3 closePos = new Vector3();
    private bool closePosTemp = false;
    private float moveRate = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DrawManage();
        DragManage();
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

    private void DrawManage()
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

    private void DragManage()
    {
        if (!closePosTemp && HandProperty.isClosed)
        {
            closePos = GameObject.Find(PARENTNAME).transform.position;
            closePosTemp = true;
        }
        if (!HandProperty.isClosed)
        {
            closePosTemp = false;
        }
        if (HandProperty.isClosed && !HandProperty.closePosWait)
        {
            Vector3 curHandPos = GameObject.Find(HandProperty.HANDNAME).transform.position;
            GameObject.Find(PARENTNAME).transform.position = closePos
                + new Vector3((curHandPos.x - HandProperty.closePos.x)/moveRate, 0, 0);
        }
    }

    private void run()
    {
        projCount++;
        GameObject temp = (GameObject)Instantiate(Resources.Load("projection"));
        temp.name = "projection" + projCount;
        temp.transform.position = gameObject.transform.position + projCount * delta_Pos;
        temp.transform.parent = GameObject.Find(PARENTNAME).transform;
    }

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

