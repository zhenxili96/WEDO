using UnityEngine;
using System.Collections;

public class ColorBack : MonoBehaviour
{

    public bool isHover = false;
    public Color originColor;
    public Color hoverColor = new Color(1, 0.5412f, 0.5412f);
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.3f;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
        originScale = transform.localScale;
        hoverScale = originScale * scaleRate;
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
                ColorChoose.isOut = false;
            }
            if (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                ColorChoose.isOut = false;
            }
        }
    }

    private void checkHover()
    {
        if (ColorChoose.isOpen
            && (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name)))
        {
            isHover = true;
            renderer.material.color = hoverColor;
            transform.localScale = hoverScale;
        }
        else
        {
            isHover = false;
            renderer.material.color = originColor;
            transform.localScale = originScale;
        }
    }
}
