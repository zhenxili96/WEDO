using UnityEngine;
using System.Collections;

public class Login_account : MonoBehaviour
{
    private bool isHover = false;
    private bool isActive = false;
    private string account = "";

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
        if (account.Length >= 20)
        {
            return;
        }
        account += ch;
        ((TextMesh)GetComponent("TextMesh")).text = account;
    }

    public void deleteChar()
    {
        if (!isActive)
        {
            return;
        }
        if (account.Length <= 0)
        {
            return;
        }
        account = account.Remove(account.Length - 1);
        ((TextMesh)GetComponent("TextMesh")).text = account;
    }

    public void loseFocus()
    {
        isActive = false;
        foreach (Transform child in transform)
        {
            if (child.name.Equals("Login_account"))
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
                if (child.name.Equals("Login_account"))
                {
                    child.renderer.material.color = Color.yellow;
                }
            }
            GameObject.Find("Login_passwordText").SendMessage("loseFocus");
        }
    }
}
