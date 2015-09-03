using UnityEngine;
using System.Collections;

public class ProjectionInfo_topaddbutton : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.1f;
    public float originZ;
    public float hoverZ;
    public int projectionCount = 0;
    public Vector3 projectionSpace = new Vector3(58, 0, 0);
    public Vector3 projectionRotation = new Vector3(90, 180, 0);
    public Vector3 projectionLocalScale = new Vector3(5, 1, 5);
    public string ProjectionPrefabName = "projection";

    // Use this for initialization
    void Start()
    {
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
                projectionCount++;
                GameObject newPorjection = (GameObject)Instantiate(Resources.Load(ProjectionPrefabName));
                newPorjection.transform.localScale = projectionLocalScale;
                newPorjection.transform.eulerAngles = projectionRotation;
                newPorjection.transform.position = transform.position + projectionCount * projectionSpace;
                newPorjection.AddComponent<ProjectionInfo_topprojection>();
                LeftHandProperty.clickUsed = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                projectionCount++;
                GameObject newPorjection = (GameObject)Instantiate(Resources.Load(ProjectionPrefabName));
                newPorjection.transform.localScale = projectionLocalScale;
                newPorjection.transform.eulerAngles = projectionRotation;
                newPorjection.transform.position = transform.position + projectionCount * projectionSpace;
                newPorjection.AddComponent<ProjectionInfo_topprojection>();
                RightHandProperty.clickUsed = true;
            }
        }
    }

    private void checkHover()
    {
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            isHover = true;
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            isHover = false;
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }
}
