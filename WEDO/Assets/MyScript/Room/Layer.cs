using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Wedo_ClientSide;
using System.Reflection;
using System;
using System.Threading;

public class Layer
{
    public float ZMINPos = 35;
    public float ZSPACE = 0.01f;
    public string ParentName = "Layer";
    public GameObject layerObject = null;
    public string LayerPrefab = "RoomPrefab/LayerPrefab";
    public string CirclePrefab = "RoomPrefab/shape_circle";
    public string RectanglePrefab = "RoomPrefab/shape_rectangle";
    public string RoungRectanglePrefab = "RoomPrefab/shape_round_rectangle";
    public string TrianglePrefab = "RoomPrefab/shape_triangle";
    public string TextPrefab = "RoomPrefab/textPrefab";
    public string ShapeInstanceName = "ShapeInstance";
    public string TextInstanceName = "TextInstance";
    public Vector3 initPos = new Vector3(0, 0, 23);
    public Vector3 initScale = new Vector3(1, 1, 1);
    public Vector3 initRotate = new Vector3(0, 0, 0);
    public Color initColor = ColorTable.C7;
    public int ObjectCount = 1; //默认为1 为plane预留位置
    public string guid;
    public List<GameObject> instanceArray = new List<GameObject>();

    public Layer()
    {
        //空Layer
    }

