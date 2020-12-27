using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PropertyBlockSetter : MonoBehaviour {

    public enum ShaderType { ALL, TAS, SKEW, BLUR };
    public ShaderType shaderType;

    [HideInInspector]
    public Texture2D mainTex;
    [HideInInspector]
    public Texture2D applyTex;
    [HideInInspector]
    public float blurAmount;
    [HideInInspector]
    public float scale = 1;
    [HideInInspector]
    public Vector2 offset;

    //The material property block we pass to the GPU
    private MaterialPropertyBlock propertyBlock;

    Material mat;

    public void CreateInstance() {
        mat = GetComponent<SpriteRenderer>().material;
    }
    public void Refresh() {
        //create propertyblock only if none exists
        if (propertyBlock == null) propertyBlock = new MaterialPropertyBlock();
        if (mainTex == null) mainTex = GetComponent<SpriteRenderer>().sprite.texture;

        //Get a renderer component either of the own gameobject or of a child
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        //set the color property
        if (mainTex != null) propertyBlock.SetTexture("_MainTex", mainTex);
        if (applyTex != null) propertyBlock.SetTexture("_ApplyTex", applyTex);
        propertyBlock.SetFloat("_BlurAmount", blurAmount);
        propertyBlock.SetFloat("_Scale", scale);
        propertyBlock.SetVector("_Offset", offset);

        //apply propertyBlock to renderer
        renderer.SetPropertyBlock(propertyBlock);
    }
    // OnValidate is called in the editor after the component is edited
    void OnValidate() {
        Refresh();
    }
}
[CustomEditor(typeof(PropertyBlockSetter))]
public class PropertyBlockSetterEditor : Editor {

    private static Texture2D TextureField(string name, Texture2D texture) {
        GUILayout.BeginVertical();
        var style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.UpperCenter;
        style.fixedWidth = 70;
        GUILayout.Label(name, style);
        var result = (Texture2D)EditorGUILayout.ObjectField(texture, typeof(Texture2D), false, GUILayout.Width(70), GUILayout.Height(70));
        GUILayout.EndVertical();
        return result;
    }
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        PropertyBlockSetter pbs = (PropertyBlockSetter)target;

        EditorGUILayout.Space();
        if (GUILayout.Button("Create Instance")) {
            pbs.CreateInstance();
        }
        if (GUILayout.Button("Refresh")) {
            pbs.Refresh();
        }
        EditorGUILayout.Space();

        if (pbs.shaderType == PropertyBlockSetter.ShaderType.ALL) {

            EditorGUILayout.BeginHorizontal();
            pbs.mainTex = TextureField("MainTex", pbs.mainTex);
            pbs.applyTex = TextureField("ApplyTex", pbs.applyTex);
            EditorGUILayout.EndHorizontal();

            pbs.blurAmount = EditorGUILayout.FloatField("Blur Amount", pbs.blurAmount);
        }
        if (pbs.shaderType == PropertyBlockSetter.ShaderType.TAS) {
            EditorGUILayout.BeginHorizontal();
            pbs.mainTex = TextureField("MainTex", pbs.mainTex);
            pbs.applyTex = TextureField("ApplyTex", pbs.applyTex);
            EditorGUILayout.EndHorizontal();

            pbs.scale = EditorGUILayout.FloatField("Scale", pbs.scale);
            pbs.offset = EditorGUILayout.Vector2Field("Offset", pbs.offset);
        }
        if (pbs.shaderType == PropertyBlockSetter.ShaderType.SKEW) {

        }
        if (pbs.shaderType == PropertyBlockSetter.ShaderType.BLUR) {
            pbs.mainTex = TextureField("MainTex", pbs.mainTex);
            pbs.blurAmount = EditorGUILayout.FloatField("Blur Amount", pbs.blurAmount);
        }
    }
}
