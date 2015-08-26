﻿using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour
{

    private bool isHover = false;
    private Color originColor;
    private bool isPressed = false;
    private string MySelf = "";

    public static string curSentence;

    // Use this for initialization
    void Start()
    {
        originColor = gameObject.renderer.material.color;
        MySelf = name;
    }

    // Update is called once per frame
    void Update()
    {
        clickManager();
    }

    private void clickManager()
    {
        checkHover();
        if (isHover && HandProperty.isClosed)
        {
            renderer.material.color = Color.red;
            if (!isPressed)
            {
                isPressed = true;
                curSentence += MySelf;
            }
        }
        else
        {
            isPressed = false;
            renderer.material.color = originColor;
        }
    }

    private void checkHover()
    {
        if (RayHit.hitName.Equals(name))
        {
            isHover = true;
        }
        else
        {
            isHover = false;
        }
    }
}
