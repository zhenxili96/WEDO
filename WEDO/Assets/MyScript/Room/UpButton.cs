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
                    GameObject menuShape = GameObject.Find(MenuBarName).transform.FindChild(MenuShapeName).gameObject;
                    for (int i = 0; i < menuShape.transform.GetChildCount(); i++)
                    {
                        menuShape.transform.GetChild(i).gameObject.SetActive(true);
                        //menuShape.transform.GetChild(i).gameObject.SendMessage("getUpOrder");
                    }
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
                    foreach (Transform child in GameObject.Find(MenuShapeName).transform.FindChild(MenuShapeName))
                    {
                        child.gameObject.SendMessage("getUpOrder");
                    }
                }
            }
        }
    }

    private void checkHover()
    {
        if (MenuBar.isOut && (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name)))
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
