using UnityEngine;
using System.Collections;

public class TextItem : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        renderer.material.color = ColorItem.curColor;
    }
}
