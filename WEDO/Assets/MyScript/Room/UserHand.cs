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

    public UserHand(RoomUser tempUser)
    {
        userName = tempUser.UserAvatar;
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
        leftHandObject.name = "LeftHand_" + userName;
        rightHandObject.name = "RighHand_" + userName;
        int LO = tempUser.Color / 1000;
        int LS = (tempUser.Color - LO * 1000) / 100;
        int RS = tempUser.Color % 10;
        int RO = (tempUser.Color % 100 - RS) / 10;
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
            leftHandObject.SetActive(true);
        }
        else
        {
            leftHandObject.SetActive(false);
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
            rightHandObject.SetActive(true);
        }
        else
        {
            rightHandObject.SetActive(false);
        }
    }

    public void refreshHand(RoomUser tempUser)
    {
        leftHandObject.transform.localPosition = new Vector3(
            tempUser.LeftCoordX, tempUser.LeftCoordY, tempUser.LeftCoordZ);
        rightHandObject.transform.localPosition = new Vector3(
            tempUser.RightCoordX, tempUser.RightCoordY, tempUser.RightCoordZ);
        int LO = tempUser.Color / 1000;
        int LS = (tempUser.Color - LO * 1000) / 100;
        int RS = tempUser.Color % 10;
        int RO = (tempUser.Color % 100 - RS) / 10;
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
            leftHandObject.SetActive(true);
        }
        else
        {
            leftHandObject.SetActive(false);
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
            rightHandObject.SetActive(true);
        }
        else
        {
            rightHandObject.SetActive(false);
        }
    }
}