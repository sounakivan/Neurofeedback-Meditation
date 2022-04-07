using UnityEngine;

public class BillboardObject : MonoBehaviour {

    public Camera mainCam;

	//if no specific camera is assigned, get the main camera
	void OnEnable () {
        if (mainCam == null)
        {
            mainCam = Camera.main;
            if (mainCam == null)
            {
                throw new System.Exception("There is no main camera!");
            }
        }
        
    }
	
	//orient object to camera
	void Update () {
        if (mainCam != null)
        {
            transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.back, mainCam.transform.rotation * Vector3.up);
        }
        
    }
}
