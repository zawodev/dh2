using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTravelArrows : MonoBehaviour {

    [Range(0f, 10f)]
    public float frequency, farness;

    private void Start() {
        foreach (Transform t in transform) {
            LeanTween.moveLocalY(t.gameObject, t.localPosition.y + farness, frequency).setEaseInOutQuad().setLoopPingPong();
        }
    }
}
