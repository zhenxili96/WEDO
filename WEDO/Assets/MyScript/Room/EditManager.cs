using UnityEngine;
using System.Collections;

public enum EditState { SCALE, SCALEX, SCALEY, ROTATE};

public class EditManager : MonoBehaviour
{
    public static EditState curEditState = EditState.SCALE;
    public string Manager_ScaleX = "manager_scalex";
    public string Manager_Scale = "manager_scale";
    public string Manager_ScaleY = "manager_scaley";
    public string Manager_Rotate = "manager_rotate";
    public GameObject scalexObject;
    public GameObject scaleyObject;
    public GameObject scaleObject;
    public GameObject rotateObject;
    public Vector3 scalexOriginScale;
    public Vector3 scaleyOriginScale;
    public Vector3 scaleOriginScale;
    public Vector3 rotateOriginScale;
    public Vector3 scalexHoverScale;
    public Vector3 scaleyHoverScale;
    public Vector3 scaleHoverScale;
    public Vector3 rotateHoverScale;
    public float scalexScaleRate = 1.2f;
    public float scaleyScaleRate = 1.2f;
    public float scaleScaleRate = 1.2f;
    public float rotateScaleRate = 1.2f;
    public Color scalexOriginColor;
    public Color scaleyOriginColor;
    public Color scaleOriginColor;
    public Color rotateOriginColor;
    public Color scalexHoverColor = new Color(0.7f, 0.7f, 0.7f);
    public Color scaleyHoverColor = new Color(0.7f, 0.7f, 0.7f);
    public Color scaleHoverColor = new Color(0.7f, 0.7f, 0.7f);
    public Color rotateHoverColor = new Color(0.7f, 0.7f, 0.7f);
    public Color scalexFocusColor = new Color(1, 0.5412f, 0.5412f);
    public Color scaleyFocusColor = new Color(1, 0.5412f, 0.5412f);
    public Color scaleFocusColor = new Color(1, 0.5412f, 0.5412f);
    public Color rotateFocusColor = new Color(1, 0.5412f, 0.5412f);
    public bool isInit = false;

    // Use this for initialization
    void Start()
    {

     }

    private void init()
    {
        isInit = true;
        scalexObject = transform.FindChild(Manager_ScaleX).gameObject;
        scaleyObject = transform.FindChild(Manager_ScaleY).gameObject;
        scaleObject = transform.FindChild(Manager_Scale).gameObject;
        rotateObject = transform.FindChild(Manager_Rotate).gameObject;

        scalexOriginColor = scalexObject.renderer.material.color;
        scalexOriginScale = scalexObject.transform.localScale;
        scaleyOriginColor = scaleyObject.renderer.material.color;
        scaleyOriginScale = scaleyObject.transform.localScale;
        scaleOriginColor = scaleObject.renderer.material.color;
        scaleOriginScale = scaleObject.transform.localScale;
        rotateOriginColor = rotateObject.renderer.material.color;
        rotateOriginScale = rotateObject.transform.localScale;

        scalexHoverScale = scalexOriginScale * scalexScaleRate;
        scaleyHoverScale = scaleyOriginScale * scaleyScaleRate;
        scaleHoverScale = scaleOriginScale * scaleScaleRate;
        rotateHoverScale = rotateOriginScale * rotateScaleRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        if (!isInit)
        {
            init();
        }

        checkScalexHover();
        checkScalezHover();
        checkScaleHover();
        checkRotateHover();
        checkScalexClick();
        checkScalezClick();
        checkScaleClick();
        checkRotateClick();
        checkColor();
    }

    private void checkColor()
    {
        switch (curEditState)
        {
            case EditState.SCALEX:
                scalexObject.renderer.material.color = scalexFocusColor;
                break;
            case EditState.SCALEY:
                scaleyObject.renderer.material.color = scaleyFocusColor;
                break;
            case EditState.SCALE:
                scaleObject.renderer.material.color = scaleFocusColor;
                break;
            case EditState.ROTATE:
                rotateObject.renderer.material.color = rotateFocusColor;
                break;
        }
    }

