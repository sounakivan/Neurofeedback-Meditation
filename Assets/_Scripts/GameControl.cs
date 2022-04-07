using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] GameObject emotivCanvas;
    [SerializeField] GameObject mirror;
    [SerializeField] GameObject avatar;

    public bool meditationStarted;
    public bool avatarSelected;

    private void Start()
    {
        meditationStarted = false;
        avatarSelected = false;
    }

    private void Update()
    {
        if (avatarSelected)
        {
            avatar.SetActive(true);
        }
        
        if (meditationStarted)
        {
            emotivCanvas.SetActive(false);
            mirror.SetActive(true);
        }
    }
}
