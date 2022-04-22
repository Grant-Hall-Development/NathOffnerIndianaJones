﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UT_demo_example_event_intervals : MonoBehaviour
{
    public GameObject eventInterval1Enabled;
    public GameObject eventInterval2Enabled;
    public GameObject eventInterval3Enabled;
    public GameObject eventInterval1Disabled;
    public GameObject eventInterval2Disabled;
    public GameObject eventInterval3Disabled;



    public void ShowEventInterval1()
    {
        eventInterval1Enabled.SetActive(true);
        eventInterval1Disabled.SetActive(false);
    }

    public void ShowEventInterval2()
    {
        eventInterval2Enabled.SetActive(true);
        eventInterval2Disabled.SetActive(false);
    }

    public void ShowEventInterval3()
    {
        eventInterval3Enabled.SetActive(true);
        eventInterval3Disabled.SetActive(false);
    }

    public void ResetEventIntervals()
    {
        eventInterval1Enabled.SetActive(false);
        eventInterval1Disabled.SetActive(true);

        eventInterval2Enabled.SetActive(false);
        eventInterval2Disabled.SetActive(true);

        eventInterval3Enabled.SetActive(false);
        eventInterval3Disabled.SetActive(true);
    }
}
