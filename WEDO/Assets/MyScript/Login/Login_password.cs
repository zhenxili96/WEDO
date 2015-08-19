using UnityEngine;
using System.Collections;

public class Login_password : MonoBehaviour
{
    private bool isHover = false;
    private bool isActive = false;
    private string password = "";

    // Use this for initialization
    void Start()
    {
        gameObject.renderer.material.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        checkActive();
    }

    public void receiveChar(string ch)
    {
        if (!isActive)
        {
            return;
        }
        if (password.Length >= 20)
        {
            return;
        }
        password += ch;
        ((TextMesh)GetComponent("TextMesh")).text = TranslatePassword();
    }

    public void deleteChar()
    {
        if (!isActive)
        {
            return;
        }
        if (password.Length <= 0)
        {
            return;
        }
        password = password.Remove(password.Length - 1);
        ((TextMesh)GetComponent("TextMesh")).text = TranslatePassword();
    }

    public void loseFocus()
    {
        isActive = false;
        foreach (Transform child in transform)
        {
            if (child.name.Equals("Login_password"))
            {
                child.renderer.material.color = Color.white;
            }
        }
    }

    private void checkActive()
    {
        isHover = false;
        if (RayHit.hitName.Equals(name))
        {
            isHover = true;
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (RayHit.hitName.Equals(child.name))
                {
                    isHover = true;
                }
            }
        }

        if (isHover && HandProperty.isClosed)
        {
            isActive = true;
            foreach (Transform child in transform)
            {
                if (child.name.Equals("Login_password"))
                {
                    child.renderer.material.color = Color.yellow;
                }
            }
            GameObject.Find("Login_accountText").SendMessage("loseFocus");
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
