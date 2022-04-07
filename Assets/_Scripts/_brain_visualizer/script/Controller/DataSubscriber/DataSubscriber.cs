using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EmotivUnityPlugin;
using Zenject;

namespace dirox.emotiv.controller
{
    /// <summary>
    /// Responsible for subscribing and displaying data
    /// we support for eeg, performance metrics, motion data at this version.
    /// </summary>
    public class DataSubscriber : BaseCanvasView
    {
        DataStreamManager _dataStreamMgr = DataStreamManager.Instance;

        [SerializeField] public Text focusPMData;       // performance metric data
        [SerializeField] public Text relaxPMData;       // performance metric data
        [SerializeField] public Text stressPMData;       // performance metric data

        float _timerDataUpdate = 0;
        const float TIME_UPDATE_DATA = 1f;

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
                double sdata = DataStreamManager.Instance.GetPMData("str");

                Debug.Log("======" + fdata + rdata + sdata);
                
                onPMSubBtnClick();
                if (fdata >= 0 && fdata <= 1) {
                    focusPMData.text = fdata.ToString("0.##");
                    relaxPMData.text = rdata.ToString("0.##");
                    stressPMData.text = sdata.ToString("0.##");
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

