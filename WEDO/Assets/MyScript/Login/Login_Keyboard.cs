using UnityEngine;
using System.Collections;

public class Login_Keyboard : MonoBehaviour
{

    private float upSpeed = 20f;
    private float downSpeed = 30f;
    public Vector3 outPos = new Vector3(-18, 11, -210);
    public Vector3 inPos = new Vector3(-18, -100, -210);
    public float outSpeed = 60f;
    public float inSpeed = 70f;
    public static bool isOut = false;
    public static bool isOpen = false;


    private float distance = 11;
    private float topBorder = 0;
    private float bottomBorder = -26;
    private bool moveUp = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkOut();
    }

    private void checkOut()
    {
        isOpen = false;
        if (isOut)
        {
            if (transform.position.y < outPos.y)
            {
                transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * outSpeed);
            }
            else
            {
                isOpen = true;
            }
        }
        else
        {
            if (transform.position.y > inPos.y)
            {
                transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * inSpeed);
            }
        }
    }
}