    public Layer(float zmin, string myguid)
    {
        ZMINPos = zmin;
        layerObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(LayerPrefab));
        layerObject.transform.position = new Vector3(0, 0, 0);
        layerObject.name = "Layer" + RoomStatic.layerArray.Count;
        layerObject.transform.parent = GameObject.Find(ParentName).transform;
        guid = myguid;
    }

    public void refreshLayer(RoomLayer tempLayer)
    {
        int[] isFindArray = new int[instanceArray.Count];
        for (int i = 0; i < tempLayer.BoardMaterials.Count; i++)
        {
            bool isFind = false;
            int j = 0;
            for (; j < instanceArray.Count; j++)
            {
                if (tempLayer.BoardMaterials[i].Guid.Equals(instanceArray[j].GetComponent<InstanceType>().MyGuid))
                {
                    isFind = true;
                    isFindArray[j] = 1;
                    if (RoomStatic.curFocusObject!= null
                        && RoomStatic.curFocusObject.GetComponent<InstanceType>().MyGuid.Equals(tempLayer.BoardMaterials[i].Guid))
                    {
                        //当前focus 物体属性仅由本地决定并上传至server server不可干涉
                        Debug.Log("focus object is being designed by raw user");
                        break;
                    }
                    instanceArray[j].transform.localPosition =
                        new Vector3(tempLayer.BoardMaterials[i].CoordX,
                            tempLayer.BoardMaterials[i].CoordY,
                            tempLayer.BoardMaterials[i].CoordZ);
                    instanceArray[j].transform.localEulerAngles =
                        new Vector3(tempLayer.BoardMaterials[i].RotateX,
                            tempLayer.BoardMaterials[i].RotateY,
                            tempLayer.BoardMaterials[i].RotateZ);
                    instanceArray[j].transform.localScale =
                        new Vector3(tempLayer.BoardMaterials[i].ScalingX,
                            tempLayer.BoardMaterials[i].ScalingY,
                            tempLayer.BoardMaterials[i].ScalingZ);
                    instanceArray[j].transform.GetChild(0).renderer.material.color = ColorTable.getColor(tempLayer.BoardMaterials[i].Color);

                    //Debug.Log("download curfocus data:"
                    //    + tempLayer.BoardMaterials[i].CoordX + ","
                    //    + tempLayer.BoardMaterials[i].CoordY + ","
                    //    + tempLayer.BoardMaterials[i].CoordZ + ","
                    //    + tempLayer.BoardMaterials[i].RotateX + ","
                    //    + tempLayer.BoardMaterials[i].RotateY + ","
                    //    + tempLayer.BoardMaterials[i].RotateZ + ","
                    //    + tempLayer.BoardMaterials[i].ScalingX + ","
                    //    + tempLayer.BoardMaterials[i].ScalingY + ","
                    //    + tempLayer.BoardMaterials[i].ScalingZ + ","
                    //    + tempLayer.BoardMaterials[i].Color);

                    if (instanceArray[j].GetComponent<InstanceType>().Type == RoomStatic.TEXT)
                    {
                        instanceArray[j].transform.GetChild(0).GetComponent<TextMesh>().text =
                            tempLayer.BoardMaterials[i].Cont;
                        instanceArray[j].transform.GetChild(0).GetComponent<TextMesh>().fontSize =
                            tempLayer.BoardMaterials[i].FontSize;
                    }
                    break;
                }
            }
            //服务器有新meterial 添加到场景中
            if (!isFind)
            {
                AddInstance(tempLayer.BoardMaterials[i]);
            }
        }
        //服务器中不存在的materil 删除
        for (int i = 0; i < instanceArray.Count; i++)
        {
            if (isFindArray[i] == 0)
            {
                //MonoBehaviour.Destroy(instanceArray[i]);
                //instanceArray.RemoveAt(i);
                DeleteInstance(instanceArray[i]);
            }
        }
    }

    public void DeleteInstance(GameObject objectT)
    {
        MyLayerGameObject temp = new MyLayerGameObject();
        temp.key = this;
        temp.value = objectT;
        RoomStatic.UnDeleteMaterial.Enqueue(temp);
    }

    public void AddInstance(int type)
    {
        MyLayerInt temp = new MyLayerInt();
        temp.key = this;
        temp.value = type;
        RoomStatic.UnAddRawMaterial.Enqueue(temp);
    }

    public void RemoveInstance()
    { 
    }

    public void AddInstance(ClientMaterial tempInstance)
    {
        MyLayerClientMaterial temp = new MyLayerClientMaterial();
        temp.key = this;
        temp.value = tempInstance;
        RoomStatic.UnAddServerMaterial.Enqueue(temp);
    }

    public void AddInstancePrivate(int type)
    {
        GameObject tempObject;
        switch (type)
        {
            case RoomStatic.SHAPE_CIRCLE:
                RoomStatic.CircleInstanceCount++;
                tempObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(CirclePrefab));
                tempObject.name = "CircleInstance_" + RoomStatic.CircleInstanceCount;
                tempObject.transform.parent = layerObject.transform.FindChild(ShapeInstanceName);
                break;
            case RoomStatic.SHAPE_RECTANGLE:
                RoomStatic.RectangleInstanceCount++;
                tempObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(RectanglePrefab));
                tempObject.name = "RectangleInstance_" + RoomStatic.RectangleInstanceCount;
                tempObject.transform.parent = layerObject.transform.FindChild(ShapeInstanceName);
                break;
            case RoomStatic.SHAPE_ROUNDRECTANGLE:
                RoomStatic.RoundRectangleInstanceCount++;
                tempObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(RoungRectanglePrefab));
                tempObject.name = "RoundRectangleInstance_" + RoomStatic.RoundRectangleInstanceCount;
                tempObject.transform.parent = layerObject.transform.FindChild(ShapeInstanceName);
                break;
            case RoomStatic.SHAPE_TRIANGLE:
                RoomStatic.TriangleInstanceCount++;
                tempObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(TrianglePrefab));
                tempObject.name = "TriangleInstance_" + RoomStatic.TriangleInstanceCount;
                tempObject.transform.parent = layerObject.transform.FindChild(ShapeInstanceName);
                break;
            case RoomStatic.TEXT:
                RoomStatic.TextInstanceCount++;
                tempObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(TextPrefab));
                tempObject.name = "TextInstance_" + RoomStatic.TextInstanceCount;
                tempObject.transform.parent = layerObject.transform.FindChild(TextInstanceName);
                break;
            default:
                tempObject = null;
                break;
        }
        Debug.Log("add a object from buttonclick");
        tempObject.GetComponent<ShapeInstance>().parentLayer = this;
        tempObject.transform.localPosition = initPos;
        tempObject.transform.localEulerAngles = initRotate;
        tempObject.transform.localScale = initScale;
        tempObject.transform.GetChild(0).renderer.material.color = initColor;
        tempObject.GetComponent<InstanceType>().MyGuid = "---";
        instanceArray.Add(tempObject);
        ObjectCount++;
    }

    public void AddInstancePrivate(ClientMaterial tempInstance)
    {
        GameObject tempObject;
        switch (tempInstance.Type)
        {
            case RoomStatic.SHAPE_CIRCLE:
                RoomStatic.CircleInstanceCount++;
                tempObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(CirclePrefab));
                tempObject.name = "CircleInstance_" + RoomStatic.CircleInstanceCount;
                tempObject.transform.parent = layerObject.transform.FindChild(ShapeInstanceName);
                break;
            case RoomStatic.SHAPE_RECTANGLE:
                RoomStatic.RectangleInstanceCount++;
                tempObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(RectanglePrefab));
                tempObject.name = "RectangleInstance_" + RoomStatic.RectangleInstanceCount;
                tempObject.transform.parent = layerObject.transform.FindChild(ShapeInstanceName);
                break;
            case RoomStatic.SHAPE_ROUNDRECTANGLE:
                RoomStatic.RoundRectangleInstanceCount++;
                tempObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(RoungRectanglePrefab));
                tempObject.name = "RoundRectangleInstance_" + RoomStatic.RoundRectangleInstanceCount;
                tempObject.transform.parent = layerObject.transform.FindChild(ShapeInstanceName);
                break;
            case RoomStatic.SHAPE_TRIANGLE:
                RoomStatic.TriangleInstanceCount++;
                tempObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(TrianglePrefab));
                tempObject.name = "TriangleInstance_" + RoomStatic.TriangleInstanceCount;
                tempObject.transform.parent = layerObject.transform.FindChild(ShapeInstanceName);
                break;
            case RoomStatic.TEXT:
                RoomStatic.TextInstanceCount++;
                tempObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(TextPrefab));
                tempObject.name = "TextInstance_" + RoomStatic.TextInstanceCount;
                tempObject.transform.parent = layerObject.transform.FindChild(TextInstanceName);
                break;
            default:
                tempObject = null;
                break;
        }
        Debug.Log("add a object from server");
        tempObject.GetComponent<ShapeInstance>().parentLayer = this;
        tempObject.transform.localPosition = new Vector3(
            tempInstance.CoordX, tempInstance.CoordY, tempInstance.CoordZ);
        tempObject.transform.localEulerAngles = new Vector3(
            tempInstance.RotateX, tempInstance.RotateY, tempInstance.RotateZ);
        tempObject.transform.localScale = new Vector3(
            tempInstance.ScalingX, tempInstance.ScalingY, tempInstance.ScalingZ);
        tempObject.transform.GetChild(0).renderer.material.color = ColorTable.getColor(tempInstance.Color);
        tempObject.GetComponent<InstanceType>().MyGuid = tempInstance.Guid;
        tempObject.GetComponent<InstanceType>().LayerGuid = tempInstance.BelongLevel;
        instanceArray.Add(tempObject);
        ObjectCount++;
    }
}
