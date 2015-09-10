using UnityEngine;
using System.Collections;

public class Login_account : MonoBehaviour
{
    public bool isHover = false;
    public bool isFocus = false;
    public Color focusColor = new Color(1, 0.5412f, 0.5412f);
    public Color originColor;
    public GameObject accountText;
    public Color accountTextColor = Color.black;
    public static string account = "";
    public int accounttextIndex = 0;


    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
        accountText = transform.GetChild(accounttextIndex).gameObject;
        accountText.renderer.material.color = accountTextColor;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        if (isFocus)
        {
            renderer.material.color = focusColor;
        }
        checkFocus();
        checkContent();
    }

    private void checkContent()
    {
        if (!isFocus)
        {
            return;
        }
        account = Keyboard.curSentence;
        accountText.GetComponent<TextMesh>().text = account;
    }

    private void checkFocus()
    {
        isFocus = false;
        if (LoginStatic.curFocus.Equals(name))
        {
            isFocus = true;
        }
        else if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            if (LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                isFocus = true;
                LoginStatic.curFocus = name;
                LeftHandProperty.clickUsed = true;
                Keyboard.curSentence = account;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                isFocus = true;
                LoginStatic.curFocus = name;
                RightHandProperty.clickUsed = true;
                Keyboard.curSentence = account;
            }
        }
    }

    private void checkHover()
    {
        isHover = false;

        if (RayHit.LeftHitName.Equals(name) && !LeftHandProperty.isClosed
            || RayHit.RightHitName.Equals(name) && !RightHandProperty.isClosed)
        {
            isHover = true;
            if (!isFocus)
            {
            }
        }
        else
        {
            isHover = false;
            if (!isFocus)
            {
                renderer.material.color = originColor;
            }
        }
    }

}
