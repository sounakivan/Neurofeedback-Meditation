using System.Collections;
using UnityEngine;

public class TimeBurst : MonoBehaviour {

    public ScreenRipple screenRipple;

    public enum BlendType {None, Additive, Subtractive };

    public BlendType blendType;
    public Color color;

    [Range(0,1)]
    public float fadeAmount = .6f;

    [Range(0, .5f)]
    public float ringThickness = 0.337f;

    [Range(-1, 1)]
    public float distortion = .1f;

    public float falloffDistance = 1.5f;

    private void Awake()
    {
        if (screenRipple == null)
        {
            screenRipple = Camera.main.GetComponent<ScreenRipple>();
        }
        if (screenRipple == null)
        {
            throw new System.Exception("A camera with a Screen Ripple component must be assigned!");
        }
    }

    public void Burst()
    {
        //interrupt coroutine if currently playing
        StopCoroutine("TimeBurstSequence");
        StartCoroutine("TimeBurstSequence");
    }

    IEnumerator TimeBurstSequence()
    {
        if (screenRipple != null)
        {
            //initial values
            screenRipple.ringSize = 0;
            screenRipple.falloffDistance = falloffDistance;
            screenRipple.distortion = -1;
            screenRipple.ringThickness = 0.337f;
            screenRipple.fadeAmount = 0;
            screenRipple.color = color;
            screenRipple.blendType = (int)blendType;

            //compress distortion
            float timer = 0;
            float maxTime = 1f;
            while (timer < maxTime)
            {
                yield return null;
                screenRipple.ringSize = Mathf.Lerp(.05f, .2f, timer / maxTime);
                timer += Time.deltaTime;
            }
            yield return new WaitForSeconds(.05f);
            timer = 0;
            maxTime = .15f;
            //expand out
            while (timer < maxTime)
            {
                yield return null;
                screenRipple.ringSize = Mathf.Lerp(.2f, .1f, timer / maxTime);
                timer += Time.deltaTime;
            }
            screenRipple.distortion = distortion;
            screenRipple.fadeAmount = fadeAmount;
            screenRipple.ringThickness = ringThickness;
            screenRipple.blendType = 0;
            timer = 0;
            maxTime = 3f;
            while (timer < maxTime)
            {
                yield return null;
                screenRipple.ringSize = Mathf.Lerp(.2f, screenRipple.falloffDistance, timer / maxTime);

                timer += Time.deltaTime;
            }
            yield return new WaitForSeconds(.2f);
            screenRipple.ringSize = 0;
        }
        

    }
}
