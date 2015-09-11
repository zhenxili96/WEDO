using UnityEngine;
using System.Collections;

public class ShapeButton : MonoBehaviour
{
    public static string MENUNAME = "menu";
    public static string MENUBARNAME = "MenuBar";
    public static string MENUSHAPENAME = "menu_shape";
    private bool isHover = false;
    private Vector3 originScale;
    private Vector3 hoverScale;
    private float scaleRate = 2;
    private float originZ;
    private float hoverZ;
    public Color hoverColor = new Color(1, 0.5412f, 0.5412f);
    public Color originColor;

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
            if (RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                GameObject.Find(MENUNAME).SetActive(false);
                LeftHandProperty.clickUsed = true;
                GameObject.Find(MENUBARNAME).transform.Find(MENUSHAPENAME).gameObject.SetActive(true);
            }
            if (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                GameObject.Find(MENUNAME).SetActive(false);
                RightHandProperty.clickUsed = true;
                GameObject.Find(MENUBARNAME).transform.Find(MENUSHAPENAME).gameObject.SetActive(true);
            }
        }
    }

    private void checkHover()
    {
        if (MenuBar.isOpen && RayHit.hitName.Equals(name))
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
