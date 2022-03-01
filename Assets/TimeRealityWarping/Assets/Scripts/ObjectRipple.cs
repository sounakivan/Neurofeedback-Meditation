using UnityEngine;

public class ObjectRipple : MonoBehaviour {

    //Parameters
    [Tooltip("Should the object always be facing the camera?")]
    public bool billboard = true;
    [Tooltip("Camera used for billboarding (The main camera is used by default if one isn't assigned.)")]
    public Camera cam;

    [Tooltip("Offset the effect from texture center (-0.5 - 0.5)")]
    public Vector2 originOffset;
    [Tooltip("Distance from origin point before the effect fades out completely (-0.5 - 0.5)")]
    public float falloffDistance = 1;
    [Tooltip("How much the image is distorted.")]
    [Range(0,1)]
    public float distortion = .3f;
    [Tooltip("Radius of the ripple.")]
    [Range(0,2)]
    public float ringSize = .3f;
    [Tooltip("How thick the ripple is. A value of 0 turns off the effect.")]
    public float ringThickness = 2;

    [Tooltip("The amount the original image is blended with the distorted image. A value of 1 turns off the effect.")]
    [Range(0, 1)]
    public float fadeAmount = 0;

    //Optional color parameters
    public enum BlendType { None, Additive, Subtractive};
    [Tooltip("Whether the color is added or subtracted from the image, or no color effect at all.")]
    public BlendType blendType;
    [Tooltip("Optional color multiplied by distortion. Darker colors make weaker effects.")]
    public Color color = Color.white;

    //optional normal map parameters
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

    Renderer matRenderer;
    MaterialPropertyBlock propBlock;

    Vector4 bumpTile;

	void OnEnable () {
        matRenderer = GetComponent<Renderer>();
        propBlock = new MaterialPropertyBlock();

        //get main camera if one isn't assigned
        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null)
                throw new System.Exception("There is no main camera!");
        }
        
        
	}
	
	void Update () {

        //assign material parameters
        matRenderer.GetPropertyBlock(propBlock);

        propBlock.SetVector("_Position", originOffset);
        propBlock.SetFloat("_FalloffDistance", falloffDistance);
        propBlock.SetFloat("_Distortion", distortion);
        propBlock.SetFloat("_RingSize", ringSize);
        propBlock.SetFloat("_OuterThick", ringThickness);
        propBlock.SetFloat("_FadeAmount", fadeAmount);
        propBlock.SetFloat("_BlendType", (int)blendType);
        propBlock.SetColor("_Color", color);

        if (normalMap != null)
        {
            propBlock.SetTexture("_BumpTex", normalMap);
            propBlock.SetFloat("_BumpStrength", bumpStrength);
        }
        else
        {
            propBlock.SetTexture("_BumpTex", Texture2D.blackTexture);
            propBlock.SetFloat("_BumpStrength", 0);
        }
        
        normalOffset += normalOffsetScroll * Time.deltaTime;
        normalOffset -= new Vector2((int)normalOffset.x, (int)normalOffset.y);
        bumpTile = new Vector4(normalScale.x, normalScale.y, normalOffset.x, normalOffset.y);
        propBlock.SetVector("_BumpTex_ST", bumpTile);

        //set renderer property block
        matRenderer.SetPropertyBlock(propBlock);
    }

    private void LateUpdate()
    {
        if (billboard && cam != null)
        {
            //orient to camera if billboard is checked.
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.back, cam.transform.rotation * Vector3.up);
        }
    }
}
