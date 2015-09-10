using UnityEngine;
using System.Collections;

public class ColorButton : MonoBehaviour
{

    private bool isHover = false;
    private Color originColor;
    private Vector3 originScale;
    private Vector3 hoverScale;
    private float scaleRate = 2;
    private float originZ;
    private float hoverZ;
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
            if (RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                LeftHandProperty.clickUsed = true;
                GameObject.Find("Color_choose").SendMessage("open");
            }
            if (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                GameObject.Find("Color_choose").SendMessage("open");
            }
        }
    }

    private void checkHover()
    {
        if (MenuBar.isOut && RayHit.hitName.Equals(name))
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
}
