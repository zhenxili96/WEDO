using UnityEngine;
using System.Collections;

public class TextSort : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        sortContent();
    }

    private void sortContent(int maxCol = 15)
    {
        char [] content = GetComponent<TextMesh>().text.ToCharArray();
        string result = "";
        int tempCount = 0;
        for (int i = 0; i < content.Length; i++)
        {
            result += content[i];
            if (content[i] != '\n')
            {
                tempCount++;
            }
            if (tempCount % maxCol == 0)
            {
                result += "\n";
            }
        }
        GetComponent<TextMesh>().text = result;
    }
}
