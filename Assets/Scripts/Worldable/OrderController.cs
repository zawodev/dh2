using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OrderController : MonoBehaviour {

    public bool moving; //or 2 points for liner, called in update
    public Transform point;

    [HideInInspector]
    [Space]
    public bool linerBETA;
    [HideInInspector]
    public int amplitude = 10;

    SpriteRenderer sr;
    TrailRenderer tr;
    ParticleSystemRenderer psr;

    int startOrder;

    private void Start() {
        sr = gameObject.GetComponent<SpriteRenderer>();
        tr = gameObject.GetComponent<TrailRenderer>();
        psr = gameObject.GetComponent<ParticleSystemRenderer>();

        startOrder = sr != null ? sr.sortingOrder : tr != null ? tr.sortingOrder : psr.sortingOrder;

        if (!moving) {
            if (point != null) {
                if (sr != null) sr.sortingOrder = (int)(point.transform.position.y * -1000) + startOrder;
                if (tr != null) tr.sortingOrder = (int)(point.transform.position.y * -1000) + startOrder;
                if (psr != null) psr.sortingOrder = (int)(point.transform.position.y * -1000) + startOrder;
            }
            else {
                if (sr != null) sr.sortingOrder = 4500 + startOrder;
                if (tr != null) tr.sortingOrder = 4500 + startOrder;
                if (psr != null) psr.sortingOrder = 4500 + startOrder;
            }
            enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) isTriggerred = true;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) isTriggerred = false;
    }
    //trigger must be
    bool isTriggerred;
    private void Update() {
        if (linerBETA && sr != null) {
            if (isTriggerred) sr.sortingOrder = (int)(point.transform.position.y * -1000) + startOrder + amplitude;
            else sr.sortingOrder = (int)(point.transform.position.y * -1000) + startOrder - amplitude;
        }
        else if (point != null) {
            if (sr != null) sr.sortingOrder = (int)(point.transform.position.y * -1000) + startOrder;
            if (tr != null) tr.sortingOrder = (int)(point.transform.position.y * -1000) + startOrder;
            if (psr != null) psr.sortingOrder = (int)(point.transform.position.y * -1000) + startOrder;
        }
        else {
            if (sr != null) sr.sortingOrder = 4500 + startOrder;
            if (tr != null) tr.sortingOrder = 4500 + startOrder;
            if (psr != null) psr.sortingOrder = 4500 + startOrder;
        }
    }
    public bool isLeft(Transform line1, Transform line2, Transform point) {

        Vector2 a = line1.position;
        Vector2 b = line2.position;
        Vector2 c = point.position;

        return ((b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x)) > 0;
    }
}
[CustomEditor(typeof(OrderController)), CanEditMultipleObjects]
public class OrderControllerEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        OrderController oc = (OrderController)target;

        if (oc.moving) {
            oc.linerBETA = EditorGUILayout.Toggle("linerBETA", oc.linerBETA);
            oc.amplitude = EditorGUILayout.IntSlider("Amplitude", oc.amplitude, 1, 30);
        }
    }
}