using UnityEngine;
using System.Collections;

public class TextButton : MonoBehaviour
{

    private Color originColor;
    private bool textLock = false;
    private bool isHover = false;
    private float lockTime = 3.0f;
    private Vector3 textOriginPos = new Vector3(-70, 1, 22);
    private int textInstanceCount = 0;
    private string TEXTPARENTNAME = "TextInstance";
    private string ROOMNPCNAME = "Room_NPC";
    private string TEXTBOARDNAME = "TextBoard";
    private Vector3 originScale;
    private Vector3 hoverScale;
    private float scaleRate = 2;
    private float originZ;
    private float hoverZ;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = transform.position.z;
        hoverZ = originZ - 1;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        if (isHover)
        {
            if ((RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
                || (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed))
            {
                if (textLock)
                {
                    return;
                }
                else
                {
                    textLock = true;
                    Invoke("textLockRelease", lockTime);
                }
                if ((RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed))
                {
                    LeftHandProperty.clickUsed = true;
                }
                else if ((RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed))
                {
                    RightHandProperty.clickUsed = true;
                }
                textInstanceCount++;
                //GameObject.Find(ROOMNPCNAME).transform.Find(TEXTBOARDNAME).gameObject.SetActive(true);
                GameObject temp = (GameObject)Instantiate(Resources.Load("textPrefab"));
                temp.transform.parent = GameObject.Find(TEXTPARENTNAME).transform;
                temp.name = "TextInstance_" + textInstanceCount;
                GameObject tempChild = temp.transform.GetChild(0).gameObject;
                tempChild.transform.position = textOriginPos;
                TextMesh tempText = tempChild.GetComponent<TextMesh>();
                tempText.text = "Hello world";
                tempText.fontSize = 100;
                Keyboard.isOut = true;
            }
        }
    }

    private void textLockRelease()
    {
        textLock = false;
    }

    private void checkHover()
    {
        if (MenuBar.isOut && RayHit.hitName.Equals(name))
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
