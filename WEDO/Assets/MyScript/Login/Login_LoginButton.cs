using UnityEngine;
using System.Collections;
using System;
using Wedo_ClientSide;

public class Login_LoginButton : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 2;
    public float originZ;
    public float hoverZ;
    public string NPCName = "NPC";

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
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                if (checkLogin())
                {
                    Application.LoadLevel(Name.ENTRYPAGENAME);
                }
            }
        }
    }

    private bool checkLogin()
    {
        string account = Login_account.account;
        string password = Login_password.password;
        //TODO 登陆校验查询，若登陆成功则存储当前用户信息
        if (account.Length == 0)
        {
            AttentionStatic.callAttention(NPCName, "账号为空，请重新输入！");
            return false;
        }
        if (password.Length == 0)
        {
            AttentionStatic.callAttention(NPCName, "密码为空，请重新输入！");
            return false;
        }
        ClientUser user = ProxyInterface.User_Login(account, password);
        if (user == null)
        {
            AttentionStatic.callAttention(NPCName, "用户名或密码错误，请重新输入！");
            return false;
        }
        WholeStatic.curUser = user;
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

