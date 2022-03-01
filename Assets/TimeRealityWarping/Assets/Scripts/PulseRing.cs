using UnityEngine;

public class PulseRing : MonoBehaviour {

    public ScreenRipple screenRipple;

    public float pulseSpeed = 1;

    public enum BlendType { None, Additive, Subtractive };
    public BlendType blendType;
    public Color color;

    [Range(0, 1)]
    public float fadeAmount = .6f;

    [Range(0, .5f)]
    public float ringThickness = 0.337f;

    [Range(-1, 1)]
    public float distortion = .1f;

    public float falloffDistance = 1.5f;
	
	// expand ring out continuously
	void Update () {
        if (screenRipple != null)
        {
            screenRipple.ringSize += Time.deltaTime * falloffDistance * pulseSpeed;
            if (screenRipple.ringSize >= falloffDistance)
                screenRipple.ringSize -= falloffDistance;

            screenRipple.falloffDistance = falloffDistance;
            screenRipple.color = color;
            screenRipple.blendType = (int)blendType;
            screenRipple.distortion = distortion;
            screenRipple.fadeAmount = fadeAmount;
            screenRipple.ringThickness = ringThickness;
        }
        
    }
}
