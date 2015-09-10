using UnityEngine;
using System.Collections;

public class AspectChangeButton : MonoBehaviour
{

    private static string MODECHANGENAME = "ModeChange";
    private static string CURMODENAME = "Mode1";
    private static string ANOTHERMODENAME = "Mode2";
    private static string LAYERNAME = "Layer";
    private bool isHover = false;
    private Color originColor;
    private Vector3 originScale;
    private Vector3 hoverScale;
    private float scaleRate = 1.5f;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
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
            RoomStatic.curMode = RoomMode.Mode2;
            GameObject.Find(CURMODENAME).SetActive(false);
            GameObject.Find(MODECHANGENAME).transform.Find(ANOTHERMODENAME).gameObject.SetActive(true);
            LayRay.rayStyle = RayStyle.Perspect;
            GameObject.Find(LAYERNAME).GetComponent<LayerManager>().callChange();
            foreach (Transform child in GameObject.Find(LAYERNAME).transform)
            {
                //child.gameObject.SendMessage("callChange");
                child.gameObject.GetComponent<LayerItemManager>().callChange();
            }
            //GameObject.Find(LAYERNAME).SendMessage("callChange");
        }
    }

    private void checkHover()
    {
        isHover = false;

        if (RayHit.LeftHitName.Equals(name) && !LeftHandProperty.isClosed
            || RayHit.RightHitName.Equals(name) && !RightHandProperty.isClosed)
        {
            isHover = true;
            transform.localScale = hoverScale;
        }
        else
        {
            isHover = false;
            transform.localScale = originScale;
        }
    }
}

