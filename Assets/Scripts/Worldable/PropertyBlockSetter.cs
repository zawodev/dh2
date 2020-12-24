using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyBlockSetter : MonoBehaviour {

    public Texture2D mainTex;
    [Range(0f, 5f)]
    public float blurAmount;

    //The material property block we pass to the GPU
    private MaterialPropertyBlock propertyBlock;

    // OnValidate is called in the editor after the component is edited
    void OnValidate() {
        //create propertyblock only if none exists
        if (propertyBlock == null) propertyBlock = new MaterialPropertyBlock();

        //Get a renderer component either of the own gameobject or of a child
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        //set the color property
        propertyBlock.SetTexture("_MainTex", mainTex);
        propertyBlock.SetFloat("_BlurAmount", blurAmount);

        //apply propertyBlock to renderer
        renderer.SetPropertyBlock(propertyBlock);
    }
}
