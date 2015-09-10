using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Wedo_ClientSide;

public class ProjectionStatic : MonoBehaviour
{

    public static string curProjectionName = "";
    public static string curProjectionLeader = "";
    public static List<ClientProject> subProjects = new List<ClientProject>();
    public static int subProjectCount = 0;
    public static Vector3 UpLinePos = new Vector3(-7, 20, 65);
    public static Vector3 DownLinePos = new Vector3(-7, 0, 65);
    public static Vector3 ProjectSpace = new Vector3(17, 0, 0);
    public static Vector3 ProjRotation = new Vector3(0, 11, 0);
    public static Vector3 ProjScale = new Vector3(1, 1, 1);
    public static string SUBPROJECTPREFABNAME = "ProjectionPrefab/subprojectionprefab";
    public static string ProjectionSubItemName = "Projection_subitem";
    public static string SubProjectNameName = "subproject_name";
    public static string ProjectionNPCName = "Projection_NPC";
    public static string SubAddName = "subadd";
    public static bool isTransPage = false;

    // Use this for initialization
    void Start()
    {
        initProjectionInfo();
        initSubProjection();
        LayRay.rayStyle = RayStyle.Perspect;
        Keyboard.init();
        LeftHandProperty.HandInit();
        RightHandProperty.HandInit();
    }

    public static void reFreshProject()
    {
        initProjectionInfo();
        initSubProjection();
    }

    private static void initProjectionInfo()
    {
        curProjectionName = WholeStatic.curProject.Name;
        curProjectionLeader = WholeStatic.curProject.OwnerAccount;
        if (curProjectionLeader == null || curProjectionLeader.Equals(""))
        {
            curProjectionLeader = WholeStatic.curUser.Account;
        }
    }

    public static void addProjection(string name, ClientProject project)
    {
        subProjectCount++;
        GameObject tempProjection = (GameObject)Instantiate(Resources.Load(SUBPROJECTPREFABNAME));
        tempProjection.transform.FindChild(SubProjectNameName).GetComponent<TextMesh>().text = name;
        tempProjection.GetComponent<Projection_subproj>().projectObject = project;
        tempProjection.transform.parent = GameObject.Find(ProjectionSubItemName).transform;
        tempProjection.name = ProjectionSubItemName + "_" + name;
        tempProjection.transform.localEulerAngles = ProjRotation;
        tempProjection.transform.localScale = ProjScale;
        if (subProjectCount % 2 == 1)
        {
            tempProjection.transform.localPosition = UpLinePos + (subProjectCount - 1) * ProjectSpace;
        }
        else
        {
            tempProjection.transform.localPosition = DownLinePos + (subProjectCount/2 - 1) * ProjectSpace;
        }
    }

    private static void removeSubProj()
    {
        GameObject subItems = GameObject.Find(ProjectionSubItemName).gameObject;
        foreach (Transform child in subItems.transform)
        {
            if (child.name.Equals(SubAddName))
            {
                continue;
            }
            Destroy(child.gameObject);
        }
        subProjectCount = 0;
    }

    private static void initSubProjection()
    {
        removeSubProj();
        subProjects = ProxyInterface.Project_GetChildren(WholeStatic.curProject.Guid);
        subProjectCount = subProjects.Count;
        for (int i = 0; i < subProjectCount; i++)
        {
            GameObject tempProjection = (GameObject)Instantiate(Resources.Load(SUBPROJECTPREFABNAME));
            tempProjection.transform.FindChild(SubProjectNameName).GetComponent<TextMesh>().text = subProjects[i].Name;
            tempProjection.GetComponent<Projection_subproj>().projectObject = subProjects[i];
            tempProjection.transform.parent = GameObject.Find(ProjectionSubItemName).transform;
            tempProjection.name = ProjectionSubItemName + "_" + subProjects[i].Name;
            tempProjection.transform.eulerAngles = ProjRotation;
            tempProjection.transform.localScale = ProjScale;
            if (i % 2 == 0)
            {
                tempProjection.transform.localPosition = UpLinePos + i * ProjectSpace;
            }
            else
            {
                tempProjection.transform.localPosition = DownLinePos + i * ProjectSpace;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        if (!isTransPage)
        {
            Debug.Log("exit");
            ProxyInterface.Connect_End();
        }
        else
        {
            isTransPage = false;
        }
    }
}
