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
        LeftHandObject.SetActive(true);
    }

    public void showRightHand()
    {
        RayHit.RightHitName = "";
        RightHandObject.SetActive(true);
    }

    public void hideLeftHand()
    {
        RayHit.LeftHitName = "";
        LeftHandObject.SetActive(false);
    }

    public void hideRightHand()
    {
        RayHit.RightHitName = "";
        RightHandObject.SetActive(false);
    }
}
