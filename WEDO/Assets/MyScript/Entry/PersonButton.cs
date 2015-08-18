using UnityEngine;
using System.Collections;
using System;

public class PersonButton : BaseButton
{

    private bool isExpand = false;
    private Vector3 expandScale = new Vector3(1.3f, 1.3f, 1.3f);
    private Vector3 originScale = new Vector3();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (base.isHover && !isExpand)
        {
            expand();
        }
        if (isExpand && !base.isHover)
        {
            normalScale();
        }
        base.DrawManage();
    }

    private void expand()
    {
        isExpand = true;
        originScale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(originScale.x * expandScale.x,
            originScale.y * expandScale.y, originScale.z * expandScale.z);
    }

    private void normalScale()
    {
        isExpand = false;
        gameObject.transform.localScale = originScale;
    }

    public override void run()
    {
        
    }

}

