using UnityEngine;
using System.Collections;

public class MenuBar : MonoBehaviour
{

    private bool statue = false;
    private bool isHover = false;
    public static bool isOut = false;
    private Vector3 outPos = new Vector3(-144f, 0, 40);
    private Vector3 inPos = new Vector3(-190f, 0, 40);
    private float outSpeed = 40f;
    private float inSpeed = 50f;
    private string planeName = "menuplane";
    private Color originBarColor;
    private Color barNewColor = Color.red;


    // Use this for initialization
    void Start()
    {
        originBarColor = GameObject.Find(planeName).renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkStatue();
        checkPlace();
    }

    private void checkPlace()
    {
        Vector3 curPos = transform.position;
        if (statue && curPos.x <= outPos.x)
        {
            GameObject.Find(planeName).renderer.material.color = barNewColor;
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * outSpeed);
        }
        else
        {
            GameObject.Find(planeName).renderer.material.color = originBarColor;
        }
        if (!statue && curPos.x >= inPos.x)
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * inSpeed);
        }
        if (curPos.x >= outPos.x)
        {
            isOut = true;
        }
        else
        {
            isOut = false;
        }
    }

    private void checkStatue()
    {
        if (isHover && !statue)
        {
            statue = true;
        }
        if (!isHover)
        {
            statue = false;
        }
    }

    private void checkHover()
    {
        isHover = false;

        if (RayHit.LeftHitName.Equals(name) && !LeftHandProperty.isClosed
            || RayHit.RightHitName.Equals(name) && !RightHandProperty.isClosed)
        {
            isHover = true;
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (RayHit.LeftHitName.Equals(child.name) && !LeftHandProperty.isClosed
                    || RayHit.RightHitName.Equals(child.name) && !RightHandProperty.isClosed)
                {
                    isHover = true;
                }
                else
                {
                    foreach (Transform grandChild in child.transform)
                    {
                        if (RayHit.LeftHitName.Equals(grandChild.name))
                        {
                            if (statue)
                            {
                                isHover = true;
                            }
                        } 
                        else if (RayHit.RightHitName.Equals(grandChild.name))
                        {
                            if (statue)
                            {
                                isHover = true;
                            }
                        }
                    }
                }
            }
        }
    }

}
