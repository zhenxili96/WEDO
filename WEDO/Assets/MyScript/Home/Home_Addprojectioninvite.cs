//using UnityEngine;
//using System.Collections;

//public class Home_Addprojectinvite : MonoBehaviour
//{
//    public string CancelButton = "notice_cancel";
//    public string InputLine = "notice_input";
//    public string NextButton = "notice_next";
//    public bool isHover = false;
//    public Vector3 cancelOriginScale;
//    public Vector3 cancelHoverScale;
//    public float cancelScaleRate = 1.3f;
//    public float cancelOriginZ;
//    public float cancelHoverZ;
//    public Vector3 nextOriginScale;
//    public Vector3 nextHoverScale;
//    public float nextScaleRate = 1.3f;
//    public float nextOriginZ;
//    public float nextHoverZ;
//    public Vector3 inPos = new Vector3(-57, 49, 30);
//    public Vector3 outPos = new Vector3(-57, -6, 30);
//    public float inSpeed = 60;
//    public float outSpeed = 70;
//    public bool isOut = true;
//    public GameObject noticeTextObject;
//    public static bool isAwake = false;

//    // Use this for initialization
//    void Start()
//    {
//        isAwake = true;
//    }

//    //每次唤醒时调用一次，唤醒后立即关闭
//    private void checkAwake()
//    {
//        if (!isAwake)
//        {
//            return;
//        }
//        isAwake = false;
//        cancelOriginScale = GameObject.Find(CancelButton).transform.localScale;
//        cancelHoverScale = cancelScaleRate * cancelOriginScale;
//        cancelOriginZ = GameObject.Find(CancelButton).transform.position.z;
//        cancelHoverZ = cancelHoverZ - 1;
//        nextOriginScale = GameObject.Find(NextButton).transform.localScale;
//        nextHoverScale = nextScaleRate * nextOriginScale;
//        nextOriginZ = GameObject.Find(NextButton).transform.position.z;
//        nextHoverZ = nextOriginZ - 1;
//        noticeTextObject = transform.FindChild(InputLine).GetChild(0).gameObject;
//        Keyboard.init();
//        Keyboard.curSentence = "";
//        Keyboard.isOut = true;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        checkAwake();
//        checkCancelClick();
//        checkNextClick();
//        checkNextHover();
//        checkCancelHover();
//        checkOut();
//        checkInput();
//    }

//    private void checkInput()
//    {
//        noticeTextObject.GetComponent<TextMesh>().text = Keyboard.curSentence;
//    }

//    private void checkOut()
//    {
//        if (!Keyboard.isOut)
//        {
//            isOut = false;
//        }
//        if (isOut)
//        {
//            if (transform.position.y > outPos.y)
//            {
//                transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * outSpeed);
//            }
//        }
//        else
//        {
//            if (transform.position.y < inPos.y)
//            {
//                transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * inSpeed);
//            }
//        }
//    }

//    private void checkCancelClick()
//    {
//        if ((RayHit.LeftHitName.Equals(CancelButton) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
//            || (RayHit.RightHitName.Equals(CancelButton) && RightHandProperty.isClosed && !RightHandProperty.clickUsed))
//        {
//            //transform.parent.GetComponent<Home_Addproject>().InWake();
//            transform.parent.gameObject.SetActive(false);
//        }
//    }

//    private void checkCancelHover()
//    {
//        if (RayHit.LeftHitName.Equals(CancelButton) || RayHit.RightHitName.Equals(CancelButton))
//        {
//            GameObject.Find(CancelButton).transform.localScale = cancelHoverScale;
//            GameObject.Find(CancelButton).transform.position = new Vector3(GameObject.Find(CancelButton).transform.position.x,
//                GameObject.Find(CancelButton).transform.position.y, cancelHoverZ);
//        }
//        else
//        {
//            GameObject.Find(CancelButton).transform.localScale = cancelOriginScale;
//            GameObject.Find(CancelButton).transform.position = new Vector3(GameObject.Find(CancelButton).transform.position.x,
//                GameObject.Find(CancelButton).transform.position.y, cancelOriginZ);
//        }
//    }

//    private void checkNextClick()
//    {
//        if ((RayHit.LeftHitName.Equals(NextButton) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
//            || (RayHit.RightHitName.Equals(NextButton) && RightHandProperty.isClosed && !RightHandProperty.clickUsed))
//        {
//            //transform.parent.FindChild(Addproject_invite).gameObject.SetActive(true);
//            gameObject.SetActive(false);
//        }
//    }

//    private void checkNextHover()
//    {
//        if (RayHit.LeftHitName.Equals(NextButton) || RayHit.RightHitName.Equals(NextButton))
//        {
//            GameObject.Find(NextButton).transform.localScale = nextHoverScale;
//            GameObject.Find(NextButton).transform.position = new Vector3(GameObject.Find(NextButton).transform.position.x,
//                GameObject.Find(NextButton).transform.position.y, nextHoverZ);
//        }
//        else
//        {
//            GameObject.Find(NextButton).transform.localScale = nextOriginScale;
//            GameObject.Find(NextButton).transform.position = new Vector3(GameObject.Find(NextButton).transform.position.x,
//                GameObject.Find(NextButton).transform.position.y, nextOriginZ);
//        }
//    }
//}
