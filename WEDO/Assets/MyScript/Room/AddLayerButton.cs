using UnityEngine;
using System.Collections;

public class AddLayerButton : MonoBehaviour
{

    private bool isHover = false;
    private Color originColor;
    private Color hoverColor = Color.red;
    private string LAYERTEXTNAME = "LayerText";
    private int ZMIN = 35;  //创建层最小Z值
    private int ZMAX = 24;  //创建曾最大Z值
    private Vector3 originScale;
    private Vector3 hoverScale;
    private float scaleRate = 2;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
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
            ((Layer)RoomStatic.layerArray[RoomStatic.curLayer]).isActive = false;
            RoomStatic.layerArray.Add(new Layer(ZMIN + 1 - RoomStatic.curLayer, ZMIN + 2 - RoomStatic.curLayer));
            RoomStatic.LayerCount++;
            GameObject layerObject = new GameObject();
            layerObject.name = "Layer" + RoomStatic.LayerCount;
            layerObject.transform.position = new Vector3(0, 0, 0);
            GameObject shapeObject = new GameObject();
            shapeObject.name = "ShapeInstance";
            shapeObject.transform.position = new Vector3(0, 0, 0);
            shapeObject.transform.parent = layerObject.transform;
            GameObject textObject = new GameObject();
            textObject.name = "TextInstance";
            textObject.transform.position = new Vector3(0, 0, 0);
            textObject.transform.parent = layerObject.transform;
            RoomStatic.curLayer = RoomStatic.LayerCount;
            GameObject.Find(LAYERTEXTNAME).GetComponent<TextMesh>().text = "Layer" + RoomStatic.LayerCount;
        }
        if (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            RoomStatic.LayerCount++;
            RoomStatic.curLayer = RoomStatic.LayerCount;
            GameObject.Find(LAYERTEXTNAME).GetComponent<TextMesh>().text = "Layer" + RoomStatic.LayerCount;
        }
    }

    private void checkHover()
    {
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
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
