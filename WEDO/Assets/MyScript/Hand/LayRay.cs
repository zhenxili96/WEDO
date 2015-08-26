using UnityEngine;
using System.Collections;

public class LayRay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
        Vector3 curPos = gameObject.transform.position;
        Vector3 target = curPos + new Vector3(0, 0, 100);
        Vector3 direction = target - curPos;  //垂直向下方式
        //Vector3 direction = curPos - Camera.main.transform.position;
        curPos.x = curPos.x + direction.normalized.x;
        curPos.y = curPos.y + direction.normalized.y;
        curPos.z = curPos.z + direction.normalized.z;
        //direction.x = direction.x * 3;
        //direction.y = direction.y * 3;
        //direction.z = direction.z * 3;
        Vector3 drawDirection = direction * 5;
        RaycastHit hit;
        if (Physics.Raycast(curPos, direction, out hit))
        {
            if (!hit.collider.name.Equals(name))
            {
                //**新增
                if (gameObject.name.Equals(LeftHandProperty.HANDNAME))
                {
                    RayHit.LeftHitName = hit.collider.name;
                    RayHit.hitName = hit.collider.name;
                    //Debug.Log(RayHit.hitName);
                    Debug.DrawRay(curPos, hit.point - curPos, Color.red);
                }
                else if (gameObject.name.Equals(RightHandProperty.HANDNAME))
                {
                    RayHit.RightHitName = hit.collider.name;
                    Debug.DrawRay(curPos, hit.point - curPos, Color.red);
                }
                else
                {
                    RayHit.hitName = hit.collider.name;
                    //Debug.Log(RayHit.hitName);
                    Debug.DrawRay(curPos, hit.point - curPos, Color.red);
                }
                //if (gameObject.name.Equals(LeftHandProperty.HANDNAME))
                //{
                //    RayHit.LeftHitName = hit.collider.name;
                //}
                //if (gameObject.name.Equals(RightHandProperty.HANDNAME))
                //{
                //    RayHit.RightHitName = hit.collider.name;
                //}
                //RayHit.hitName = hit.collider.name;
                ////Debug.Log(RayHit.hitName);
                //Debug.DrawRay(curPos, direction, Color.red);
            }
        }
	}
}
