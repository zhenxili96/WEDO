using UnityEngine;
using System.Collections;

public class RightHandProperty : MonoBehaviour
{

    public static bool isClosed = false;
    public static bool clickUsed = false;   //每次手势闭合只能被用一次 
    public static string HANDNAME = "RightHand";
    public static Vector3 closePos = new Vector3();
    public static bool closePosWait = true; //防止因位置还没更新而已被其他脚本使用
    private bool closePosTemp = false;  //为获得手刚闭合时的位置的中间临时变量
    public Material handOpenMaterial;
    public string handOpenMaterialName = "BlackRightHand";
    public Material handCloseMaterial;
    public string handCloseMaterialName = "BlackRightHandFist";
    public static Vector3 curPos;
    public static bool isShow = false;

    // Use this for initialization
    void Start()
    {
        handOpenMaterial = (Material)Instantiate(Resources.Load(handOpenMaterialName));
        handCloseMaterial = (Material)Instantiate(Resources.Load(handCloseMaterialName));
        renderer.material = handOpenMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        curPos = transform.position;
        //记录每次手初闭合时的位置
        if (!closePosTemp && isClosed)
        {
            closePos = gameObject.transform.position;
            closePosTemp = true;
            closePosWait = false;
            clickUsed = false;
        }
        if (!isClosed)
        {
            closePosTemp = false;
            closePosWait = true;
        }
    }

    public static void HandInit()
    {
        isClosed = false;
    }

    public void handClosed()
    {
        isClosed = true;
        renderer.material = handCloseMaterial;
    }

    public void handOpened()
    {
        isClosed = false;
        renderer.material = handOpenMaterial;
    }
}
