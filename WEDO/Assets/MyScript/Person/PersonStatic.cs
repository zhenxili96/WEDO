using UnityEngine;
using System.Collections;

public class PersonStatic : MonoBehaviour
{

    public static string account = "";
    public static string userimage = "";
    public static string email = "";
    public static string sex = "";

    // Use this for initialization
    void Start()
    {
        Keyboard.init();
        getUserInfomation();
        LayRay.rayStyle = RayStyle.Ortho;
        LeftHandProperty.HandInit();
        RightHandProperty.HandInit();
    }

    private void getUserInfomation()
    {
        //TODO 获取当前用户资料信息
        if (WholeStatic.curUser != null)
        {
            account = WholeStatic.curUser.Account;
            userimage = WholeStatic.curUser.Avatar;
            email = WholeStatic.curUser.MailBox;
            string tempsex = WholeStatic.curUser.Sex;
            switch (tempsex)
            {
                case "0":
                    sex = "男";
                    break;
                case "1":
                    sex = "女";
                    break;
                default:
                    sex = "男";
                    break;
            }
        }
        else
        {
            Debug.Log("当前无用户");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

