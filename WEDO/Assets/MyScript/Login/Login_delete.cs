using UnityEngine;
using System.Collections;

public class Login_delete : MonoBehaviour
{

    public bool isHover = false;
    public Color originColor;
    public Color hoverColor = Color.red;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 2;
    public float originZ;
    public float hoverZ;


    // Use this for initialization
    void Start()
    {
        originColor = gameObject.renderer.material.color;
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = transform.position.z;
        hoverZ = originZ - 1;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkClick();
    }

    private void checkClick()
    {
        if (isHover)
        {
            if (LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                if (Login_Key.curSentence.Length <= 0)
                {
                    return;
                }
                Login_Key.curSentence = Login_Key.curSentence.Remove(Login_Key.curSentence.Length - 1);
                LeftHandProperty.clickUsed = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                if (Login_Key.curSentence.Length <= 0)
                {
                    return;
                }
                Login_Key.curSentence = Login_Key.curSentence.Remove(Login_Key.curSentence.Length - 1);
                RightHandProperty.clickUsed = true;
            }
        }
    }

    private void checkHover()
    {
        if (Login_Keyboard.isOpen && (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name)))
        {
            isHover = true;
            renderer.material.color = Color.red;
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            isHover = false;
            renderer.material.color = originColor;
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }
}
