using UnityEngine;
using System.Collections;

public class MemberState : MonoBehaviour
{

    public string FirstManName = "firstman";
    public string SecondManName = "secondman";
    public string ThirdManName = "thirdman";


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkRoomMemberState();
    }

    private void checkRoomMemberState()
    {
        if (WholeStatic.curRoomInterface == null)
        {
            Debug.Log("curRoomInterface null no member");
        }
        Debug.Log("cur User count " + WholeStatic.curRoomInterface.RoomUsers.Count);
        switch (WholeStatic.curRoomInterface.RoomUsers.Count)
        {
            case 0:
                transform.FindChild(FirstManName).gameObject.SetActive(true);
                transform.FindChild(FirstManName).GetChild(0).GetComponent<TextMesh>().text
                    = WholeStatic.curUser.Account;
                transform.FindChild(SecondManName).gameObject.SetActive(false);
                transform.FindChild(ThirdManName).gameObject.SetActive(false);
                break;
            case 1:
                transform.FindChild(FirstManName).gameObject.SetActive(true);
                transform.FindChild(SecondManName).gameObject.SetActive(true);
                transform.FindChild(ThirdManName).gameObject.SetActive(false);
                if (RoomStatic.MyEnterTime == 1)
                {
                    transform.FindChild(FirstManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curUser.Account;
                    transform.FindChild(SecondManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curRoomInterface.RoomUsers[0].UserNickName;
                }
                break;
            case 2:
                transform.FindChild(FirstManName).gameObject.SetActive(true);
                transform.FindChild(SecondManName).gameObject.SetActive(true);
                transform.FindChild(ThirdManName).gameObject.SetActive(true);
                if (RoomStatic.MyEnterTime == 1)
                {
                    transform.FindChild(FirstManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curUser.Account;
                    transform.FindChild(SecondManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curRoomInterface.RoomUsers[0].UserNickName;
                    transform.FindChild(ThirdManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curRoomInterface.RoomUsers[1].UserNickName;
                }
                if (RoomStatic.MyEnterTime == 2)
                {
                    transform.FindChild(FirstManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curRoomInterface.RoomUsers[0].UserNickName;
                    transform.FindChild(SecondManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curUser.Account;
                    transform.FindChild(ThirdManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curRoomInterface.RoomUsers[1].UserNickName;
                }
                if (RoomStatic.MyEnterTime == 3)
                {
                    transform.FindChild(FirstManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curRoomInterface.RoomUsers[0].UserNickName;
                    transform.FindChild(SecondManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curRoomInterface.RoomUsers[1].UserNickName;
                    transform.FindChild(ThirdManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curUser.Account;
                }
                break;
            default:
                transform.FindChild(FirstManName).gameObject.SetActive(true);
                transform.FindChild(SecondManName).gameObject.SetActive(true);
                transform.FindChild(ThirdManName).gameObject.SetActive(true);
                if (RoomStatic.MyEnterTime == 1)
                {
                    transform.FindChild(FirstManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curUser.Account;
                    transform.FindChild(SecondManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curRoomInterface.RoomUsers[0].UserNickName;
                    transform.FindChild(ThirdManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curRoomInterface.RoomUsers[1].UserNickName;
                }
                if (RoomStatic.MyEnterTime == 2)
                {
                    transform.FindChild(FirstManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curRoomInterface.RoomUsers[0].UserNickName;
                    transform.FindChild(SecondManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curUser.Account;
                    transform.FindChild(ThirdManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curRoomInterface.RoomUsers[1].UserNickName;
                }
                if (RoomStatic.MyEnterTime == 3)
                {
                    transform.FindChild(FirstManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curRoomInterface.RoomUsers[0].UserNickName;
                    transform.FindChild(SecondManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curRoomInterface.RoomUsers[1].UserNickName;
                    transform.FindChild(ThirdManName).GetChild(0).GetComponent<TextMesh>().text
                        = WholeStatic.curUser.Account;
                }
                break;
        }
    }
}
