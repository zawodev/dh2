using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using System.Linq;

[ExecuteInEditMode]
public class BorderLine : MonoBehaviour {

    public LineRenderer LINE;
    public SpriteRenderer sprt;

    private void Update() {
        if (Application.isEditor) Actualize();
    }
    public void Actualize() {
        SetPointPos(transform.position + new Vector3(-sprt.bounds.size.x / 2, sprt.bounds.size.y / 2, 0), 0);
        SetPointPos(transform.position + new Vector3(sprt.bounds.size.x / 2, sprt.bounds.size.y / 2, 0), 1);
        SetPointPos(transform.position + new Vector3(sprt.bounds.size.x / 2, -sprt.bounds.size.y / 2, 0), 2);
        SetPointPos(transform.position + new Vector3(-sprt.bounds.size.x / 2, -sprt.bounds.size.y / 2, 0), 3);
    }
    void SetPointPos(Vector3 a, int i) {
        LINE.SetPosition(i, new Vector3(a.x, a.y, 0));
    }
}