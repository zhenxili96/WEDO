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
        LeftHandObject.SetActive(true);
    }

    public void showRightHand()
    {
        RightHandObject.SetActive(true);
    }

    public void hideLeftHand()
    {
        LeftHandObject.SetActive(false);
    }

    public void hideRightHand()
    {
        RightHandObject.SetActive(false);
    }
}
