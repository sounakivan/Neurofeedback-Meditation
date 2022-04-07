using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class VRAvatarCalibrator : MonoBehaviour
{
    public PrimaryButtonWatcher watcher;
    public bool IsPressed = false; // used to display button state in the Unity Inspector window

    public VRIK ik;
    public float scaleMlp = 1f;

    void Start()
    {
        watcher.primaryButtonPress.AddListener(OnPrimaryButtonEvent);

    }

    public void OnPrimaryButtonEvent(bool pressed)
    {
        IsPressed = pressed;
        //Compare the height of the head target to the height of the head bone, multiply scale by that value.
        float sizeF = (ik.solver.spine.headTarget.position.y - ik.references.root.position.y) / (ik.references.head.position.y - ik.references.root.position.y);
        ik.references.root.localScale *= sizeF * scaleMlp;
    }


}
