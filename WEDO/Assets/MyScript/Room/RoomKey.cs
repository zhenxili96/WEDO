using UnityEngine;
using System.Collections;

public class RoomKey : MonoBehaviour
{

    private bool isHover = false;
    private Color originColor;
    private bool isPressed = false;
    private string MySelf = "";
    private Vector3 originScale;
    private Vector3 hoverScale;
    private float scaleRate = 2;
    private float originZ;
    private float hoverZ;

    public static string curSentence;

    // Use this for initialization
    void Start()
    {
        originColor = gameObject.renderer.material.color;
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        MySelf = name;
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
                curSentence += MySelf;
                LeftHandProperty.clickUsed = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                curSentence += MySelf;
                RightHandProperty.clickUsed = true;
            }
        }
    }

    private void checkHover()
    {
        if (RoomKeyBoard.isOpen && (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name)))
        {
            isHover = true;
            renderer.material.color = Color.red;
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            isHover = false;
            renderer.material.color = originColor;
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }
}
