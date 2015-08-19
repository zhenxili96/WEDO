using UnityEngine;
using System.Collections;

public class deleteKey : MonoBehaviour
{

    private bool isHover = false;
    private Color originColor;
    private bool isPressed = false;

    // Use this for initialization
    void Start()
    {
        originColor = gameObject.renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        clickManager();
    }

    private void clickManager()
    {
        checkHover();
        if (isHover && HandProperty.isClosed)
        {
            renderer.material.color = Color.red;
            if (!isPressed)
            {
                isPressed = true;
                GameObject.Find("Login_accountText").SendMessage("deleteChar");
                GameObject.Find("Login_passwordText").SendMessage("deleteChar");
            }
        }
        else
        {
            isPressed = false;
            renderer.material.color = originColor;
        }
    }

    private void checkHover()
    {
        if (RayHit.hitName.Equals(name))
        {
            isHover = true;
        }
        else
        {
            isHover = false;
        }
    }
}
