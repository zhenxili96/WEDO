using UnityEngine;
using System.Collections;

public class RoomKeyBoard : MonoBehaviour
{
    public static bool isOut = false;
    private Vector3 outPos = new Vector3(0, -19, 0);
    private Vector3 inPos = new Vector3(0, -128, 0);
    private static float outSpeed = 50f;
    private static float inSpeed = 40f;

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
        if (isOut)
        {
            if (transform.position.y < outPos.y)
            {
                transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * outSpeed);
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
