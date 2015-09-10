using UnityEngine;
using System.Collections;

public class AddLayerButton : MonoBehaviour
{

    private bool isHover = false;
    private Color originColor;
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
            GameObject.Find(LAYERTEXTNAME).GetComponent<TextMesh>().text = "Layer" + RoomStatic.curLayer;
            //RoomStatic.layerArray.Add(new Layer(ZMIN - RoomStatic.layerArray.Count, RoomStatic.UNSETGUID));
            //WholeStatic.curRoomInterface.AddLayer(RoomStatic.layerArray.Count, 0, 0, ZMIN + 1 - RoomStatic.layerArray.Count);
            WholeStatic.curRoomInterface.AddLayer(RoomStatic.layerArray.Count + 1, 0, 0, ZMIN - RoomStatic.layerArray.Count);
            //RoomStatic.curLayer = RoomStatic.layerArray.Count;
            Debug.Log("add layer raw and server");
        }
        if (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            GameObject.Find(LAYERTEXTNAME).GetComponent<TextMesh>().text = "Layer" + RoomStatic.curLayer;
            //RoomStatic.layerArray.Add(new Layer(ZMIN - RoomStatic.layerArray.Count, RoomStatic.UNSETGUID));
            //WholeStatic.curRoomInterface.AddLayer(RoomStatic.layerArray.Count, 0, 0, ZMIN + 1 - RoomStatic.layerArray.Count);
            WholeStatic.curRoomInterface.AddLayer(RoomStatic.layerArray.Count + 1, 0, 0, ZMIN - RoomStatic.layerArray.Count);
            //RoomStatic.curLayer = RoomStatic.layerArray.Count;
            Debug.Log("add layer raw and server");
        }
    }

    private void checkHover()
    {
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            isHover = true;
            transform.localScale = hoverScale;
        }
        else
        {
            isHover = false;
            transform.localScale = originScale;
        }
    }

}
