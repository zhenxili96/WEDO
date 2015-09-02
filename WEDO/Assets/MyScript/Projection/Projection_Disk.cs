using UnityEngine;
using System.Collections;

public class Projection_Disk : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 outPos = new Vector3(-17, 0, 10);
    public Vector3 inPos = new Vector3(-27, 0, 10);
    public float outSpeed = 0.12f;
    public float inSpeed = 0.15f;
    public bool isOpen = false;
    public bool isOut = false;
    public string PROJECTIONNPCNAME = "Projection_NPC";
    public string PROJECTIONDETAILNAME = "Projection_detail";

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkClick();
        checkOut();
    }

    private void checkOut()
    {
        if (isOpen)
        {
            GameObject.Find(PROJECTIONNPCNAME).transform.Find(PROJECTIONDETAILNAME).gameObject.SetActive(true);
            return;
        }
        else
        {
            GameObject.Find(PROJECTIONNPCNAME).transform.Find(PROJECTIONDETAILNAME).gameObject.SetActive(false);
        }
        if (isHover)
        {
            if (transform.position.x < outPos.x)
            {
                transform.position = new Vector3(transform.position.x + outSpeed, transform.position.y, transform.position.z);
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
                transform.position = new Vector3(transform.position.x - outSpeed, transform.position.y, transform.position.z);
            }
        }
    }

    private void checkClick()
    {
        if (isHover)
        {
            if (LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                LeftHandProperty.clickUsed = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
            }
        }
        else
        {
            if (LeftHandProperty.isClosed)
            {
                isOpen = false;
            }
            if (RightHandProperty.isClosed)
            {
                isOpen = false;
            }
        }
    }

    private void checkHover()
    {
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            isHover = true;
        }
        else
        {
            isHover = false;
        }
    }
}
