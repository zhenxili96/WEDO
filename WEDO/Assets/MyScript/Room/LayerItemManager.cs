using UnityEngine;
using System.Collections;

public class LayerItemManager : MonoBehaviour
{
    public GameObject layerBack = null;
    public Vector3 changePosBase = new Vector3(-40, 0, 80);
    public Vector3 originPos;
    public int selfLayer;
    public Color hoverColor = Color.red;
    public Color originColor;
    public bool isHover = false;
    public bool isDrag = false;
    public float layerZ;
    public static string LEFTHANDNAME = "LeftHand";
    public static string RIGHTHANDNAME = "RightHand";
    public float warnHight = 100f;
    public float deleteHight = 120f;
    public float minHight = -60f;

    // Use this for initialization
    void Start()
    {
        selfLayer = int.Parse(name.Remove(0, 5));
        layerBack = (GameObject)Instantiate(Resources.Load(name));
        layerBack.name = name + "_back";
        layerBack.transform.parent = gameObject.transform;
        if (RoomStatic.layerArray.Count > selfLayer)
        {
            layerZ = ((Layer)RoomStatic.layerArray[selfLayer]).ZMINPos;
            layerBack.transform.localPosition = new Vector3(layerBack.transform.position.x, layerBack.transform.position.y, layerZ);
        }
        originPos = transform.position;
        originColor = layerBack.transform.GetChild(0).renderer.material.color;
        layerBack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (RoomStatic.layerArray.Count > selfLayer)
        {
            layerZ = ((Layer)RoomStatic.layerArray[selfLayer]).ZMINPos;
            layerBack.transform.localPosition = new Vector3(layerBack.transform.position.x, layerBack.transform.position.y, layerZ);
        }
        checkHover();
        checkDrag();
        checkDelete();
    }

    private void checkDelete()
    {
        if (transform.position.y >= warnHight)
        {
            AnotherDelete.isOut = true;
        }
        if (AnotherDelete.isOpen && transform.position.y >= deleteHight)
        {
            if (!isDrag)
            {
                RoomStatic.layerArray.RemoveAt(selfLayer);
                Destroy(gameObject);
                RoomStatic.LayerCount--;
            }
        }
    }

    private void checkDrag()
    {
        if (RoomStatic.curMode == RoomMode.Mode1)
        {
            return;
        }

        isDrag = false;
        HAND dragHand = HAND.LEFTHAND;
        if (RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed)
        {
            isDrag = true;
            dragHand = HAND.LEFTHAND;
        } 
        else if (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed)
        {
            isDrag = true;
            dragHand = HAND.RIGHTHAND;
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (RayHit.LeftHitName.Equals(child.name) && LeftHandProperty.isClosed)
                {
                    isDrag = true;
                    dragHand = HAND.LEFTHAND;
                }
                else if (RayHit.RightHitName.Equals(child.name) && RightHandProperty.isClosed)
                {
                    isDrag = true;
                    dragHand = HAND.RIGHTHAND;
                }
                else
                {
                    foreach (Transform grandChild in child.transform)
                    {
                        if (RayHit.LeftHitName.Equals(grandChild.name) && LeftHandProperty.isClosed)
                        {
                            isDrag = true;
                            dragHand = HAND.LEFTHAND;
                        }
                        else if (RayHit.RightHitName.Equals(grandChild.name) && RightHandProperty.isClosed)
                        {
                            isDrag = true;
                            dragHand = HAND.RIGHTHAND;
                        }
                    }
                }
            }
        }
        if (isDrag)
        {
            switch (dragHand)
            {
                case HAND.LEFTHAND:
                    if (GameObject.Find(LEFTHANDNAME).transform.position.y >= minHight)
                    {
                        transform.position = new Vector3(transform.position.x, GameObject.Find(LEFTHANDNAME).transform.position.y,
                        transform.position.z);
                    }
                    break;
                case HAND.RIGHTHAND:
                    if (GameObject.Find(RIGHTHANDNAME).transform.position.y >= minHight)
                    {
                        transform.position = new Vector3(transform.position.x, GameObject.Find(RIGHTHANDNAME).transform.position.y,
                        transform.position.z);
                    }
                    break;
            }
        }
    }

    private void checkHover()
    {
        if (RoomStatic.curMode == RoomMode.Mode1)
        {
            return;
        }
        isHover = false;
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            isHover = true;
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (RayHit.LeftHitName.Equals(child.name) || RayHit.RightHitName.Equals(child.name))
                {
                    isHover = true;
                }
                else
                {
                    foreach (Transform grandChild in child.transform)
                    {
                        if (RayHit.LeftHitName.Equals(grandChild.name) || RayHit.RightHitName.Equals(grandChild.name))
                        {
                            isHover = true;
                        }
                    }
                }
            }
        }
        if (isHover)
        {
            layerBack.transform.GetChild(0).renderer.material.color = hoverColor;
        }
        else
        {
            layerBack.transform.GetChild(0).renderer.material.color = originColor;
        }
    }

    public void callChange()
    {
        layerBack.SetActive(true);
        transform.position = originPos + (selfLayer - 1) * changePosBase;
    }

    public void backChange()
    {
        transform.position = originPos;
        layerBack.SetActive(false);
    }
}
