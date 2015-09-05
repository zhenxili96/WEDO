using UnityEngine;
using System.Collections;

public class AttentionStatic
{
    public static string AttentionName = "Attention";
    public static string AttentionTextName = "attentiontext";

    public static void callAttention(string parentName, string content)
    {
        GameObject attentionObject = GameObject.Find(parentName).transform.Find(AttentionName).gameObject;
        attentionObject.SetActive(true);
        attentionObject.transform.Find(AttentionTextName).GetComponent<TextMesh>().text = content;
    }
}
