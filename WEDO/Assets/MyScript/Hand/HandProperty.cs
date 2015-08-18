using UnityEngine;
using System.Collections;

public class HandProperty : MonoBehaviour
{

    public static bool isClosed = false;
    public static string HANDNAME = "HandObject";
    public static Vector3 closePos = new Vector3();
    public static bool closePosWait = true; //防止因位置还没更新而已被其他脚本使用
    private bool closePosTemp = false;  //为获得手刚闭合时的位置的中间临时变量

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //记录每次手初闭合时的位置
        if (!closePosTemp && isClosed)
        {
            closePos = gameObject.transform.position;
            closePosTemp = true;
            closePosWait = false;
        }
        if (!isClosed)
        {
            closePosTemp = false;
            closePosWait = true;
        }
    }

    public void handClosed()
    {
        isClosed = true;
        gameObject.renderer.material.color = Color.blue;
    }

    public void handOpened()
    {
        isClosed = false;
        gameObject.renderer.material.color = Color.red;
    }
}
