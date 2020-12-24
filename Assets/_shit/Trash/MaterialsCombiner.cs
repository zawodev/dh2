using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialsCombiner : MonoBehaviour {
    public SpriteRenderer sprt;
    public Material[] mat;
    private void Start() {
        sprt = GetComponent<SpriteRenderer>();
    }
    void Update() {
        if (Application.isEditor) {
            sprt.materials = mat;
        }
    }
}
