using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandScapeEvent : MonoBehaviour {

    public ParallaxController.LandScape enabledOnContact;
    ParallaxController.LandScape disabledOnContact;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            disabledOnContact = ParallaxController.parallaxController.GetLandScape();
            ParallaxController.parallaxController.ActualizeLandScape(enabledOnContact);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            ParallaxController.parallaxController.ActualizeLandScape(disabledOnContact);
        }
    }
}
