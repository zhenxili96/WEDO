using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RayHit : MonoBehaviour {

    //public static Dictionary<string, bool> hitMap = new Dictionary<string, bool>();//当前被点击对象缓存
    public static string hitName = ""; //当前被点击对象名称
    public static string LeftHitName = "";  //当前被左手点击对象名称
    public static string RightHitName = ""; //当前被右手点击对象名称

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        Vector3 curPos = gameObject.transform.position;
        Vector3 target = curPos + new Vector3(0, 0, 100);
        Vector3 direction = target - curPos;
        RaycastHit hit;
        Physics.Raycast(curPos, direction, out hit);
        Debug.DrawRay(curPos, direction, Color.red);

	}
}
