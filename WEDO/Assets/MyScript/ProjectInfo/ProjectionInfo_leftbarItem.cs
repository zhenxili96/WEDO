using UnityEngine;
using System.Collections;

public class ProjectionInfo_leftbarItem : MonoBehaviour
{

    public bool isHover = false;
    public Color originColor;
    public Color hoverColor = Color.red;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.1f;
    public float originZ;
    public float hoverZ;
    public string barName = "member_topbar";
    public GameObject barObject;
    public bool isOpen = false;

    // Use this for initialization
    void Start()
    {
        barObject = transform.Find(barName).gameObject;
        originColor = barObject.renderer.material.color;
        originScale = barObject.transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = barObject.transform.position.z;
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
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
                isOpen = true;
                LeftHandProperty.clickUsed = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
                isOpen = true;
                RightHandProperty.clickUsed = true;
            }
        }
        else
        {
            if (LeftHandProperty.isClosed)
            {
                foreach (Transform child in transform)
                {
                    if (child.name.Equals(barName))
                    {
                        continue;
                    }
                    child.gameObject.SetActive(false);
                }
                isOpen = false;
            }
            if (RightHandProperty.isClosed)
            {
                foreach (Transform child in transform)
                {
                    if (child.name.Equals(barName))
                    {
                        continue;
                    }
                    child.gameObject.SetActive(false);
                }
                isOpen = false;
            }
        }
    }

    private void checkHover()
    {
        isHover = false;
        if (RayHit.LeftHitName.Equals(barName) || RayHit.RightHitName.Equals(barName))
        {
            isHover = true;
        } 
        else
        {
            foreach (Transform child in transform)
            {
                if (RayHit.LeftHitName.Equals(child.name) || RayHit.RightHitName.Equals(child.name))
                {
                    isHover = true;
                }
                else
                {
                    foreach (Transform grandchild in child.transform)
                    {
                        if (RayHit.LeftHitName.Equals(grandchild.name) || RayHit.RightHitName.Equals(grandchild.name))
                        {
                            isHover = true;
                        }
                    }
                }
            }
        }

        if (isHover && !isOpen)
        {
            barObject.renderer.material.color = hoverColor;
            barObject.transform.localScale = hoverScale;
            barObject.transform.position = new Vector3(barObject.transform.position.x,
                barObject.transform.position.y, hoverZ);
        }
        else
        {
            barObject.renderer.material.color = originColor;
            barObject.transform.localScale = originScale;
            barObject.transform.position = new Vector3(barObject.transform.position.x,
                barObject.transform.position.y, originZ);
        }
    }
}
