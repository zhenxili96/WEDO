using UnityEngine;
using System.Collections;

public class AddLayerButton : MonoBehaviour
{

    private bool isHover = false;
    private Color originColor;
    private Color hoverColor = Color.red;
    private string LAYERTEXTNAME = "LayerText";
    private int ZMIN = 35;  //创建层最小Z值
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
            RoomStatic.layerArray.Add(new Layer(ZMIN - RoomStatic.layerArray.Count, RoomStatic.UNSETGUID));
            WholeStatic.curRoomInterface.AddLayer(RoomStatic.layerArray.Count, 0, 0, ZMIN + 1 - RoomStatic.layerArray.Count);
            RoomStatic.curLayer = RoomStatic.layerArray.Count - 1;
        }
        if (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            RoomStatic.layerArray.Add(new Layer(ZMIN - RoomStatic.layerArray.Count, RoomStatic.UNSETGUID));
            WholeStatic.curRoomInterface.AddLayer(RoomStatic.layerArray.Count, 0, 0, ZMIN + 1 - RoomStatic.layerArray.Count);
            RoomStatic.curLayer = RoomStatic.layerArray.Count - 1;
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
