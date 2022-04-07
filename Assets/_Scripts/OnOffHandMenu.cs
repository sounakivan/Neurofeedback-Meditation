using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class OnOffHandMenu : MonoBehaviour
{
    public GameObject moodUI;
    public bool activeMoodUI = true;
    //[SerializeField] ActionBasedController actionBasedController;


    // Start is called before the first frame update
    void Start()
    {
        DisplayMoodUI();

        //actionBasedController.selectAction.action.performed += PressTrigger;
    }

    public void PressTrigger(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            DisplayMoodUI();
        }
    }

    public void DisplayMoodUI()
    {
        if(activeMoodUI)
        {
            moodUI.SetActive(false);
            activeMoodUI = false;
        }
        else if (!activeMoodUI)
        {
            moodUI.SetActive(true);
            activeMoodUI = true;
        }
    }
}
