using UnityEngine;
using System.Collections;

public class UpButton : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.5f;
    public float originZ;
    public float hoverZ;
    public string MenuBarName = "MenuBar";
    public string MenuName = "menu";
    public string MenuShapeName = "menu_shape";
    public Color originColor;
    public Color hoverColor = new Color(1, 0.5412f, 0.5412f);
    public string[] shapaNameArray = {"menu_line",
                                      "menu_circlebutton", "menu_roundrectanglebutton",
                                      "menu_rectanglebutton", "menu_trianglebutton",
                                      "menu_back"};
    public Vector3[] posArray = {new Vector3(0, 16, 0), new Vector3(0, 6, 0),
                                 new Vector3(0, -4, 0), new Vector3(0, -14, 0)};
    public static int upFlag = 2;
    public static int downFlag = 5;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
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
        checkShapePos();
    }

    private void checkShapePos()
    {
        GameObject menuShape = GameObject.Find(MenuBarName).transform.FindChild(MenuShapeName).gameObject;
        for (int i = 0; i < shapaNameArray.Length; i++)
        {
            if ((i < upFlag) || (i > downFlag))
            {
                menuShape.transform.FindChild(shapaNameArray[i]).gameObject.SetActive(false);
            }
            else
            {
                menuShape.transform.FindChild(shapaNameArray[i]).gameObject.SetActive(true);
                menuShape.transform.FindChild(shapaNameArray[i]).gameObject.transform.localPosition
                    = posArray[i - upFlag];
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
                if (GameObject.Find(MenuBarName).transform.FindChild(MenuName).gameObject.activeInHierarchy)
                {
                    //TODO 
                }
                if (GameObject.Find(MenuBarName).transform.FindChild(MenuShapeName).gameObject.activeInHierarchy)
                {
                    upFlag--;
                    if (upFlag < 0)
                    {
                        upFlag = 0;
                        return;
                    }
                    downFlag--;
                }
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                if (GameObject.Find(MenuBarName).transform.FindChild(MenuName).gameObject.activeInHierarchy)
                {
                    //TODO 
                }
                if (GameObject.Find(MenuShapeName).transform.FindChild(MenuShapeName).gameObject.activeInHierarchy)
                {
                    upFlag--;
                    if (upFlag < 0)
                    {
                        upFlag = 0;
                        return;
                    }
                    downFlag--;
                }
            }
        }
    }

    private void checkHover()
    {
        if (MenuBar.isOpen && (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name)))
        {
            isHover = true;
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
            renderer.material.color = hoverColor;
        }
        else
        {
            isHover = false;
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
            renderer.material.color = originColor;
        }
    }
}
