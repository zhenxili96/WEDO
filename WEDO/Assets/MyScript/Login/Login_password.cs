using UnityEngine;
using System.Collections;

public class Login_password : MonoBehaviour
{
    public bool isHover = false;
    public bool isFocus = false;
    public Color focusColor = new Color(1, 0.5412f, 0.5412f);
    public Color originColor;
    public GameObject passwordText;
    public Color passwordTextColor = Color.black;
    public static string password = "";
    public int passwordtextIndex = 0;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
        passwordText = transform.GetChild(passwordtextIndex).gameObject;
        passwordText.renderer.material.color = passwordTextColor;
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
        password = Keyboard.curSentence;
        passwordText.GetComponent<TextMesh>().text = TranslatePassword();
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
                Keyboard.curSentence = password;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                isFocus = true;
                LoginStatic.curFocus = name;
                RightHandProperty.clickUsed = true;
                Keyboard.curSentence = password;
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

    private string TranslatePassword()
    {
        string showPassword = "";
        for (int i = 0; i < password.Length; i++)
        {
            showPassword += "*";
        }
        return showPassword;
    }

}
