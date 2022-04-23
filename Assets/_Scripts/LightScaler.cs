using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScaler : MonoBehaviour
{
    [SerializeField] private int _Seconds = 4;
    [SerializeField] private int _MinScale = 0;
    [SerializeField] private int AntiSpeed = 2;
    private float _CurrentTimeInPhase;
    private float _CurrentScale;
    private bool _IsBreathing;

    private enum STATE { IN, HOLDIN, OUT, HOLDOUT }
    private STATE _CurrentState;

    private void Awake()
    {
        StartOrb();
    }


    public void StartOrb()
    {
        _IsBreathing = true;
        OnStart();
    }

    public void StopOrb()
    {
        _IsBreathing = false;
    }

    private void Update()
    {
        if (_IsBreathing == true)
        {
            //Debug.Log(_CurrentState);
            CheckState(Time.deltaTime / AntiSpeed);
            transform.localScale = new Vector3(_CurrentScale, _CurrentScale, _CurrentScale);

            _CurrentTimeInPhase += Time.deltaTime;
            if (_CurrentTimeInPhase >= _Seconds)
            {
                ChangeState();
                _CurrentTimeInPhase = 0;
            }
        }
    }

    private void OnStart()
    {
        _CurrentState = STATE.IN;
        _CurrentScale = _MinScale/2;

    }

    private void OnBreathIn(float time)
    {
        _CurrentScale += time;
    }

    private void OnHold()
    {

    }

    private void OnBreathOut(float time)
    {
        _CurrentScale -= time;
    }

    private void CheckState(float pTime)
    {
        if (_CurrentState == STATE.IN)
        {
            OnBreathIn(pTime);
        }
        if (_CurrentState == STATE.OUT)
        {
            OnBreathOut(pTime);
        }
        else
        {
            OnHold();
        }
    }

    private void ChangeState()
    {

        if (_CurrentState == STATE.IN)
        {
            _CurrentState = STATE.HOLDIN;
        }
        else if (_CurrentState == STATE.HOLDIN)
        {
            _CurrentState = STATE.OUT;
        }
        else if (_CurrentState == STATE.OUT)
        {
            _CurrentState = STATE.HOLDOUT;
        }
        else
        {
            _CurrentState = STATE.IN;
        }
    }
}
