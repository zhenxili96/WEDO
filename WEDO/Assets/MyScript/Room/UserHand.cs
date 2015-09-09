using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wedo_ClientSide;

public class UserHand
{
    public GameObject leftHandObject;
    public GameObject rightHandObject;
    public string leftHandPrefab = "RoomPrefab/userlefthandprefab";
    public string rightHandPrefab = "RoomPrefab/userrighthandprefab";
    public string ParentName = "OtherHands";
    public string leftOpenHandMetrialPrefab = "YellowLeftHand";
    public string leftCloseHandMetrialPrefab = "YellowLeftHandFist";
    public string rightOpenHandMetrialPrefab = "YellowRightHand";
    public string rightCloseHandMetrialPrefab = "YellowRightHandFist";
    public string userName = "";
    public string userGuid = "";
    public string LeftName = "";
    public string RightName = "";

    public UserHand()
    {
    }

    public void initHand(RoomUser tempUser)
    {
        userName = tempUser.UserNickName;
        userGuid = tempUser.UserGuid;
        leftHandObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(leftHandPrefab));
        rightHandObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(rightHandPrefab));
        leftHandObject.transform.parent = GameObject.Find(ParentName).transform;
        leftHandObject.transform.localPosition = new Vector3(
            tempUser.LeftCoordX, tempUser.LeftCoordY, tempUser.LeftCoordZ);
        leftHandObject.transform.localScale = new Vector3(1, 1, 1);
        leftHandObject.transform.localEulerAngles = new Vector3(270, 0, 0);
        rightHandObject.transform.parent = GameObject.Find(ParentName).transform;
        rightHandObject.transform.localPosition = new Vector3(
            tempUser.RightCoordX, tempUser.RightCoordY, tempUser.RightCoordZ);
        rightHandObject.transform.localScale = new Vector3(1, 1, 1);
        rightHandObject.transform.localEulerAngles = new Vector3(270, 0, 0);
        LeftName = "LeftHand_" + userName;
        RightName = "RighHand_" + userName;
        leftHandObject.name = LeftName;
        rightHandObject.name = RightName;
        int tc = tempUser.Color - 10000;
        int LO = tc / 1000;
        int LS = (tc - LO * 1000) / 100;
        int RS = tc % 10;
        int RO = (tc % 100 - RS) / 10;
        if (LS == 1)
        {
            leftHandObject.renderer.material = (Material)MonoBehaviour.Instantiate(Resources.Load(leftOpenHandMetrialPrefab));
        }
        else
        {
            leftHandObject.renderer.material = (Material)MonoBehaviour.Instantiate(Resources.Load(leftCloseHandMetrialPrefab));
        }
        if (LO == 1)
        {
            GameObject.Find(ParentName).transform.FindChild(LeftName).gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find(ParentName).transform.FindChild(LeftName).gameObject.SetActive(false);
        }
        if (RS == 1)
        {
            rightHandObject.renderer.material = (Material)MonoBehaviour.Instantiate(Resources.Load(rightOpenHandMetrialPrefab));
        }
        else
        {
            rightHandObject.renderer.material = (Material)MonoBehaviour.Instantiate(Resources.Load(rightCloseHandMetrialPrefab));
        }
        if (RO == 1)
        {
            GameObject.Find(ParentName).transform.FindChild(RightName).gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find(ParentName).transform.FindChild(RightName).gameObject.SetActive(true);
        }
    }



    public void refreshHand(RoomUser tempUser)
    {
        int tc = tempUser.Color - 10000;
        int LO = tc / 1000;
        int LS = (tc - LO * 1000) / 100;
        int RS = tc % 10;
        int RO = (tc % 100 - RS) / 10;
        if (LS == 1)
        {
            leftHandObject.renderer.material = (Material)MonoBehaviour.Instantiate(Resources.Load(leftOpenHandMetrialPrefab));
        } 
        else
        {
            leftHandObject.renderer.material = (Material)MonoBehaviour.Instantiate(Resources.Load(leftCloseHandMetrialPrefab));
        }
        if (LO == 1)
        {
            GameObject.Find(ParentName).transform.FindChild(LeftName).gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find(ParentName).transform.FindChild(LeftName).gameObject.SetActive(false);
        }
        if (RS == 1)
        {
            rightHandObject.renderer.material = (Material)MonoBehaviour.Instantiate(Resources.Load(rightOpenHandMetrialPrefab));
        }
        else
        {
            rightHandObject.renderer.material = (Material)MonoBehaviour.Instantiate(Resources.Load(rightCloseHandMetrialPrefab));
        }
        if (RO == 1)
        {
            GameObject.Find(ParentName).transform.FindChild(RightName).gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find(ParentName).transform.FindChild(RightName).gameObject.SetActive(true);
        }
        leftHandObject.transform.localPosition = new Vector3(
            tempUser.LeftCoordX, tempUser.LeftCoordY, tempUser.LeftCoordZ);
        rightHandObject.transform.localPosition = new Vector3(
            tempUser.RightCoordX, tempUser.RightCoordY, tempUser.RightCoordZ);
        Debug.Log(tempUser.Color);
        Debug.Log("refresh " + LeftName + " " + leftHandObject.transform.localPosition);
        Debug.Log("refresh " + RightName + " " + rightHandObject.transform.localPosition);
    }
}