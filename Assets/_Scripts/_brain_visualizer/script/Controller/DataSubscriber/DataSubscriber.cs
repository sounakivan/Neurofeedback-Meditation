using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EmotivUnityPlugin;
using Zenject;
using UnityEngine.XR.Interaction.Toolkit;

namespace dirox.emotiv.controller
{
    /// <summary>
    /// Responsible for subscribing and displaying data
    /// we support for eeg, performance metrics, motion data at this version.
    /// </summary>
    public class DataSubscriber : BaseCanvasView
    {
        DataStreamManager _dataStreamMgr = DataStreamManager.Instance;
        //GameControl gameStates;

        [SerializeField] GameObject emotivCanvas;
        [SerializeField] GameObject mirror;
        [SerializeField] GameObject lightOrb;
        [SerializeField] GameObject lefthand;
        [SerializeField] GameObject righthand;

        [SerializeField] public float focus;       // performance metric data
        [SerializeField] public float relax;       // performance metric data

        float _timerDataUpdate = 0;
        const float TIME_UPDATE_DATA = 1f;

        public bool meditateOn = false;

        private void Start()
        {
            //startMeditation(meditateOn);
            meditateOn = false;
        }

        void Update() 
        {
            if (!this.isActive) {
                return;
            }

            _timerDataUpdate += Time.deltaTime;
            if (_timerDataUpdate < TIME_UPDATE_DATA) 
                return;

            _timerDataUpdate -= TIME_UPDATE_DATA;
                                    
            // update pm data
            if (DataStreamManager.Instance.GetNumberPMSamples() > 0) {
                
                //bool hasPMUpdate = true;
                
                double fdata = DataStreamManager.Instance.GetPMData("foc");
                double rdata = DataStreamManager.Instance.GetPMData("rel");

                onPMSubBtnClick();

                if (fdata >= 0 && fdata <= 1) {
                    focus = (float)fdata;
                    relax = (float)rdata;
                }
                
            }
        }

        public override void Activate()
        {
            Debug.Log("DataSubscriber: Activate");
            base.Activate();
        }

        public override void Deactivate()
        {
            Debug.Log("DataSubscriber: Deactivate");
            base.Deactivate();
        }

        public void onPMSubBtnClick() {
            //Debug.Log("onPMSubBtnClick");
            List<string> dataStreamList = new List<string>(){DataStreamName.PerformanceMetrics};
            _dataStreamMgr.SubscribeMoreData(dataStreamList);

            startMeditation(true);
        }

        public void startMeditation(bool visible)
        {
            //set onboarding components visibility
            emotivCanvas.GetComponent<Canvas>().enabled = !visible;
            lefthand.GetComponent<XRInteractorLineVisual>().enabled = !visible;
            righthand.GetComponent<XRInteractorLineVisual>().enabled = !visible;
            
            //set meditation components visibility
            mirror.SetActive(visible);
            lightOrb.SetActive(visible);

            meditateOn = true;
            Debug.Log("meditation started");
        }

        /// <summary>
        /// UnSubscribe Performance metrics data
        /// </summary>
        public void onPMUnSubBtnClick() {
            Debug.Log("onPMUnSubBtnClick");
            List<string> dataStreamList = new List<string>(){DataStreamName.PerformanceMetrics};
            _dataStreamMgr.UnSubscribeData(dataStreamList);
            
        }
    }
}

