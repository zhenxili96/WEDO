using UnityEngine;
using System.Collections;

public class Person_Information : MonoBehaviour
{

    public string AccountTextName = "accountText";
    public string EmailTextName = "emailText";
    public string IDTextName = "IDText";
    public string SexTextName = "sexText";
    public GameObject accountTextObject;
    public GameObject emailTextObject;
    public GameObject IDTextObject;
    public GameObject sexTextObject;

    // Use this for initialization
    void Start()
    {
        accountTextObject = GameObject.Find(AccountTextName);
        emailTextObject = GameObject.Find(EmailTextName);
        IDTextObject = GameObject.Find(IDTextName);
        sexTextObject = GameObject.Find(SexTextName);
    }

    // Update is called once per frame
    void Update()
    {
        accountTextObject.GetComponent<TextMesh>().text = "昵称：" + PersonStatic.account;
        emailTextObject.GetComponent<TextMesh>().text = "邮箱：" + PersonStatic.email;
        IDTextObject.GetComponent<TextMesh>().text = "ID：" + PersonStatic.ID;
        sexTextObject.GetComponent<TextMesh>().text = "性别：" + PersonStatic.sex;
    }
}
