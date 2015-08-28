using UnityEngine;
using System.Collections;

public class RoomKeyDelete : MonoBehaviour
{

    private bool isHover = false;
    private Color originColor;
    private Color hoverColor = Color.red;
    private Vector3 originScale;
    private Vector3 hoverScale;
    private float scaleRate = 2;
    private float originZ;
    private float hoverZ;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
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
        if (RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
        {
            LeftHandProperty.clickUsed = true;
            if (RoomKey.curSentence.Length <= 0)
            {
                return;
            }
            RoomKey.curSentence = RoomKey.curSentence.Remove(RoomKey.curSentence.Length - 1);
        }
        if (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed)
        {
            RightHandProperty.clickUsed = true;
            if (RoomKey.curSentence.Length <= 0)
            {
                return;
            }
            RoomKey.curSentence = RoomKey.curSentence.Remove(RoomKey.curSentence.Length - 1);
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
