using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RSUnityToolkit;

public class aaaction : BaseAction
{

    public static Dictionary<string, bool> hitMap = new Dictionary<string, bool>(); //UIÔªËØÊÇ·ñ±»µã»÷map
    private bool isFist = false;

    //void Start()
    //{
    //    CleanSupportedTriggers();
    //    SetDefaultTriggers();

    //    if (SupportedTriggers != null)
    //    {
    //        for (int i = 0; i < SupportedTriggers.Length; i++)
    //        {
    //            SupportedTriggers[i].CleanRules();
    //            SetDefaultTriggerValues(i, SupportedTriggers[i]);
    //        }
    //    }
    //}

    void Update()
    {
        ProcessAllTriggers();

        var buffer = new List<string>(hitMap.Keys);
        foreach (string key in buffer)
        {
            hitMap[key] = false;
        }

        if (!isFist && SupportedTriggers[0].Success)
        {
            isFist = true;
        }

        if (isFist && SupportedTriggers[1].Success)
        {
            isFist = false;
        }

        if (!isFist)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.position - Camera.main.transform.position, out hit))
        {
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.position - Camera.main.transform.position, Color.red);
            hitMap[hit.collider.name] = true;
            Debug.Log(hit.collider.name + "is fisted");
        }
    }

    #region ŒÌ³ÐBaseActionœÓ¿ÚÖØÐÂÖÆÔì¶¯×÷Ž¥·¢œÅ±ŸÐèÒªÊµÏÖµÄº¯Êý
    /// <summary>
    /// Determines whether this instance is support custom triggers.
    /// </summary>		
    public override bool IsSupportCustomTriggers()
    {
        return true;
    }

    /// <summary>
    /// Returns the actions's description for GUI purposes.
    /// </summary>
    /// <returns>
    /// The action description.
    /// </returns>
    public override string GetActionDescription()
    {
        return "This Action check if the hand want to choose the item";
    }

    /// <summary>
    /// Sets the default trigger values (for the triggers set in SetDefaultTriggers() )
    /// </summary>
    /// <param name='index'>
    /// Index of the trigger.
    /// </param>
    /// <param name='trigger'>
    /// A pointer to the trigger for which you can set the default rules.
    /// </param>
    public override void SetDefaultTriggerValues(int index, Trigger trigger)
    {
        switch (index)
        {
            case 0:
                trigger.FriendlyName = "Start Event";
                ((EventTrigger)trigger).Rules = new BaseRule[1] { AddHiddenComponent<HandClosedRule>() };
                break;
            case 1:
                trigger.FriendlyName = "Stop Event";
                ((EventTrigger)trigger).Rules = new BaseRule[1] { AddHiddenComponent<HandOpennedRule>() };
                break;
        }
    }

    /// <summary>
    /// Sets the default triggers for that action.
    /// </summary>
    protected override void SetDefaultTriggers()
    {
        SupportedTriggers = new Trigger[2]{
        AddHiddenComponent<EventTrigger>(),
		AddHiddenComponent<EventTrigger>()};
    }

#if UNITY_EDITOR

    /// <summary>
    /// Adds the action to the RealSense Unity Toolkit menu.
    /// </summary>
    [UnityEditor.MenuItem("RealSense Unity Toolkit/Add Action/RayCheck")]
    static void AddThisAction()
    {
        AddAction<aaaction>();
    }

#endif
    #endregion

}
