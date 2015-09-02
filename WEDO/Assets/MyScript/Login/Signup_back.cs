using UnityEngine;
using System.Collections;

public class Signup_back : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 2;
    public float originZ;
    public float hoverZ;
    public string NPCNAME = "NPC";
    public string LIGINNPCNAME = "Login_NPC";
    public string SIGNNPCNAME = "Signup_NPC";

    // Use this for initialization
    void Start()
    {
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
                Keyboard.isOut = false;
                GameObject.Find(NPCNAME).transform.Find(LIGINNPCNAME).gameObject.SetActive(true);
                GameObject.Find(NPCNAME).transform.Find(SIGNNPCNAME).gameObject.SetActive(false);
                LeftHandProperty.clickUsed = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                Keyboard.isOut = false;
                GameObject.Find(NPCNAME).transform.Find(LIGINNPCNAME).gameObject.SetActive(true);
                GameObject.Find(NPCNAME).transform.Find(SIGNNPCNAME).gameObject.SetActive(false);
                RightHandProperty.clickUsed = true;
            }
        }
    }

    private void checkHover()
    {
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            isHover = true;
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            isHover = false;
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }
}
