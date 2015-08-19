using UnityEngine;
using System.Collections;

public class Login_NPC : MonoBehaviour
{

    private float upSpeed = 16f;
    private float downSpeed = 20f;
    private float distance = 11;
    private float topBorder = 11;
    private float bottomBorder = 0;
    private bool isHover = false;
    private bool moveUp = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        checkMove();
    }

    private void checkMove()
    {
        checkHover();
        if (moveUp)
        {
            if (gameObject.transform.position.y <= topBorder)
            {
                transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * upSpeed);
            }
        }
        else
        {
            if (gameObject.transform.position.y >= bottomBorder)
            {
                transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * downSpeed);
            }
        }
    }

    private void checkHover()
    {
        isHover = false;
        foreach (Transform child in transform)
        {
            if (RayHit.hitName.Equals(child.name))
            {
                isHover = true;
            }
        }
        if (isHover)
        {
            moveUp = true;
            GameObject.Find("Keyboard").SendMessage("setMoveUp");
        }
    }

    public void setMoveDown()
    {
        moveUp = false;
    }
}
