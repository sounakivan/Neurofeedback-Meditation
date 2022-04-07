using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dirox.emotiv.controller;
using EmotivUnityPlugin;
using UnityEngine.UI;

public class SetHandData : MonoBehaviour
{
    [SerializeField] DataSubscriber subsriber;

    [SerializeField] private Text focusData;       // performance metric data
    [SerializeField] private Text relaxData;       // performance metric data
    [SerializeField] private Text stressData;       // performance metric data

    float counter = 0;

    void Start()
    {
        focusData.text = "0";
        relaxData.text = "0";
        stressData.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        //focusData.text = subsriber.focusPMData.text;
        //relaxData.text = subsriber.relaxPMData.text;
        //stressData.text = subsriber.stressPMData.text;
        counter += Time.deltaTime;

        if (counter > 60)
        {
            focusData.text = Random.Range(0.00f, 1.00f).ToString("0.##");
            relaxData.text = Random.Range(0.00f, 1.00f).ToString("0.##");
            stressData.text = Random.Range(0.00f, 1.00f).ToString("0.##");

            counter = 0;
        }

    }
}
