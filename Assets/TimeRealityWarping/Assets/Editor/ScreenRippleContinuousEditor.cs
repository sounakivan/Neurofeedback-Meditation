using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScreenRippleContinuous))]
[CanEditMultipleObjects]
public class ScreenRippleContinuousEditor : Editor {

    SerializedProperty shader;

    [Tooltip("Manually enter a position or use an object's transform position.")]
    public int posIndex = 0;
    string[] rippleOrigin = new string[] { "Object Transform", "Vector Position" };

    SerializedProperty normalMap;
    SerializedProperty normalScale;
    SerializedProperty bumpStrength;
    SerializedProperty normalOffset;

    SerializedProperty useTransform;
    SerializedProperty originObject;
    SerializedProperty position;

    SerializedProperty distortion;

    SerializedProperty ringThinness;

    SerializedProperty fadeAmount;

    SerializedProperty effectRadius;

    SerializedProperty speed;
    SerializedProperty frequency;

    SerializedProperty worldSpace;

    SerializedProperty color;
    SerializedProperty blendType;
    string[] colorBlend = new string[] { "None", "Additive", "Subtractive" };

    private void OnEnable()
    {
        shader = serializedObject.FindProperty("shader");
        normalMap = serializedObject.FindProperty("normalMap");
        normalScale = serializedObject.FindProperty("normalScale");
        normalOffset = serializedObject.FindProperty("normalOffsetScroll");
        bumpStrength = serializedObject.FindProperty("bumpStrength");
        useTransform = serializedObject.FindProperty("useTransform");
        originObject = serializedObject.FindProperty("originObject");
        position = serializedObject.FindProperty("originPosition");

        distortion = serializedObject.FindProperty("distortion");
        ringThinness = serializedObject.FindProperty("ringThinness");
        fadeAmount = serializedObject.FindProperty("fadeAmount");
        effectRadius = serializedObject.FindProperty("effectRadius");
        speed = serializedObject.FindProperty("speed");
        frequency = serializedObject.FindProperty("frequency");

        color = serializedObject.FindProperty("color");
        blendType = serializedObject.FindProperty("blendType");

        worldSpace = serializedObject.FindProperty("worldSpace");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(shader);
        EditorGUILayout.Space();

        GUILayout.Label("Ripple Origin", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(worldSpace);
        if (worldSpace.boolValue)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(new GUIContent("Origin Type", "Manually enter a position or use an object's transform position."));
            posIndex = EditorGUILayout.Popup(posIndex, rippleOrigin);
            useTransform.intValue = posIndex;
            EditorGUILayout.EndHorizontal();
            switch (posIndex)
            {
                case 0:
                    EditorGUILayout.PropertyField(originObject);
                    break;
                case 1:
                    EditorGUILayout.PropertyField(position);
                    break;
                default:
                    EditorGUILayout.PropertyField(position);
                    break;
            }
        }
        else
        {
            EditorGUILayout.PropertyField(position);
        }
        
        EditorGUILayout.Space();
        GUILayout.Label("Properties", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(effectRadius);
        EditorGUILayout.PropertyField(distortion);
        EditorGUILayout.PropertyField(frequency);
        EditorGUILayout.PropertyField(speed);
        EditorGUILayout.PropertyField(ringThinness);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(fadeAmount);
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Color Blend Type", "Whether the color is added or subtracted from the image, or no color effect at all."));
        blendType.intValue = EditorGUILayout.Popup(blendType.intValue, colorBlend);
        EditorGUILayout.EndHorizontal();
        if (blendType.intValue == 0)
        {
            GUI.enabled = false;
        }
        EditorGUILayout.PropertyField(color);
        GUI.enabled = true;
        EditorGUILayout.Space();

        GUILayout.Label("Normal Map", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(normalMap);
        if (normalMap.objectReferenceValue != null)
        {
            EditorGUILayout.PropertyField(normalScale);
            EditorGUILayout.PropertyField(normalOffset);
            EditorGUILayout.PropertyField(bumpStrength);
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}
