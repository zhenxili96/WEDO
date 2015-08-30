using UnityEngine;
using System.Collections;

public class AnotherDelete : MonoBehaviour
{

    public Vector3 inPos = new Vector3(-90, 105, -90);
    public Vector3 outPos = new Vector3(-90, 80, -90);
    public float inSpeed = 30f;
    public float outSpeed = 20f;
    public static bool isOut = false;
    public static bool isOpen = false;

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

        }
    }
}
