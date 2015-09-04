using UnityEngine;
using System.Collections;
using System;

public class Login_LoginButton : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 2;
    public float originZ;
    public float hoverZ;

    // Use this for initialization
    void Start()
    {
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = transform.position.z;
        hoverZ = originZ - 1;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkClick();
    }

    private void checkClick()
    {
        if (isHover)
        {
            if (LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                LeftHandProperty.clickUsed = true;
                if (checkLogin())
                {
                    Application.LoadLevel(Name.ENTRYPAGENAME);
                }
                else
                {
                    //TODO 弹出登录失败提示窗
                }
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                Application.LoadLevel(Name.ENTRYPAGENAME);
                RightHandProperty.clickUsed = true;
            }
        }
    }

    private bool checkLogin()
    {
        string account = Login_account.account;
        string password = Login_password.password;
        //TODO 登陆校验查询，若登陆成功则存储当前用户信息
        return true;
    }

    private void checkHover()
    {
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            isHover = true;
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            isHover = false;
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }
}

