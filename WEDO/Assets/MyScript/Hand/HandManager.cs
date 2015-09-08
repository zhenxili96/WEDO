using UnityEngine;
using System.Collections;

public class HandManager : MonoBehaviour
{

    public GameObject LeftHandObject = null;
    public GameObject RightHandObject = null;
    public string LeftHandName = "LeftHand";
    public string RightHandName = "RightHand";

    // Use this for initialization
    void Start()
    {
        LeftHandObject = transform.Find(LeftHandName).gameObject;
        RightHandObject = transform.Find(RightHandName).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showLeftHand()
    {
        RayHit.LeftHitName = "";
        LeftHandProperty.isShow = true;
        LeftHandObject.SetActive(true);
    }

    public void showRightHand()
    {
        RayHit.RightHitName = "";
        RightHandProperty.isShow = true;
        RightHandObject.SetActive(true);
    }

    public void hideLeftHand()
    {
        RayHit.LeftHitName = "";
        LeftHandProperty.isShow = false;
        LeftHandObject.SetActive(false);
    }

    public void hideRightHand()
    {
        RayHit.RightHitName = "";
        RightHandProperty.isShow = false;
        RightHandObject.SetActive(false);
    }
}
