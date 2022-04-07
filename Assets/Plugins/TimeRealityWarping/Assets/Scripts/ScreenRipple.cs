using System;
using UnityEngine;

public class ScreenRipple : MonoBehaviour {
    [Tooltip("Shader Hidden/ScreenRipple goes here.")]
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

    //Parameters
    [HideInInspector]
    public Vector2 normalOffset;

    public int useTransform = 0;
    [Tooltip("Transform to use for ripple's origin.")]
    public Transform originObject;
    [Tooltip("The position of the ripple's origin.")]
    public Vector3 originPosition;

    [Tooltip("How much the image is distorted.")]
    [Range(-1,1)]
    public float distortion = 1;

    [Tooltip("Radius of the ripple.")]
    [Range(0, 2)]
    public float ringSize = 0.0f;

    [Tooltip("How thick the ripple is. A value of 0 turns off the effect.")]
    [Range(0, 1)]
    public float ringThickness = 0.0f;

    [Tooltip("The amount the original image is blended with the distorted image. A value of 1 turns off the effect.")]
    [Range(0, 1)]
    public float fadeAmount = 0.0f;

    [Tooltip("Distance from origin point in screen space before the effect fades out completely.")]
    [Range(0, 2)]
    public float falloffDistance;

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

        //Set material parameters
        position = worldSpace ? (cam.WorldToViewportPoint((useTransform == 0 && originObject != null) ? originObject.position : originPosition)) : originPosition;
        material.SetVector("_Position", position);
        material.SetFloat("_Distortion", distortion);
        material.SetFloat("_RingSize", ringSize);
        material.SetFloat("_OuterThick", ringThickness);
        material.SetFloat("_FadeAmount", fadeAmount);
        material.SetFloat("_FalloffDistance", falloffDistance);
        material.SetFloat("_WorldSpace", Convert.ToSingle(worldSpace));
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
