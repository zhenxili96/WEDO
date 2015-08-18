using UnityEngine;
using System.Collections;
using System;

public class BackButton : BaseButton
{


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        base.DrawManage();
    }


    public override void run()
    {
        Application.LoadLevel(Name.ENTRYPAGENAME);
    }

}

