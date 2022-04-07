using UnityEngine;
using UnityEngine.UI;

public class DemoNav : MonoBehaviour {

    GameObject cam;
    public Button nextButton;
    public Button prevButton;

	// Use this for initialization
	void Start () {
        if (Camera.main == null)
            throw new System.Exception("There is no main camera!");
        cam = Camera.main.gameObject;
        
        prevButton.interactable = false;
	}

    public void Next()
    {
        cam.transform.position += new Vector3(5, 0, 0);
        prevButton.interactable = true;
        if (cam.transform.position.x >= 30)
        {
            cam.transform.position = new Vector3(30, 0, -10);
            nextButton.interactable = false;
        }
    }

    public void Prev()
    {
        cam.transform.position -= new Vector3(5, 0, 0);
        nextButton.interactable = true;
        if (cam.transform.position.x < 0)
        {
            cam.transform.position = new Vector3(0, 0, -10);
            prevButton.interactable = false;
        }
    }
}
