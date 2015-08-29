﻿using UnityEngine;
using System.Collections;

public class RoomStatic : MonoBehaviour
{

    public static string curFocus = "";
    public static int LayerCount = 1;
    public static int curLayer = 1;
    public static ArrayList layerArray = new ArrayList();

    // Use this for initialization
    void Start()
    {
        layerArray.Add(new Layer());
        layerArray.Add(new Layer(35, 34, 0, true));
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class Layer
{
    public float ZMINPos = 25;
    public float ZMAXPos = 35;
    public int ObjectCount = -1;
    public bool isActive = true;
    public float ZSPACE = 0.01f;

    public Layer()
    {
        //空Layer
    }

    public Layer(float zmin, float zmax, int objectcount = 0, bool isactive = true)
    {
        ZMINPos = zmin;
        ZMAXPos = zmax;
        ObjectCount = objectcount;
        isActive = isactive;
    }
}
