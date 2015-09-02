using UnityEngine;
using System.Collections;

public class DeleteKey : MonoBehaviour
{

    public bool isHover = false;
    public Vector3 originScale;
    public Vector3 hoverScale;
    public float scaleRate = 2;
    public float originZ;
    public float hoverZ;
    public Color originColor;
    public Color hoverColor = Color.red;

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
                if (Keyboard.curSentence.Length <= 0)
                {
                    return;
                }
                Keyboard.curSentence = Keyboard.curSentence.Remove(Keyboard.curSentence.Length - 1);
                LeftHandProperty.clickUsed = true;
            }
            if (RightHandProperty.isClosed && !RightHandProperty.clickUsed)
            {
                if (Keyboard.curSentence.Length <= 0)
                {
                    return;
                }
                Keyboard.curSentence = Keyboard.curSentence.Remove(Keyboard.curSentence.Length - 1);
                RightHandProperty.clickUsed = true;
            }
        }
    }

    private void checkHover()
    {
        if (Keyboard.isOpen && (RayHit.LeftHitName.Equals(name) || RayHit.RightHitName.Equals(name)))
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
