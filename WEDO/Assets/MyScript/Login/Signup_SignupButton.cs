using UnityEngine;
using System.Collections;
using System;
using Wedo_ClientSide;

public class Signup_SignupButton : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 2;
    public float originZ;
    public float hoverZ;
    public string NPCName = "NPC";
    public string UNSET = "UNSET";

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
                if (checkSignup())
                {
                    AttentionStatic.callAttention(NPCName, "注册成功");
                }
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                if (checkSignup())
                {
                    AttentionStatic.callAttention(NPCName, "注册成功");
                }
            }
        }
    }

    private bool checkSignup()
    {
        string account = Signup_account.signupAccount;
        string password = Signup_password.signupPassword;
        string repassword = Signup_repassword.signupRePassword;
        //TODO 检测并注册账号
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
        if (!password.Equals(repassword))
        {
            AttentionStatic.callAttention(NPCName, "两次输入密码不同，请重新输入！");
            return false;
        }
        ClientUser user = ProxyInterface.User_Register(account, password, UNSET, UNSET, UNSET);
        if (user == null)
        {
            AttentionStatic.callAttention(NPCName, "注册失败！");
            return false;
        }
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

