using UnityEngine;
using System.Collections;

public class TextButton : MonoBehaviour
{

    private Color originColor;
    private bool textLock = false;
    private bool isHover = false;
    private float lockTime = 3.0f;
    private Color textOriginColor = Color.red;
    private Vector3 textOriginPos = new Vector3(-28, 0, 22);
    private int textInstanceCount = 0;
    private string TEXTPARENTNAME = "TextInstance";
    private string ROOMNPCNAME = "Room_NPC";
    private string TEXTBOARDNAME = "TextBoard";

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        if (isHover)
        {
            if ((RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed)
                || (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed))
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
                temp.transform.position = textOriginPos;
                temp.renderer.material.color = textOriginColor;
                temp.name = "TextInstance_" + textInstanceCount;
                TextMesh tempText = temp.GetComponent<TextMesh>();
                tempText.text = "hello";
                tempText.characterSize = 1.5f;
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
        }
        else
        {
            isHover = false;
            renderer.material.color = originColor;
        }
    }
}
