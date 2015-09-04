﻿using UnityEngine;
using System.Collections;

public class Person_Information : MonoBehaviour
{

    public string AccountTextName = "accountText";
    public string EmailTextName = "emailText";
    public string UserImageName = "Person_image";
    public string SexTextName = "sexText";
    public GameObject accountTextObject;
    public GameObject emailTextObject;
    public GameObject userImageObject;
    public GameObject sexTextObject;

    // Use this for initialization
    void Start()
    {
        accountTextObject = GameObject.Find(AccountTextName);
        emailTextObject = GameObject.Find(EmailTextName);
        userImageObject = GameObject.Find(UserImageName);
        sexTextObject = GameObject.Find(SexTextName);
    }

    // Update is called once per frame
    void Update()
    {
        accountTextObject.GetComponent<TextMesh>().text = "昵称：" + PersonStatic.account;
        emailTextObject.GetComponent<TextMesh>().text = "邮箱：" + PersonStatic.email;
        sexTextObject.GetComponent<TextMesh>().text = "性别：" + PersonStatic.sex;
        //TODO 用户头像替换
    }
}
