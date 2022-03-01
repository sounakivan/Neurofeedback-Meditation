using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ObjectRipple))]
public class DistortionHit : MonoBehaviour {

    ObjectRipple rip;

    public float timeInSeconds = .4f;

    public enum BlendType { None, Additive, Subtractive };

    public BlendType blendType;
    public Color color;

    [Range(0, 1)]
    public float fadeAmount = .6f;

    [Range(0, .5f)]
    public float ringThickness = 0.337f;

    [Range(0, 1)]
    public float distortion = .1f;

    public float falloffDistance = 1.5f;

    public bool destroyAfterBurst = true;

    void Awake () {
        rip = GetComponent<ObjectRipple>();
	}

    private void OnEnable()
    {
        StopCoroutine("ExpandSequence");
        StartCoroutine("ExpandSequence");
    }

    IEnumerator ExpandSequence()
    {
        //initial values
        rip.ringSize = falloffDistance;
        rip.falloffDistance = falloffDistance;
        rip.color = color;
        rip.blendType = (ObjectRipple.BlendType)((int)blendType);

        rip.distortion = distortion;
        rip.fadeAmount = fadeAmount;
        rip.ringThickness = ringThickness;
        float timer = 0;
        float maxTime = timeInSeconds;

        //expand ring out
        while (timer <= maxTime)
        {
            yield return null;
            rip.ringSize = Mathf.Lerp(.2f, falloffDistance + ringThickness, timer / maxTime);

            timer += Time.deltaTime;
        }

        yield return new WaitForSeconds(1);
        //destroy object after burst
        if (destroyAfterBurst)
        {
            Destroy(gameObject);
        }
        
    }
}
