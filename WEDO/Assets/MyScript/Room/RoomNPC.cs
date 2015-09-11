using UnityEngine;
using System.Collections;

public class RoomNPC : MonoBehaviour
{

    public string EditManagerName = "room_editmanager";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkShow();
    }

    private void checkShow()
    {
        if (RoomStatic.curFocus.Equals(""))
        {
            transform.FindChild(EditManagerName).gameObject.SetActive(false);
        }
        else
        {
            transform.FindChild(EditManagerName).gameObject.SetActive(true);
        }
    }
}