    private void checkScalexHover()
    {
        if (RayHit.LeftHitName.Equals(Manager_ScaleX)
            || RayHit.RightHitName.Equals(Manager_ScaleX))
        {
            if (curEditState == EditState.SCALEX)
            {
                return;
            }
            scalexObject.renderer.material.color = scalexHoverColor;
            scalexObject.transform.localScale = scalexHoverScale;
        }
        else
        {
            scalexObject.renderer.material.color = scalexOriginColor;
            scalexObject.transform.localScale = scalexOriginScale;
        }
    }

    private void checkScalexClick()
    {
        if (RayHit.LeftHitName.Equals(Manager_ScaleX) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            LeftHandProperty.clickUsed = true;
            curEditState = EditState.SCALEX;
            scalexObject.renderer.material.color = scalexFocusColor;
        }
        if (RayHit.RightHitName.Equals(Manager_ScaleX) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            curEditState = EditState.SCALEX;
            scalexObject.renderer.material.color = scalexFocusColor;
        }
    }

    private void checkScalezHover()
    {
        if (RayHit.LeftHitName.Equals(Manager_ScaleY)
            || RayHit.RightHitName.Equals(Manager_ScaleY))
        {
            if (curEditState == EditState.SCALEY)
            {
                return;
            }
            scaleyObject.renderer.material.color = scaleyHoverColor;
            scaleyObject.transform.localScale = scaleyHoverScale;
        }
        else
        {
            scaleyObject.renderer.material.color = scaleyOriginColor;
            scaleyObject.transform.localScale = scaleyOriginScale;
        }
    }

    private void checkScalezClick()
    {
        if (RayHit.LeftHitName.Equals(Manager_ScaleY) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            LeftHandProperty.clickUsed = true;
            curEditState = EditState.SCALEY;
            scaleyObject.renderer.material.color = scaleyFocusColor;
        }
        if (RayHit.RightHitName.Equals(Manager_ScaleY) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            curEditState = EditState.SCALEY;
            scaleyObject.renderer.material.color = scaleyFocusColor;
        }
    }

    private void checkScaleHover()
    {
        if (RayHit.LeftHitName.Equals(Manager_Scale)
            || RayHit.RightHitName.Equals(Manager_Scale))
        {
            if (curEditState == EditState.SCALE)
            {
                return;
            }
            scaleObject.renderer.material.color = scaleHoverColor;
            scaleObject.transform.localScale = scaleHoverScale;
        }
        else
        {
            scaleObject.renderer.material.color = scaleOriginColor;
            scaleObject.transform.localScale = scaleOriginScale;
        }
    }

    private void checkScaleClick()
    {
        if (RayHit.LeftHitName.Equals(Manager_Scale) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            LeftHandProperty.clickUsed = true;
            curEditState = EditState.SCALE;
            scaleObject.renderer.material.color = scaleFocusColor;
        }
        if (RayHit.RightHitName.Equals(Manager_Scale) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            curEditState = EditState.SCALE;
            scaleObject.renderer.material.color = scaleFocusColor;
        }
    }

    private void checkRotateHover()
    {
        if (RayHit.LeftHitName.Equals(Manager_Rotate)
            || RayHit.RightHitName.Equals(Manager_Rotate))
        {
            if (curEditState == EditState.ROTATE)
            {
                return;
            }
            rotateObject.renderer.material.color = rotateHoverColor;
            rotateObject.transform.localScale = rotateHoverScale;
        }
        else
        {
            rotateObject.renderer.material.color = rotateOriginColor;
            rotateObject.transform.localScale = rotateOriginScale;
        }
    }

    private void checkRotateClick()
    {
        if (RayHit.LeftHitName.Equals(Manager_Rotate) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            LeftHandProperty.clickUsed = true;
            curEditState = EditState.ROTATE;
            rotateObject.renderer.material.color = rotateFocusColor;
        }
        if (RayHit.RightHitName.Equals(Manager_Rotate) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            curEditState = EditState.ROTATE;
            rotateObject.renderer.material.color = rotateFocusColor;
        }
    }
}
