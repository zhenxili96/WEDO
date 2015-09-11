using UnityEngine;
using System.Collections;

public class ScreenShot : MonoBehaviour
{
    public string Mode1Name = "Mode1";
    public string RoomNPCName = "Room_NPC";
    public Rect shotRect;
    public bool isPrepare = false;
    public bool isShot = false;

    // Use this for initialization
    void Start()
    {
        shotRect = new Rect((Screen.width * 0.1218f), (Screen.height * 0.0896f),
            (Screen.width * 0.7335f), (Screen.height * 0.7959f));
    }

    // Update is called once per frame
    void OnPostRender()
    {
        if (isShot)
        {
            CaptureScreenshot2(shotRect);
            GameObject.Find(Mode1Name).transform.FindChild(RoomNPCName).gameObject.SetActive(true);
            isShot = false;
        }
    }

    public void ScreenShotSave()
    {
        isShot = true;
    }

    /// <summary>  
    /// Captures the screenshot2.  
    /// </summary>  
    /// <returns>The screenshot2.</returns>  
    /// <param name="rect">Rect.截图的区域，左下角为o点</param>  
    Texture2D CaptureScreenshot2(Rect rect)
    {
        // 先创建一个的空纹理，大小可根据实现需要来设置  
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);

        // 读取屏幕像素信息并存储为纹理数据，  
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();

        // 然后将这些纹理数据，成一个png图片文件  
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = Application.dataPath + "/Screenshot.png";
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("截屏了一张图片: {0}", filename));

        // 最后，我返回这个Texture2d对象，这样我们直接，所这个截图图示在游戏中，当然这个根据自己的需求的。  
        return screenShot;
    }
}
