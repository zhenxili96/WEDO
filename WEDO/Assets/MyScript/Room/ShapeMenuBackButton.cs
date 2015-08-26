using UnityEngine;
using System.Collections;

public class ShapeMenuBackButton : MonoBehaviour
{

    private bool isHover = false;
    private Color originColor;
    private Color hoverColor = Color.red;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
