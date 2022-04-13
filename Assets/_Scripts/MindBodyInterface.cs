using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dirox.emotiv.controller;

public class MindBodyInterface : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] DataSubscriber eeg;

    public float focusData;
    public float relaxData;

    private float timeElapsed = 0;
    public float lerpDuration = 600;

    public float focAmp;
    public float relFre;
    private Color fresnelcolor;
    public Color startcolor;
    public Color endcolor;

    [SerializeField] private bool eegActive = false;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        //startcolor = new Color(0.8584906f, 0.1247241f, 0.4385725f);
        //endcolor = new Color(0.6391598f, 0.8231602f, 0.8867924f);
        fresnelcolor = startcolor;
        focAmp = 0.009f;
        relFre = 1f;
    }

    void Update()
    {
        if (eeg.meditateOn)
        {
            if (eegActive)
            {
                if (eeg.focus >= 0 && eeg.focus <= 1 && eeg.relax >= 0 && eeg.relax <= 1)
                {
                    focusData = eeg.focus;
                    relaxData = eeg.relax;
                }
                else
                {
                    focusData = 0.5f;
                    relaxData = 0.5f;
                }
                focAmp = scale(0f, 1f, 0.001f, 0.009f, focusData);
                relFre = scale(0f, 1f, 0.01f, 1f, relaxData);
            }
            else
            {
                if (timeElapsed < lerpDuration)
                {
                    focAmp = Mathf.Lerp(0.009f, 0.001f, timeElapsed / lerpDuration);
                    relFre = Mathf.Lerp(1f, 0.01f, timeElapsed / lerpDuration);
                    fresnelcolor = Color.Lerp(startcolor, endcolor, timeElapsed / lerpDuration);
                    timeElapsed += Time.deltaTime;
                }
            }

            material.SetFloat("_Amplitude", focAmp);
            material.SetFloat("_Frequency", relFre);
            material.SetColor("_FresnelColor", fresnelcolor);
        }
    }

    public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}
