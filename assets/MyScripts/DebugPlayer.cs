﻿// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : razial
// Created          : 12-29-2014
//
// Last Modified By : razial
// Last Modified On : 01-07-2015
// ***********************************************************************
// <copyright file="DebugPlayer.cs" company="INLUSIO">
//     Copyright (c) INLUSIO. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;

/// <summary>
/// Class DebugPlayer.
/// </summary>
public class DebugPlayer : MonoBehaviour
{

    /// <summary>
    /// The start point coordinates
    /// </summary>
    Transform StartPointCoordinates;
    /// <summary>
    /// The urand
    /// </summary>
    private UnityRandom urand;
    /// <summary>
    /// The responce time
    /// </summary>
    float responceTime;
    /// <summary>
    /// The react after
    /// </summary>
    float reactAfter;
    /// <summary>
    /// The temp1
    /// </summary>
    float temp1;
    /// <summary>
    /// The counter
    /// </summary>
    bool counter = false;
    /// <summary>
    /// The blue ball position
    /// </summary>
    Transform BlueBallPosition;
    /// <summary>
    /// The defetablemassage
    /// </summary>
    bool defetablemassage = true;
    // Use this for initialization

    // real rinnning or just quick jumst to end
    /// <summary>
    /// The real player or not
    /// </summary>
    public	bool RealPlayerOrNot = false;


    /// <summary>
    /// Starts this instance.
    /// </summary>
    void Start ()
    {

        StartPointCoordinates = GameObject.Find("StartPoint").transform;
        urand = new UnityRandom((int)System.DateTime.Now.Ticks);
      
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    void Update ()
    {

        // We need to unpause the game
        if (ManagerScript.getState() == ManagerScript.states.pause || ManagerScript.getState() == ManagerScript.states.blockover)
        {
            Invoke("Unpause", 8.5f);
            
        }


        if (ManagerScript.getState() != ManagerScript.states.startScreen && ManagerScript.getState() != ManagerScript.states.end)
        {






            // move player forward
            if (ManagerScript.getState() == ManagerScript.states.walking)
            {

                BlueBallPosition = GameObject.Find("WaypointBlue").transform;

                temp1 = Stressor.GetSpeedMoveScale();

                transform.position = Vector3.MoveTowards(transform.position, BlueBallPosition.position, (float)(temp1 * Time.deltaTime * 5f));
                transform.LookAt(BlueBallPosition); // lets allways face the blue ball 


            }

            // is we are in the stressor defeatable mode than we ca defeat it

            if (((Stressor)(GameObject.Find("StressorYellow").GetComponent("Stressor"))).GetYellowState() == Stressor.yellowSphereStates.defeatable && defetablemassage)
            {
                defetablemassage = false;
                GenrataTimeForDebugPlayerResponceDeley();

            }

            // to prevent it from defetting it multiple times, this is needed 

            if (((Stressor)(GameObject.Find("StressorYellow").GetComponent("Stressor"))).GetYellowState() != Stressor.yellowSphereStates.defeatable)
            {
                defetablemassage = true;
            }


            // if a sphere is in the defeatable mode, generate a random time with a function (due to some fucked up shit, the yellow spehre will do it ...)


            // lets point 
            if (ManagerScript.getState() == ManagerScript.states.pointing)
            {
                transform.LookAt(StartPointCoordinates);

                int temp2 = UnityEngine.Random.Range(1, 2);

                switch (temp2)
                {
                // the jidder should be around 5 to 15 degree in total, so we dont have so many conditions
                // lets try it with 10 degree in total
                    case 1:

                        transform.Rotate(0, 90, 0, Space.Self);
                        break;
                    case 2:

                        transform.Rotate(0, 270, 0, Space.Self);

                        break;
                }

                ((PointingScript)(GameObject.Find("helperObject").GetComponent("PointingScript"))).PointFakeButton = true;

                Invoke("unpush", 0.2f);
            }


        }
    }

    /// <summary>
    /// Unpauses this instance.
    /// </summary>
    private void Unpause ()
    {
        ((Pause)(GameObject.Find("OVRCameraRig").GetComponent("Pause"))).FakePauseButton = true;
    }

    /// <summary>
    /// Unpushes this instance.
    /// </summary>
    void unpush ()
    {
        ((PointingScript)(GameObject.Find("helperObject").GetComponent("PointingScript"))).PointFakeButton = false;
        ((Stressor)(GameObject.Find("StressorYellow").GetComponent("Stressor"))).FakePress = false;

    }

    /// <summary>
    /// Genratas the time for debug player responce deley.
    /// </summary>
    public void GenrataTimeForDebugPlayerResponceDeley ()
    {

        //generate here the time
        responceTime = (float)urand.Range(10, 60, UnityRandom.Normalization.STDNORMAL, 10.0f);

        responceTime = responceTime / 100;

        Invoke("PushAndUnpushButton", responceTime);
            
        Debug.Log(responceTime);

    }

    public void PushAndUnpushButton ()
    {
        ((Stressor)(GameObject.Find("StressorYellow").GetComponent("Stressor"))).FakePress = true;
        Invoke("unpush", 0.3f);
        // this will "push " the button

    }

}


