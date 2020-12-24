using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour {

    public CameraSettings cameraSettings;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            CameraFollow.CF.ActualizeCameraSettings(cameraSettings);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            CameraFollow.CF.ActualizeCameraSettings();
        }
    }
}
