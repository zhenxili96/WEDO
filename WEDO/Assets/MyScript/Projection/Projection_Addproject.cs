using UnityEngine;
using System.Collections;
using Wedo_ClientSide;

public class Projection_Addproject : MonoBehaviour
{
    public string CancelButton = "cancelbutton";
    public string InputLine = "inputline";
    public string ConfirmButton = "confirmbutton";

    public Vector3 confirmOriginScale;
    public Vector3 confirmHoverScale;
    public float confirmScaleRate = 1.3f;
    public float confirmOriginZ;
    public float confirmHoverZ;
    public GameObject confirmbutton;

    public Vector3 cancelOriginScale;
    public Vector3 cancelHoverScale;
    public float cancelScaleRate = 1.3f;
    public float cancelOriginZ;
    public float cancelHoverZ;
    public GameObject cancelbutton;

    public GameObject nameTextObject;
    public string name = "";
    public string ProjectionNPCName = "";

    // Use this for initialization
    void Start()
    {
        confirmbutton = transform.FindChild(ConfirmButton).gameObject;
        confirmOriginScale = confirmbutton.transform.localScale;
        confirmHoverScale = confirmScaleRate * confirmOriginScale;
        confirmOriginZ = confirmbutton.transform.position.z;
        confirmHoverZ = confirmOriginZ - 1;

        cancelbutton = transform.FindChild(CancelButton).gameObject;
        cancelOriginScale = cancelbutton.transform.localScale;
        cancelHoverScale = cancelScaleRate * cancelOriginScale;
        cancelOriginZ = cancelbutton.transform.position.z;
        cancelHoverZ = cancelOriginZ - 1;

        nameTextObject = transform.FindChild(InputLine).GetChild(0).gameObject;
        Keyboard.curSentence = "";
        Keyboard.isOut = true;
    }


    // Update is called once per frame
    void Update()
    {
        checkCancelClick();
        checkConfirmClick();
        checkConfirmHover();
        checkCancelHover();
        checkInput();
    }

    private void checkInput()
    {
        nameTextObject.GetComponent<TextMesh>().text = Keyboard.curSentence;
        name = Keyboard.curSentence;
    }

    private void checkCancelClick()
    {
        if ((RayHit.LeftHitName.Equals(CancelButton) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            || (RayHit.RightHitName.Equals(CancelButton) && RightHandProperty.isClosed && !RightHandProperty.clickUsed))
        {
            gameObject.SetActive(false);
            Keyboard.isOut = false;
        }
    }

    private void checkConfirmClick()
    {
        if ((RayHit.LeftHitName.Equals(ConfirmButton) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
            || (RayHit.RightHitName.Equals(ConfirmButton) && RightHandProperty.isClosed && !RightHandProperty.clickUsed))
        {
            ClientProject tempProject = ProxyInterface.Project_Create(WholeStatic.curUser.Guid, name, WholeStatic.curProject.Guid);
            if (tempProject == null)
            {
                AttentionStatic.callAttention(ProjectionNPCName, "新建项目失败！");
            }
            else
            {
                Debug.Log(tempProject.CreateTime);
                Debug.Log(tempProject.Guid);
                Debug.Log(tempProject.Name);
                Debug.Log(tempProject.OwnerAccount);
                Debug.Log(tempProject.OwnerAvatar);
                Debug.Log(tempProject.OwnerGuid);
                ProjectionStatic.addProjection(name, tempProject);
            }
            gameObject.SetActive(false);
        }
    }

    private void checkConfirmHover()
    {
        if (RayHit.LeftHitName.Equals(ConfirmButton) || RayHit.RightHitName.Equals(ConfirmButton))
        {
            confirmbutton.transform.localScale = confirmHoverScale;
            confirmbutton.transform.position = new Vector3(confirmbutton.transform.position.x,
                confirmbutton.transform.position.y, confirmHoverZ);
        }
        else
        {
            confirmbutton.transform.localScale = confirmOriginScale;
            confirmbutton.transform.position = new Vector3(confirmbutton.transform.position.x,
                confirmbutton.transform.position.y, confirmOriginZ);
        }
    }

    private void checkCancelHover()
    {
        if (RayHit.LeftHitName.Equals(CancelButton) || RayHit.RightHitName.Equals(CancelButton))
        {
            cancelbutton.transform.localScale = cancelHoverScale;
            cancelbutton.transform.position = new Vector3(cancelbutton.transform.position.x,
                cancelbutton.transform.position.y, cancelHoverZ);
        }
        else
        {
            cancelbutton.transform.localScale = cancelOriginScale;
            cancelbutton.transform.position = new Vector3(cancelbutton.transform.position.x,
                cancelbutton.transform.position.y, cancelOriginZ);
        }
    }

}
