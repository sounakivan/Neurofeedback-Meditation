using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuButton : MonoBehaviour
{
    [SerializeField] GameControl gameController;
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject mirror;
    [SerializeField] GameObject lightOrb;
    [SerializeField] GameObject lefthand;
    [SerializeField] GameObject righthand;
    [SerializeField] Slider timeSlider;
    [SerializeField] Toggle captionToggle;

    public bool startMeditate;

    void Start()
    {
        startMeditate = false;
    }

    public void onBtnClick()
    {
        startMeditation(true);
    }

    public void startMeditation(bool visible)
    {
        //set onboarding components visibility
        menuCanvas.GetComponent<Canvas>().enabled = !visible;
        lefthand.GetComponent<XRInteractorLineVisual>().enabled = !visible;
        righthand.GetComponent<XRInteractorLineVisual>().enabled = !visible;

        //set meditation components visibility
        mirror.SetActive(visible);
        lightOrb.SetActive(visible);

        startMeditate = true;
        Debug.Log("meditation started");
    }

    public void toggleCaptions()
    {
        //set onboarding components visibility
        gameController.captionVisibility = captionToggle.isOn;
    }

    public void setMeditationTimer()
    {
        gameController.meditationTime = timeSlider.value;
    }
}
