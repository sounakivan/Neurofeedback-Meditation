using System;
using UnityEngine;

public class ScreenRippleContinuous : MonoBehaviour {
    [Tooltip("Shader Hidden/ScreenRippleContinuous goes here.")]
    public Shader shader;

    //Optional normal map parameters
    [Tooltip("Optional normal map texture that is added to the distortion.")]
    public Texture2D normalMap;
    [Tooltip("Normal map tiling")]
    public Vector2 normalScale = Vector2.one;
    [Tooltip("Normal map scrolling speed")]
    public Vector2 normalOffsetScroll = Vector2.zero;
    [Tooltip("How much the normal texture affects distortion.")]
    public float bumpStrength = 1;

    [HideInInspector]
    public Vector2 normalOffset;

    //Parameters
    public int useTransform = 0;
    [Tooltip("Transform to use for ripple's origin.")]
    public Transform originObject;
    [Tooltip("The position of the ripple's origin.")]
    public Vector3 originPosition;

    [Tooltip("How much the image is distorted.")]
    [Range(0,1)]
    public float distortion;

    [Tooltip("How thin each wave is. The greater the value, the thinner and farther apart each wave. Keep value at 2 for even spacing and thinness.")]
    public float ringThinness = 2;

    [Tooltip("The amount the original image is blended with the distorted image. A value of 1 turns off the effect.")]
    [Range(0, 1)]
    public float fadeAmount = 0.0f;

    [Tooltip("Radius of the effect in screen space. A value of 0 turns off the effect.")]
    [Range(0, 2)]
    public float effectRadius;

    [Tooltip("How fast the waves move out from the origin.")]
    public float speed = 3;
    [Tooltip("The greater the value, the more waves on screen at once.")]
    public float frequency = 2;

    [Tooltip("Should the origin point be in world space or screen space?")]
    public bool worldSpace;

    //Optional color parameters
    [Tooltip("Optional color multiplied by distortion. Darker colors make weaker effects.")]
    public Color color;
    public int blendType;

    Material material;
    Camera cam;
    Vector3 position;

	void Awake () {
        material = new Material(shader);
        cam = GetComponent<Camera>();
        
	}

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);

        //set material parameters
        position = worldSpace ? (cam.WorldToViewportPoint((useTransform == 0 && originObject != null) ? originObject.position : originPosition)) : originPosition;
        position.z = worldSpace ? position.z : 0;
        material.SetVector("_Position", position);
        material.SetFloat("_Distortion", distortion);
        material.SetFloat("_OuterThick", ringThinness);
        material.SetFloat("_FadeAmount", fadeAmount);
        material.SetFloat("_FalloffDistance", effectRadius);
        material.SetFloat("_WorldSpace", Convert.ToSingle(worldSpace));
        material.SetFloat("_Speed", speed);
        material.SetFloat("_Frequency", frequency);
        material.SetColor("_Color", color);
        material.SetInt("_BlendType", blendType);
        if (normalMap != null)
        {
            material.SetTexture("_BumpTex", normalMap);
            normalOffset += normalOffsetScroll * Time.deltaTime;
            normalOffset -= new Vector2((int)normalOffset.x, (int)normalOffset.y);
            material.SetTextureScale("_BumpTex", normalScale);
            material.SetTextureOffset("_BumpTex", normalOffset);
            material.SetFloat("_BumpStrength", bumpStrength);
        }
        else
        {
            material.SetTexture("_BumpTex", Texture2D.blackTexture);
            material.SetFloat("_BumpStrength", 0);
        }
    }
}
