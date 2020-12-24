using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TextureApplier : MonoBehaviour {

    Material mat;
    public Texture2D texture;

    private void Start() {
        SwitchTexture();
    }
    private void Update() {
        if (Application.isEditor) {
            SwitchTexture();
        }
    }
    public void SwitchTexture() {
        mat = GetComponent<SpriteRenderer>().material;
        //mat = GetComponent<SpriteRenderer>().sharedMaterial;
        if (mat != null) {
            //mat.SetTexture("_SecTex", texture);
        }
    }
}
