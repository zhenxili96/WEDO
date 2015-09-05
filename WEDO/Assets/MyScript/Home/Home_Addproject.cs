using UnityEngine;
using System.Collections;
using Wedo_ClientSide;

public class Home_Addproject : MonoBehaviour
{
    public string CancelButton = "cancelbutton";
    public string InputLine = "inputline";
    public string ConfirmButton = "confirmbutton";
    public Vector3 cancelOriginScale;
    public Vector3 cancelHoverScale;
    public float cancelScaleRate = 1.3f;
    public float cancelOriginZ;
    public float cancelHoverZ;
    public Vector3 confirmOriginScale;
    public Vector3 confirmHoverScale;
    public float confirmScaleRate = 1.3f;
    public float confirmOriginZ;
    public float confirmHoverZ;
    public Vector3 inPos = new Vector3(-57, 65, -20);
    public Vector3 outPos = new Vector3(-57, 10, -20);
    public float inSpeed = 60;
    public float outSpeed = 70;
    public static bool isOut = true;
    public GameObject nameTextObject;
    public string name = "";
    public string HomeNPCName = "Home_NPC";

    // Use this for initialization
    void Start()
    {
        cancelOriginScale = GameObject.Find(CancelButton).transform.localScale;
        cancelHoverScale = cancelScaleRate * cancelOriginScale;
        cancelOriginZ = GameObject.Find(CancelButton).transform.position.z;
        cancelHoverZ = cancelHoverZ - 1;
        confirmOriginScale = GameObject.Find(ConfirmButton).transform.localScale;
        confirmHoverScale = confirmScaleRate * confirmOriginScale;
        confirmOriginZ = GameObject.Find(ConfirmButton).transform.position.z;
        confirmHoverZ = confirmOriginZ - 1;
        nameTextObject = transform.FindChild(InputLine).GetChild(0).gameObject;
        Keyboard.curSentence = "";
        Keyboard.isOut = true;
        isOut = false;
    }


    // Update is called once per frame
    void Update()
    {
        checkCancelClick();
        checkConfirmClick();
        checkConfirmHover();
        checkCancelHover();
        checkOut();
        checkInput();
    }

    private void checkInput()
    {
        nameTextObject.GetComponent<TextMesh>().text = Keyboard.curSentence;
        name = Keyboard.curSentence;
    }

    private void checkOut()
    {
        if (!Keyboard.isOut)
        {
            isOut = true;
        }
        if (isOut)
        {
            if (transform.position.y > outPos.y)
            {
                transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * outSpeed);
            }
        }
        else
        {
            if (transform.position.y < inPos.y)
            {
                transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * inSpeed);
            }
        }
    }

    private void checkCancelClick()
    {
        if ((RayHit.LeftHitName.Equals(CancelButton) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            || (RayHit.RightHitName.Equals(CancelButton) && RightHandProperty.isClosed && !RightHandProperty.clickUsed))
        {
            gameObject.SetActive(false);
        }
    }

    private void checkCancelHover()
    {
        if (RayHit.LeftHitName.Equals(CancelButton) || RayHit.RightHitName.Equals(CancelButton))
        {
            transform.FindChild(CancelButton).transform.localScale = cancelHoverScale;
            transform.FindChild(CancelButton).transform.position = new Vector3(transform.FindChild(CancelButton).transform.position.x,
                transform.FindChild(CancelButton).transform.position.y, cancelHoverZ);
        }
        else
        {
            transform.FindChild(CancelButton).transform.localScale = cancelOriginScale;
            transform.FindChild(CancelButton).transform.position = new Vector3(transform.FindChild(CancelButton).transform.position.x,
                transform.FindChild(CancelButton).transform.position.y, cancelOriginZ);
        }
    }

    private void checkConfirmClick()
    {
        if ((RayHit.LeftHitName.Equals(ConfirmButton) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            || (RayHit.RightHitName.Equals(ConfirmButton) && RightHandProperty.isClosed && !RightHandProperty.clickUsed))
        {
            ClientProject tempProject = ProxyInterface.Project_Create(WholeStatic.curUser.Guid, name);
            if (tempProject == null)
            {
                AttentionStatic.callAttention(HomeNPCName, "新建项目失败！");
            }
            else
            {
                Debug.Log(tempProject.CreateTime);
                Debug.Log(tempProject.Guid);
                Debug.Log(tempProject.Name);
                Debug.Log(tempProject.OwnerAccount);
                Debug.Log(tempProject.OwnerAvatar);
                Debug.Log(tempProject.OwnerGuid);
                HomeStatic.addProjection(name);
            }
            gameObject.SetActive(false);
        }
    }

    private void checkConfirmHover()
    {
        if (RayHit.LeftHitName.Equals(ConfirmButton) || RayHit.RightHitName.Equals(ConfirmButton))
        {
            transform.FindChild(ConfirmButton).transform.localScale = confirmHoverScale;
            transform.FindChild(ConfirmButton).transform.position = new Vector3(transform.FindChild(ConfirmButton).transform.position.x,
                transform.FindChild(ConfirmButton).transform.position.y, confirmHoverZ);
        }
        else
        {
            transform.FindChild(ConfirmButton).transform.localScale = confirmOriginScale;
            transform.FindChild(ConfirmButton).transform.position = new Vector3(transform.FindChild(ConfirmButton).transform.position.x,
                transform.FindChild(ConfirmButton).transform.position.y, confirmOriginZ);
        }
    }
}
