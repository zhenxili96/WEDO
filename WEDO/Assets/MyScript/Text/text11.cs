using UnityEngine;
using System.Collections;

public class text11 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        ((TextMesh)gameObject.GetComponent("TextMesh")).renderer.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
