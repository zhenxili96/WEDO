using UnityEngine;
using System.Collections;

public class NumberBackKey : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 2;
    public float originZ;
    public float hoverZ;
    public Color originColor;
    public string KeyboardName = "Keyboard";
    public string CharKeyName = "CharKey";
    public string NumberKeyName = "NumberKey";

    // Use this for initialization
    void Start()
    {
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = transform.position.z;
        hoverZ = originZ - 1;
        originColor = renderer.material.color;
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
                GameObject.Find(KeyboardName).transform.FindChild(CharKeyName).gameObject.SetActive(true);
                GameObject.Find(KeyboardName).transform.FindChild(NumberKeyName).gameObject.SetActive(false);
                LeftHandProperty.clickUsed = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                GameObject.Find(KeyboardName).transform.FindChild(CharKeyName).gameObject.SetActive(true);
                GameObject.Find(KeyboardName).transform.FindChild(NumberKeyName).gameObject.SetActive(false);
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
