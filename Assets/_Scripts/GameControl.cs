using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dirox.emotiv.controller;
using RootMotion.FinalIK;

public class GameControl : MonoBehaviour
{
    [SerializeField] DataSubscriber datasub;
    [SerializeField] GameObject movableAvatar;
    [SerializeField] GameObject avatarLight;
    [SerializeField] GameObject selfLight;
    [SerializeField] GameObject head;
    public Transform obeStartPos, obeEndPos;

    private Vector3 playerPos;
    private Vector3 targetPos;

    //captions text and voiceover audio variables
    [SerializeField] AudioSource voice;
    [SerializeField] AudioSource music;

    public float startvolume = 0.02f;
    public float endvolume = 0.4f;

    public AudioClip intro;
    public AudioClip outro;
    public AudioClip bell;
    public AudioClip[] voices = new AudioClip[] {    };

    public Text caption;
    public string[] introCaps = new string[] {    };
    public string[] guidance = new string[] {    };

    //timer variables
    [SerializeField] public float meditationTime = 600f;
    [SerializeField] private Text timeText;
    private float seconds = 0;

    public float durationbBetweenGuidance = 64f;
    private float duration = 0;
    private int index = 0;

    public bool obe = false;

    private void Start()
    {
        selfLight.SetActive(false);
        targetPos = obeEndPos.position;
        obe = false;
    }

    private void Update()
    {
        if (datasub.meditateOn == true)
        {
            seconds += Time.deltaTime;

            //update timer clock
            float minutes = seconds / 59;
            string minuteString = ((int)minutes).ToString("f0");
            string secondString = (seconds % 59).ToString("f0");

            timeText.text = minuteString + ":" + secondString;

            //guided meditation
            playGuidedMeditation();

            //music control
            music.volume = Mathf.Lerp(startvolume, endvolume, seconds / meditationTime);
        }
        //Debug.Log(datasub.meditateOn);

        triggerOutofBody();
        Debug.Log(seconds);
    }

    private void playGuidedMeditation()
    {
        //play intro
        if (seconds <= 16)
        {
            duration += Time.deltaTime;
            if (duration > 2)
            {
                voice.PlayOneShot(intro);
                duration = -15;
                caption.text = introCaps[0];
            }
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
        //guided voice
        else if (seconds > 58 && seconds <= 68)
        {
            duration += Time.deltaTime;

            if (duration > 6)
            {
                voice.PlayOneShot(voices[0]);
                caption.text = guidance[0];
                duration = 0;
                index = 1;
            }
        }
        else if (seconds > 68 && seconds <= meditationTime)
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
        //outro
        else if (seconds > meditationTime && seconds <= meditationTime + 6f)
        {
            duration += Time.deltaTime;

            if (duration > 5)
            {
                voice.PlayOneShot(outro);
                voice.PlayOneShot(bell);
                caption.text = "Peace, peace, peace. AUM.";
                duration = 0;
            }
            timeText.text = "Thank you";
        }
        //out of body
        else if (seconds > meditationTime + 6f)
        {
            //disable avatar agency
            if (avatarLight.activeInHierarchy)
            {
                movableAvatar.GetComponent<VRIK>().enabled = false;
                playerPos = obeStartPos.position;
                duration = 0;
                selfLight.SetActive(true);
                avatarLight.SetActive(false);
            }

            //make camera float up
            duration += Time.deltaTime;
            Vector3 pos = Vector3.Lerp(playerPos, targetPos, duration/30);
            head.transform.position = pos;
            Debug.Log(duration);
        }
    }

    private void triggerOutofBody()
    {
        if(Input.GetKey(KeyCode.L))
        {
            seconds += meditationTime;
        }
    }
}
