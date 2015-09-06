using UnityEngine;
using System.Collections;

public class message_record : MonoBehaviour
{

    public bool isHover = false;
    public bool isOpen = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.3f;
    public float originZ;
    public float hoverZ;
    public string normalImagePath = "RoomMessage/recordnormalprefab";
    public string pressImagePath = "RoomMessage/recordpressprefab";
    public GameObject recordObject;
    public string recordObjectPath = "RoomMessage/recordprefab";
    public Vector3 firstPos = new Vector3(-43.3f, 3.5f, 20);
    public Vector3 recordSpace = new Vector3(0, -2.5f, 0);
    public Vector3 recordScale = new Vector3(0.18f, 0.18f, 0.18f);
    public Vector3 recordRotate = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start()
    {
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = transform.position.z;
        hoverZ = originZ - 1;
    }

    public void initRecord()
    {
        recordObject = transform.GetChild(0).gameObject;
        for (int i = 0; i < RoomStatic.recordCount; i++)
        {
            GameObject tempObject = (GameObject)Instantiate(Resources.Load(recordObjectPath));
            tempObject.name = "record_" + i;
            foreach (Transform child in tempObject.transform)
            {
                child.name = tempObject.name + "_" + child.name;
            }
            tempObject.transform.FindChild(tempObject.name + "_content").GetComponent<TextMesh>().text
                = RoomStatic.records[i].Cont;
            tempObject.transform.parent = recordObject.transform;
            tempObject.transform.localPosition = firstPos + i * recordSpace;
            tempObject.transform.eulerAngles = recordRotate;
            tempObject.transform.localScale = recordScale;
        }
    }


    // Update is called once per frame
    void Update()
    {
        checkHover();
        checkClick();
        checkOpen();
    }

    private void checkOpen()
    {
        if (MessageBar.curFocus.Equals(name) && MessageBar.isOpen)
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }
        if (isOpen)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void checkClick()
    {
        if (isHover)
        {
            if (LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                LeftHandProperty.clickUsed = true;
                MessageBar.curFocus = name;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                RightHandProperty.clickUsed = true;
                MessageBar.curFocus = name;
            }
        }
    }

    private void checkHover()
    {
        if (MessageBar.isOpen && (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name)))
        {
            isHover = true;
        }
        else
        {
            isHover = false;
        }
        if (isHover && !isOpen)
        {
            renderer.material = (Material)Resources.Load(pressImagePath);
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            renderer.material = (Material)Resources.Load(normalImagePath);
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }
}
