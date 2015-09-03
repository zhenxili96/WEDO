using UnityEngine;
using System.Collections;

public class ProjectionInfo_bottominput : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.1f;
    public float originZ;
    public float hoverZ;
    public Vector3 outPos = new Vector3();
    public Vector3 inPos = new Vector3();
    public float outSpeed;
    public float inSpeed;
    public bool isOut = false;
    public bool isOpen = false;
    public GameObject inputObject;

    // Use this for initialization
    void Start()
    {
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = transform.position.z;
        hoverZ = originZ - 1;
        inputObject = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkClick();
        checkContent();
        checkOut();
    }

    private void checkOut()
    {
        if (!Keyboard.isOut)
        {
            isOut = false;
        }
        isOpen = false;
        if (isOut)
        {
            if (transform.parent.transform.position.y < outPos.y)
            {
                transform.parent.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * outSpeed);
            }
            else
            {
                isOpen = true;
            }
        }
        else
        {
            if (transform.parent.transform.position.y > inPos.y)
            {
                transform.parent.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * inSpeed);
            }
        }
    }

    private void checkContent()
    {
        if (isOpen)
        {
            inputObject.GetComponent<TextMesh>().text = Keyboard.curSentence;
        }
    }

    private void checkClick()
    {
        if (isHover)
        {
            if (LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                isOut = true;
                Keyboard.curSentence = inputObject.GetComponent<TextMesh>().text;
                Keyboard.isOut = true;
                LeftHandProperty.clickUsed = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                isOut = true;
                Keyboard.curSentence = inputObject.GetComponent<TextMesh>().text;
                Keyboard.isOut = true;
                RightHandProperty.clickUsed = true;
            }
        }
        else
        {
            if ((LeftHandProperty.isClosed && !LeftHandProperty.clickUsed) || (RightHandProperty.isClosed && !RightHandProperty.clickUsed))
            {
            }
        }
    }

    private void checkHover()
    {
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            isHover = true;
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            isHover = false;
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }
}
