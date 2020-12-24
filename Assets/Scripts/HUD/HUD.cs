using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

    public static HUD hud;
    private void Awake() {
        if (hud != null) {
            Destroy(gameObject);
            return;
        }
        else hud = this;
        DontDestroyOnLoad(gameObject);

        GetComponent<Canvas>().worldCamera = CameraFollow.CF.GetComponent<Camera>();
    }
}
