using UnityEngine;
using System.Collections;

public class ModeBackButton : MonoBehaviour
{

    private static string MODECHANGENAME = "ModeChange";
    private static string CURMODENAME = "Mode2";
    private static string ANOTHERMODENAME = "Mode1";
    private bool isHover = false;
    private Color originColor;
    private Color newColor = Color.red;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkClick();
    }

    private void checkClick()
    {
        if (RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed
            || RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            GameObject.Find(CURMODENAME).SetActive(false);
            GameObject.Find(MODECHANGENAME).transform.Find(ANOTHERMODENAME).gameObject.SetActive(true);
        }
    }

    private void checkHover()
    {
        isHover = false;

        if (RayHit.LeftHitName.Equals(name) && !LeftHandProperty.isClosed
            || RayHit.RightHitName.Equals(name) && !RightHandProperty.isClosed)
        {
            isHover = true;
            renderer.material.color = newColor;
        }
        else
        {
            isHover = false;
            renderer.material.color = originColor;
        }
    }
}

