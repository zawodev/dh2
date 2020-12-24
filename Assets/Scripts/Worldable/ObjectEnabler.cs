using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEnabler : MonoBehaviour {

    public GameObject[] disabledOnContact;
    public GameObject[] enabledOnContact;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            foreach(GameObject g in enabledOnContact) {
                g.SetActive(true);
            }
            foreach (GameObject g in disabledOnContact) {
                g.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            foreach (GameObject g in enabledOnContact) {
                g.SetActive(false);
            }
            foreach (GameObject g in disabledOnContact) {
                g.SetActive(true);
            }
        }
    }
}
