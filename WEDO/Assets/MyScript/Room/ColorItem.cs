using UnityEngine;
using System.Collections;

public class ColorItem : MonoBehaviour
{


    private Vector3 originScale;
    private Vector3 hoverScale;
    private float scaleRate = 2;
    private float originZ;
    private float hoverZ;
    private bool isHover = false;
    public static Color curColor;
    public static string curColorString;
    private string CURCOLORBOARDNAME = "CurColor";

    // Use this for initialization
    void Start()
    {
        originZ = transform.position.z;
        hoverZ = originZ - 1;
        GameObject.Find(CURCOLORBOARDNAME).renderer.material.color = curColor;
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
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
        curColorString = name;
    }


    private void checkHover()
    {
        if (ColorChoose.isOpen && (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name)))
        {
            isHover = true;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
            transform.localScale = hoverScale;
        }
        else
        {
            isHover = false;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
            transform.localScale = originScale;
        }
    }
}
