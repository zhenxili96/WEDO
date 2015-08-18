using UnityEngine;
using System.Collections;
using System;

public class Disk : MonoBehaviour
{

    protected bool isHover = false;
    private float tmpValue = 0.0f;
    private Vector3 originPos = new Vector3();
    private Vector3 maxBomb = new Vector3(15, 0, 0);
    private static string HIDEITEMNAME = "HideItems";
    private static string MESSAGEITEMNAME = "Projection_message";
    private bool closePosTemp = false;
    private Vector3 closePos = new Vector3();
    private float moveRate = 1;

    // Use this for initialization
    void Start()
    {
        originPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        DragManage();
    }



    private void DragManage()
    {
        if (!closePosTemp && HandProperty.isClosed)
        {
            closePos = gameObject.transform.position;
            closePosTemp = true;
        }
        if (!HandProperty.isClosed)
        {
            closePosTemp = false;
        }
        if (HandProperty.isClosed && !HandProperty.closePosWait)
        {
            Vector3 curHandPos = GameObject.Find(HandProperty.HANDNAME).transform.position;
            gameObject.transform.position = checkPos(closePos
                + new Vector3((curHandPos.x - HandProperty.closePos.x) / moveRate, 0, 0));
            checkShow();
        }
    }

    private Vector3 checkPos(Vector3 newPos)
    {
        Vector3 result = new Vector3();
        if ((newPos.x <= (originPos.x + maxBomb.x)) && newPos.x >= originPos.x)
        {
            return newPos;
        }
        if (newPos.x >= originPos.x + maxBomb.x)
        {
            result = originPos + maxBomb;
        }
        if (newPos.x <= originPos.x)
        {
            result = originPos;
        }
        return result;
    }

    private bool checkShow()
    {
        float distance = maxBomb.x - (gameObject.transform.position.x - originPos.x);
        if (distance <= 0.5f && distance >= 0)
        {
            GameObject.Find(HIDEITEMNAME).transform.Find(MESSAGEITEMNAME).gameObject.SetActive(true);
            return true;
        }
        if (distance <=0 && distance >= -0.5f)
        {
            GameObject.Find(HIDEITEMNAME).transform.Find(MESSAGEITEMNAME).gameObject.SetActive(true);
            return true;
        }

        GameObject.Find(HIDEITEMNAME).transform.Find(MESSAGEITEMNAME).gameObject.SetActive(false);
        return false;
    }

    public void run()
    {
        GameObject.Find(HIDEITEMNAME).transform.Find(MESSAGEITEMNAME).gameObject.SetActive(true);
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

