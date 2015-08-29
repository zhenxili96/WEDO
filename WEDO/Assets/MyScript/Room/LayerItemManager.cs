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
    public float layerZ;

    // Use this for initialization
    void Start()
    {
        selfLayer = int.Parse(name.Remove(0, 5));
        layerBack = (GameObject)Instantiate(Resources.Load(name));
        layerBack.name = name + "_back";
        layerBack.transform.parent = gameObject.transform;
        if (RoomStatic.layerArray.Count > selfLayer)
        {
            layerZ = ((Layer)RoomStatic.layerArray[selfLayer]).ZMAXPos;
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
            layerZ = ((Layer)RoomStatic.layerArray[selfLayer]).ZMAXPos;
            layerBack.transform.localPosition = new Vector3(layerBack.transform.position.x, layerBack.transform.position.y, layerZ);
        }
        checkHover();
    }

    private void checkHover()
    {
        if (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name))
        {
            isHover = true;
            layerBack.transform.GetChild(0).renderer.material.color = hoverColor;
        }
        else
        {
            isHover = false;
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
