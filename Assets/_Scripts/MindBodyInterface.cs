using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dirox.emotiv.controller;

public class MindBodyInterface : MonoBehaviour
{
    [SerializeField] Material material;
    DataSubscriber eeg;

    private float focusData;
    private float relaxData;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float focAmp = scale(0f, 1f, 0.001f, 0.009f, focusData);
        float relFre = scale(0f, 1f, 0.01f, 1f, relaxData);

        material.SetFloat("_Amplitude", focAmp);
        material.SetFloat("_Frequency", relFre);
    }

    public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}
