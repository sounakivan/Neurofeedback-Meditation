using System.Collections;
using UnityEngine;

public class DistortionHitSpawner : MonoBehaviour {

    public GameObject distortionHit;

	void Start () {
        StartCoroutine(Spawn());
	}
	
	IEnumerator Spawn()
    {
        //spawn distortion hit object every 1.5 seconds
        while(true)
        {
            Instantiate(distortionHit, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
