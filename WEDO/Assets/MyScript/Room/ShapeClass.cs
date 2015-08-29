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
    private static Vector3 initPos = new Vector3(0, 0, 23);
    private static Vector3 initScale = new Vector3(1, 1, 1);
    private static string SHAPEPARETNNAME = "ShapeInstance";
    private Vector3 originScale;
    private Vector3 hoverScale;
    private float scaleRate = 2;
    private float originZ;
    private float hoverZ;

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
        createInstanceManage();
    }

    private void createInstanceManage()
    {
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
                }
                else if ((RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed))
                {
                    RightHandProperty.clickUsed = true;
                }
                GameObject temp;
                switch (shape)
                {
                    case SHAPE.CIRCLE:
                        circleInstanceCount++;
                        temp = (GameObject)Instantiate(Resources.Load("shape_circle"));
                        temp.name = "CircleInstance_" + circleInstanceCount;
                        temp.transform.position = initPos;
                        temp.transform.localScale = initScale;
                        string curLayer = "Layer" + RoomStatic.curLayer;
                        GameObject parent = GameObject.Find(curLayer).transform.Find(SHAPEPARETNNAME).gameObject;
                        temp.transform.parent = parent.transform;
                        break;
                    case SHAPE.RECTANGLE:
                        rectangleInstanceCount++;
                        temp = (GameObject)Instantiate(Resources.Load("shape_rectangle"));
                        temp.name = "RectangleInstance_" + rectangleInstanceCount;
                        temp.transform.position = initPos;
                        temp.transform.localScale = initScale;
                        string curLayer1 = "Layer" + RoomStatic.curLayer;
                        GameObject parent1 = GameObject.Find(curLayer1).transform.Find(SHAPEPARETNNAME).gameObject;
                        temp.transform.parent = parent1.transform;
                        break;
                    case SHAPE.ROUND_RECTANGLE:
                        roundRectangleInstanceCount++;
                        temp = (GameObject)Instantiate(Resources.Load("shape_round_rectangle"));
                        temp.name = "RoundRectangleInstance_" + roundRectangleInstanceCount;
                        temp.transform.position = initPos;
                        temp.transform.localScale = initScale;
                        string curLayer2 = "Layer" + RoomStatic.curLayer;
                        GameObject parent2 = GameObject.Find(curLayer2).transform.Find(SHAPEPARETNNAME).gameObject;
                        temp.transform.parent = parent2.transform;
                        break;
                    case SHAPE.TRIANGLE:
                        triangleInstanceCount++;
                        temp = (GameObject)Instantiate(Resources.Load("shape_triangle"));
                        temp.name = "TriangleInstance_" + triangleInstanceCount;
                        temp.transform.position = initPos;
                        temp.transform.localScale = initScale;
                        string curLayer3 = "Layer" + RoomStatic.curLayer;
                        GameObject parent3 = GameObject.Find(curLayer3).transform.Find(SHAPEPARETNNAME).gameObject;
                        temp.transform.parent = parent3.transform;
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
