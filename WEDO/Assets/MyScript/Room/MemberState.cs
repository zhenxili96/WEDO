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
        switch (WholeStatic.curRoomInterface.RoomUsers.Count)
        {
            case 0:
                transform.FindChild(FirstManName).gameObject.SetActive(true);
                transform.FindChild(SecondManName).gameObject.SetActive(false);
                transform.FindChild(ThirdManName).gameObject.SetActive(false);
                break;
            case 1:
                transform.FindChild(FirstManName).gameObject.SetActive(true);
                transform.FindChild(SecondManName).gameObject.SetActive(true);
                transform.FindChild(ThirdManName).gameObject.SetActive(false);
                break;
            case 2:
                transform.FindChild(FirstManName).gameObject.SetActive(true);
                transform.FindChild(SecondManName).gameObject.SetActive(true);
                transform.FindChild(ThirdManName).gameObject.SetActive(true);
                break;
            default:
                transform.FindChild(FirstManName).gameObject.SetActive(true);
                transform.FindChild(SecondManName).gameObject.SetActive(true);
                transform.FindChild(ThirdManName).gameObject.SetActive(true);
                break;
        }
    }
}
