using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dirox.emotiv.controller;

public class GameControl : MonoBehaviour
{
    //[SerializeField] DataSubscriber datasub;
    
    //captions text and voiceover audio variables
    [SerializeField] AudioSource voice;

    public AudioClip intro;
    public AudioClip outro;
    public AudioClip[] voices = new AudioClip[] {    };

    public Text caption;
    public string[] introCaps = new string[] {    };
    public string[] guidance = new string[] {    };

    //timer variables
    [SerializeField] private Text timeText;
    private float seconds = 0;

    public float durationbBetweenGuidance = 64f;
    private float duration = 0;
    private int index = 0;

    private void Update()
    {
        if (true)
        {
            seconds += Time.deltaTime;

            float minutes = seconds / 59;
            string minuteString = ((int)minutes).ToString("f0");
            string secondString = (seconds % 59).ToString("f0");

            timeText.text = minuteString + ":" + secondString;

            playVoice();
        }
        //Debug.Log(datasub.meditateOn);
    }

    private void playVoice()
    {
        if (seconds <= 16)
        {
            duration += Time.deltaTime;
            if (duration > 2)
            {
                voice.PlayOneShot(intro);
                duration = -15;
            }
            caption.text = introCaps[0];
        }
        else if (seconds > 16 && seconds <= 34)
        {
            caption.text = introCaps[1];
            duration = 0;
        }
        else if (seconds > 34 && seconds <= 55)
        {
            caption.text = introCaps[2];
        }
        else if (seconds > 55 && seconds <= 58)
        {
            caption.text = introCaps[3];
        }
        else if (seconds > 58 && seconds <= 64)
        {
            duration += Time.deltaTime;

            if (duration > 6)
            {
                voice.PlayOneShot(voices[0]);
                caption.text = guidance[0];
                duration = 0;
            }
        }
        else if (seconds > 64 && seconds <= 600)
        {
            duration += Time.deltaTime;

            if (duration > durationbBetweenGuidance)
            {
                if (index > voices.Length)
                {
                    index = 0;
                }
                index += 1;
                voice.PlayOneShot(voices[index]);
                caption.text = guidance[index];
                duration = 0;
            }
        }
        else if (seconds > 600 && seconds <= 610)
        {
            duration += Time.deltaTime;

            if (duration > 6)
            {
                voice.PlayOneShot(outro);
                duration = 0;
            }
        }
    }

}
