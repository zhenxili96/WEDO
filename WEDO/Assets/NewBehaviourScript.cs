using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public GUISkin mySkin;
 
	void OnGUI()
	{
    	GUI.skin = mySkin;
    }
}
