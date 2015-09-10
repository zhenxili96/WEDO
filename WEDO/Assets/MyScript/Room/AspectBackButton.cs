using UnityEngine;
using System.Collections;

public class AspectBackButton : MonoBehaviour
{

    public static string MODECHANGENAME = "ModeChange";
    public static string CURMODENAME = "Mode2";
    public static string ANOTHERMODENAME = "Mode1";
    public static string LAYERNAME = "Layer";
    public bool isHover = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkClick();
    }

    private void checkClick()
    {
        if (RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed
            || RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RoomStatic.curMode = RoomMode.Mode1;
            GameObject.Find(CURMODENAME).SetActive(false);
            GameObject.Find(MODECHANGENAME).transform.Find(ANOTHERMODENAME).gameObject.SetActive(true);
            LayRay.rayStyle = RayStyle.Ortho;
            GameObject.Find(LAYERNAME).GetComponent<LayerManager>().backChange();
            foreach (Transform child in GameObject.Find(LAYERNAME).transform)
            {
                //child.gameObject.SendMessage("backChange");
                child.gameObject.GetComponent<LayerItemManager>().backChange();
            }
            //GameObject.Find(LAYERNAME).SendMessage("backChange");
        }
    }

    private void checkHover()
    {
        isHover = false;

        if (RayHit.LeftHitName.Equals(name) && !LeftHandProperty.isClosed
            || RayHit.RightHitName.Equals(name) && !RightHandProperty.isClosed)
        {
            isHover = true;
        }
        else
        {
            isHover = false;
        }
    }
}

