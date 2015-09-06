using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Wedo_ClientSide;

public class HomeStatic : MonoBehaviour
{

    public static int ProjectionCount = 0;
    public static List<ClientProject> AllProjection = new List<ClientProject>();
    public static Vector3 ProjectionSpace = new Vector3(84, 0, 0);
    public static Vector3 AddbuttonPos = new Vector3(-124, 0, 0);
    public static Vector3 ProjectionRotation = new Vector3(0, 0, 0);
    public static Vector3 ProjectionScale = new Vector3(7, 7, 1);
    public static string HOMEPROJECTIONPREFABNAME = "homeprojectionprefab";
    public static string PROJBARNAME = "ProjBar";
    public static string projectnametext = "projectiontext";
    public static string projectimage = "projectionimage";
    public static string otherprojimage = "ProjectionImage/otherprojprefab";

    // Use this for initialization
    void Start()
    {
        LayRay.rayStyle = RayStyle.Ortho;
        AllProjection = ProxyInterface.Project_ByUser(WholeStatic.curUser.Guid);
        ProjectionCount = AllProjection.Count;
        initAllProjection();
        Keyboard.init();
        LeftHandProperty.HandInit();
        RightHandProperty.HandInit();
    }

    public static void addProjection(string name, ClientProject project)
    {
        ProjectionCount++;
        GameObject tempProjection = (GameObject)Instantiate(Resources.Load(HOMEPROJECTIONPREFABNAME));
        tempProjection.GetComponent<Home_project>().projectObject = project;
        tempProjection.transform.FindChild(projectnametext).gameObject.GetComponent<TextMesh>().text = name;
        tempProjection.transform.parent = GameObject.Find(PROJBARNAME).transform;
        tempProjection.name = PROJBARNAME + "_" + name;
        tempProjection.transform.position = AddbuttonPos + ProjectionCount * ProjectionSpace;
        tempProjection.transform.eulerAngles = ProjectionRotation;
        tempProjection.transform.localScale = ProjectionScale;
    }

    private void initAllProjection()
    {
        if (ProjectionCount == 0)
        {
            return;
        }
        for (int i = 0; i < ProjectionCount; i++)
        {
            GameObject tempProjection = (GameObject)Instantiate(Resources.Load(HOMEPROJECTIONPREFABNAME));
            tempProjection.transform.FindChild(projectnametext).gameObject.GetComponent<TextMesh>().text = AllProjection[i].Name;
            tempProjection.GetComponent<Home_project>().projectObject = AllProjection[i];
            if (!AllProjection[i].OwnerAccount.Equals(WholeStatic.curUser.Account))
            {
                tempProjection.transform.FindChild(projectimage).gameObject.renderer.material =
                    (Material)Instantiate(Resources.Load(otherprojimage));
            }
            tempProjection.transform.parent = GameObject.Find(PROJBARNAME).transform;
            tempProjection.name = PROJBARNAME + "_" + AllProjection[i].Name;
            tempProjection.transform.position = AddbuttonPos + (i + 1) * ProjectionSpace;
            tempProjection.transform.eulerAngles = ProjectionRotation;
            tempProjection.transform.localScale = ProjectionScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
