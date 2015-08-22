using UnityEngine;
using System.Collections;

public class ShapeButton : MonoBehaviour
{
    public static string MENUNAME = "menu";
    public static string MENUBARNAME = "MenuBar";
    public static string MENUSHAPENAME = "menu_shape";
    private bool isHover = false;
    private Color originColor;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        if (isHover)
        {
            if (RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                GameObject.Find(MENUNAME).SetActive(false);
                LeftHandProperty.clickUsed = true;
                Debug.Log("click used");
                GameObject.Find(MENUBARNAME).transform.Find(MENUSHAPENAME).gameObject.SetActive(true);
            }
            if (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                GameObject.Find(MENUNAME).SetActive(false);
                RightHandProperty.clickUsed = true;
                Debug.Log("click used");
                GameObject.Find(MENUBARNAME).transform.Find(MENUSHAPENAME).gameObject.SetActive(true);
            }
        }
    }

    private void checkHover()
    {
        if (MenuBar.isOut && RayHit.hitName.Equals(name))
        {
            isHover = true;
            renderer.material.color = Color.red;
        }
        else
        {
            isHover = false;
            renderer.material.color = originColor;
        }
    }
}
