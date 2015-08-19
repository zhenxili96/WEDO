using UnityEngine;
using System.Collections;

public class Login_Keyboard : MonoBehaviour
{

    private float upSpeed = 20f;
    private float downSpeed = 30f;
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
        checkMove();
    }

    private void checkMove()
    {
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

    public void setMoveUp()
    {
        moveUp = true;
    }

    public void setMoveDown()
    {
        moveUp = false;
    }
}
