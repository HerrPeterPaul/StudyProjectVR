﻿using UnityEngine;

public class ChangeCameraColor : MonoBehaviour
{
    public static string CondtionTypeVariableInContainer;
    public static string CondtionTypeVariableInContainerOld;
    private GameObject cam1;
    private GameObject cam2;
    private ChangeRenderSettings cr;

    // Use this for initialization 
    private void Start ()
    {
        cam1 = GameObject.Find("RightEyeAnchor");
        cam2 = GameObject.Find("LeftEyeAnchor");

        cr = (ChangeRenderSettings)GameObject.Find("helperObject").GetComponent("ChangeRenderSettings");
    }

    // Update is called once per frame 
    private void Update ()
    {
        CondtionTypeVariableInContainer = ManagerScript.CondtionTypeVariableInContainer;
        if (CondtionTypeVariableInContainer != CondtionTypeVariableInContainerOld)
        {
            ChangeSettings();
            CondtionTypeVariableInContainerOld = CondtionTypeVariableInContainer;
        }
    }

    private void ChangeSettings ()
    {
        if (CondtionTypeVariableInContainer == "Easy" || CondtionTypeVariableInContainer == "Easy-False")
        {
            // Debug.Log("easy camera"); 
            cam1.SetActive(true);
            cam2.SetActive(true);
            cr.switchEasy();
        }
        else if (CondtionTypeVariableInContainer == "Hard" || CondtionTypeVariableInContainer == "Hard-False")
        {
            //Debug.Log("hard camera");
            cam1.SetActive(true);
            cam2.SetActive(true);

            cr.switchHard();
        }
        else if (CondtionTypeVariableInContainer == "Training" || CondtionTypeVariableInContainer == "Explain")
        {
            // Debug.Log("no cond camera"); 
            cam1.SetActive(true);
            cam2.SetActive(true);

            cr.switchNormal();
        }
        else if (CondtionTypeVariableInContainer == "ENDTRIAL")
        {
            // we need to show something in he end, so lets not disable the camera Debug.Log("no camera"); 
            cam1.SetActive(true);
            cam2.SetActive(true);

            cr.switchNormal();
        }
    }
}