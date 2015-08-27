using UnityEngine;
using System.Collections;

public class DeleteButton : MonoBehaviour
{

    public static bool isOut = false;
    public static bool isPrepare = false;
    public static bool isOpen = false;
    private static Vector3 outPos = new Vector3(0, 85, 50);
    private static Vector3 inPos = new Vector3(0, 120, 50);
    private static float inSpeed = 1.5f;
    private static float outSpeed = 1.2f;
    private Color originColor;
    private Color prepareColor = Color.red;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        checkOut();
        checkPrepare();
    }

    private void checkPrepare()
    {
        if (isPrepare)
        {
            renderer.material.color = prepareColor;
        }
        else
        {
            renderer.material.color = originColor;
        }
    }

    private void checkOut()
    {
        isOpen = false;
        if (isOut)
        {
            if (transform.position.y > outPos.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - outSpeed, transform.position.z);
            }
            else
            {
                isOpen = true;
            }
        }
        else
        {
            renderer.material.color = originColor;
            if (transform.position.y < inPos.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + inSpeed, transform.position.z);
            }
        }
        if (!isOpen)
        {
            isPrepare = false;
        }
        //Debug.Log(isOut);
    }
}
