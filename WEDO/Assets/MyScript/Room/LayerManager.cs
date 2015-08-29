using UnityEngine;
using System.Collections;

public class LayerManager : MonoBehaviour
{

    public Vector3 changeRotate = new Vector3(0, 320, 0);
    public Vector3 originRotate;
    public Vector3 changeScale = new Vector3(0.5f, 0.5f, 1);
    public Vector3 originScale;
    public Vector3 changePos = new Vector3(110, 0, 0);
    public Vector3 originPos;


    // Use this for initialization
    void Start()
    {
        originRotate = transform.eulerAngles;
        originScale = transform.localScale;
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void callChange()
    {
        transform.eulerAngles = changeRotate;
        transform.localScale = changeScale;
        transform.position = changePos;
    }

    public void backChange()
    {
        transform.eulerAngles = originRotate;
        transform.localScale = originScale;
        transform.position = originPos;
    }
}
