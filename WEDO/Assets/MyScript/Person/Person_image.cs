using UnityEngine;
using System.Collections;

public class Person_image : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 1.1f;
    public float originZ;
    public float hoverZ;

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
        checkImage();
    }

    private void checkImage()
    {
        if (PersonStatic.userimage.StartsWith("userimage"))
        {
            string imagePath = "UserImage/" + PersonStatic.userimage;
            renderer.material = (Material)Resources.Load(imagePath);
        }
        else
        {
            Debug.Log(PersonStatic.userimage);
            string imagePath = "UserImage/userimage0";
            renderer.material = (Material)Resources.Load(imagePath);
        }
    }

    private void checkClick()
    {
        if (isHover)
        {
            if (LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                LeftHandProperty.clickUsed = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                transform.GetChild(0).gameObject.SetActive(true);
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
