using UnityEngine;
using System.Collections;

public class TextButton : MonoBehaviour
{

    public bool textLock = false;
    public bool isHover = false;
    public float lockTime = 3.0f;
    public Vector3 initPos = new Vector3(0, 0, 23);
    public Vector3 initScale = new Vector3(1, 1, 1);
    public Vector3 initRotate = new Vector3(0, 0, 0);
    public int textInstanceCount = 0;
    public string TEXTPARENTNAME = "TextInstance";
    public string ROOMNPCNAME = "Room_NPC";
    public string TEXTBOARDNAME = "TextBoard";
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 2;
    public float originZ;
    public float hoverZ;
    public Color originColor;
    public Color hoverColor = new Color(1, 0.5412f, 0.5412f);

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
        checkClick();
    }

    private void checkClick()
    {
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
                //GameObject temp = (GameObject)Instantiate(Resources.Load("textPrefab"));
                //temp.transform.parent = GameObject.Find(TEXTPARENTNAME).transform;
                //temp.name = "TextInstance_" + textInstanceCount;
                //GameObject tempChild = temp.transform.GetChild(0).gameObject;
                //tempChild.transform.position = textOriginPos;
                //TextMesh tempText = tempChild.GetComponent<TextMesh>();
                //tempText.text = "Hello world";
                //tempText.fontSize = 100;
                WholeStatic.curRoomInterface.AddBoardMaterial(
                    WholeStatic.curRoomInterface.RoomLayers[RoomStatic.curLayer - 1].NowLayer.Guid,
                            initPos.x, initPos.y, initPos.z,
                            initScale.x, initScale.y, initScale.z,
                            initRotate.x, initRotate.y, initRotate.z,
                            "C7", RoomStatic.TEXT, "helloworld", 40, "UNSET");
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
        if (MenuBar.isOut && (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name)))
        {
            isHover = true;
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
            renderer.material.color = hoverColor;
        }
        else
        {
            isHover = false;
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
            renderer.material.color = originColor;
        }
    }
}
