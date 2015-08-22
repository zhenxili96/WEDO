using UnityEngine;
using System.Collections;

public enum SHAPE { CIRCLE, TRIANGLE, RECTANGLE, ROUND_RECTANGLE };

public class ShapeClass : MonoBehaviour
{

    private bool isHover = false;
    private Color originColor;
    private bool shapeLock = false; //防止连续生成
    private float lockTime = 3.0f;
    public SHAPE shape;
    private static int circleInstanceCount = 0;
    private static int rectangleInstanceCount = 0;
    private static int roundRectangleInstanceCount = 0;
    private static int triangleInstanceCount = 0;
    private static Vector3 initPos = new Vector3(0, 1, 23);
    private static string SHAPEPARETNNAME = "ShapeInstance";

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        createInstanceManage();
    }

    private void createInstanceManage()
    {
        checkHover();
        if (isHover)
        {
            if ((RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
                || (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed))
            {
                if (shapeLock)
                {
                    return;
                }
                else
                {
                    shapeLock = true;
                    Invoke("shapeLockRelease", lockTime);
                }
                if ((RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed))
                {
                    LeftHandProperty.clickUsed = true;
                    Debug.Log("click used");
                }
                else if ((RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed))
                {
                    RightHandProperty.clickUsed = true;
                    Debug.Log("click used");
                }
                GameObject temp;
                switch (shape)
                {
                    case SHAPE.CIRCLE:
                        circleInstanceCount++;
                        temp = (GameObject)Instantiate(Resources.Load("shape_circle"));
                        temp.name = "CircleInstance_" + circleInstanceCount;
                        temp.transform.position = initPos;
                        temp.transform.parent = GameObject.Find(SHAPEPARETNNAME).gameObject.transform;
                        break;
                    case SHAPE.RECTANGLE:
                        rectangleInstanceCount++;
                        temp = (GameObject)Instantiate(Resources.Load("shape_rectangle"));
                        temp.name = "RectangleInstance_" + rectangleInstanceCount;
                        temp.transform.position = initPos;
                        temp.transform.parent = GameObject.Find(SHAPEPARETNNAME).gameObject.transform;
                        break;
                    case SHAPE.ROUND_RECTANGLE:
                        roundRectangleInstanceCount++;
                        temp = (GameObject)Instantiate(Resources.Load("shape_round_rectangle"));
                        temp.name = "RoundRectangleInstance_" + roundRectangleInstanceCount;
                        temp.transform.position = initPos;
                        temp.transform.parent = GameObject.Find(SHAPEPARETNNAME).gameObject.transform;
                        break;
                    case SHAPE.TRIANGLE:
                        triangleInstanceCount++;
                        temp = (GameObject)Instantiate(Resources.Load("shape_triangle"));
                        temp.name = "TriangleInstance_" + triangleInstanceCount;
                        temp.transform.position = initPos;
                        temp.transform.parent = GameObject.Find(SHAPEPARETNNAME).gameObject.transform;
                        break;
                    default:
                        temp = null;
                        break;
                }
            }
        }
    }

    private void shapeLockRelease()
    {
        shapeLock = false;
    }

    private void checkHover()
    {
        //Debug.Log(RayHit.hitName + " + " + gameObject.name);
        if (MenuBar.isOut && RayHit.hitName.Equals(gameObject.name))
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
