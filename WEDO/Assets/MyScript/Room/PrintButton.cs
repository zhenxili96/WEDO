﻿using UnityEngine;
using System.Collections;

public class PrintButton : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.3f;
    public float originZ;
    public float hoverZ;
    public string Camera1Name = "Camera_mode1";
    public string RoomNPCName = "Room_NPC";
    public string HANDNAME = "HAND";
    public string OtherHandName = "OtherHands";

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
        checkHover();
        checkClick();
    }

    private void checkClick()
    {
        if (isHover)
        {
            if (LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                LeftHandProperty.clickUsed = true;
                GameObject.Find(RoomNPCName).SetActive(false);
                GameObject.Find(HANDNAME).SetActive(false);
                GameObject.Find(OtherHandName).SetActive(false);
                GameObject.Find(Camera1Name).GetComponent<ScreenShot>().ScreenShotSave();
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                GameObject.Find(RoomNPCName).SetActive(false);
                GameObject.Find(HANDNAME).SetActive(false);
                GameObject.Find(OtherHandName).SetActive(false);
                GameObject.Find(Camera1Name).GetComponent<ScreenShot>().ScreenShotSave();
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
}
