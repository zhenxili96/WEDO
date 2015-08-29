using UnityEngine;
using System.Collections;

public class ColorChoose : MonoBehaviour
{
    public static bool isOut = false; //展开这个过程包括展开这个状态
    public static bool isOpen = false;  //展开这个状态
    private Vector3 outPos = new Vector3(0, 20, 40);
    private Vector3 inPos = new Vector3(0, 77, 35);
    private float inSpeed = 50f;
    private float outSpeed = 40f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isOut)
        {
            if (transform.position.y > outPos.y)
            {
                transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * outSpeed);
            }
            else
            {
                isOpen = true;
            }
        }
        else
        {
            if (transform.position.y < inPos.y)
            {
                transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * inSpeed);
            }
        }
    }

    public void open()
    {
        isOut = true;
    }
}
