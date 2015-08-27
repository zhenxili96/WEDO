using UnityEngine;
using System.Collections;

public class ColorItem : MonoBehaviour
{

    private Vector3 originScale = new Vector3(0.165f, 1, 0.165f);
    private Vector3 newScale = new Vector3(0.25f, 1, 0.25f);
    private Vector3 originPos;
    private Vector3 newPos;
    private bool isHover = false;
    public static Color curColor;
    private string CURCOLORBOARDNAME = "CurColor";

    // Use this for initialization
    void Start()
    {
        originPos = transform.localPosition;
        newPos = originPos;
        newPos.z = -5;
        GameObject.Find(CURCOLORBOARDNAME).renderer.material.color = curColor;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkClick();
        GameObject.Find(CURCOLORBOARDNAME).renderer.material.color = curColor;
    }

    private void checkClick()
    {
        if (RayHit.LeftHitName.Equals(name)
            && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            LeftHandProperty.clickUsed = true;
            changeColor();
        }

        if (RayHit.RightHitName.Equals(name)
            && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            changeColor();
        }
    }

    private void changeColor()
    {
        int rowNum = name.ToCharArray()[0] - 'a';
        int colNum = name.ToCharArray()[1] - '1';
        curColor = ColorTable.Table[rowNum, colNum];
    }

    private void checkChoose()
    {
        if (ColorChoose.isOpen)
        {
            if (isHover)
            {
                transform.localPosition = newPos;
                transform.localScale = newScale;
            }
            else
            {
                transform.localPosition = originPos;
                transform.localScale = originScale;
            }
        }
    }

    private void checkHover()
    {
        if (ColorChoose.isOpen && (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name)))
        {
            isHover = true;
            transform.localPosition = newPos;
            transform.localScale = newScale;
        }
        else
        {
            isHover = false;
            transform.localPosition = originPos;
            transform.localScale = originScale;
        }
    }
}
