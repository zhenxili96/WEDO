using UnityEngine;
using System.Collections;

public class ShapeMenuBackButton : MonoBehaviour
{
    public static string MENUNAME = "menu";
    public static string MENUBARNAME = "MenuBar";
    public static string MENUSHAPENAME = "menu_shape";
    private bool isHover = false;
    public Color originColor;
    public Color hoverColor = new Color(1, 0.5412f, 0.5412f);
    private Vector3 originScale;
    private Vector3 hoverScale;
    private float scaleRate = 2;
    private float originZ;
    private float hoverZ;
    public Vector3 moveSpace = new Vector3(0, 10, 0);
    public Vector3 UpBorder = new Vector3(0, 16, 0);
    public Vector3 DownBorder = new Vector3(0, -14, 0);

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
        if (RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            LeftHandProperty.clickUsed = true;
            GameObject.Find(MENUSHAPENAME).SetActive(false);
            GameObject.Find(MENUBARNAME).transform.Find(MENUNAME).gameObject.SetActive(true);
        }
        if (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            GameObject.Find(MENUSHAPENAME).SetActive(false);
            GameObject.Find(MENUBARNAME).transform.Find(MENUNAME).gameObject.SetActive(true);
        }
    }

    private void checkHover()
    {
        if (MenuBar.isOut && (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name)))
        {
            isHover = true;
            renderer.material.color = hoverColor;
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            isHover = false;
            renderer.material.color = originColor;
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }

    public void getDownOrder()
    {
        transform.localPosition = transform.localPosition - moveSpace;
        checkShow();
    }

    public void getUpOrder()
    {
        transform.localPosition = transform.localPosition + moveSpace;
        checkShow();
    }

    private void checkShow()
    {
        if (transform.localPosition.y > UpBorder.y)
        {
            gameObject.SetActive(false);
        }
        else if (transform.localPosition.y < DownBorder.y)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
