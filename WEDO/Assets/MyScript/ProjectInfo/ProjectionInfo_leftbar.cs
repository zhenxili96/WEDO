using UnityEngine;
using System.Collections;

public class ProjectionInfo_leftbar : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 outPos = new Vector3();
    public Vector3 inPos = new Vector3();
    public float outSpeed;
    public float inSpeed;
    public bool isOpen = false;
    public bool isOut = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkOut();
    }

    private void checkOut()
    {
        if (isHover)
        {
            if (transform.position.x < outPos.x)
            {
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * outSpeed);
            }
            else
            {
                isOpen = true;
            }
        }
        else
        {
            if (transform.position.x > inPos.x)
            {
                transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * inSpeed);
            }
        }
    }

    private void checkHover()
    {
        isHover = false;
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
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
                        else
                        {
                            foreach (Transform grandgrandchild in grandchild.transform)
                            {
                                if (RayHit.LeftHitName.Equals(grandgrandchild.name) || RayHit.RightHitName.Equals(grandgrandchild.name))
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
}
