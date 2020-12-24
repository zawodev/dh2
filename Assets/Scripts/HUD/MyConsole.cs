using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyConsole : MonoBehaviour {

    public GameObject EscPanel;
    public GameObject KNotP;
    public GameObject charOutfit;
    string command;
    bool pause;

    private void Start() {
        EscPanel.SetActive(pause);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            charOutfit.SetActive(false);
            KNotP.SetActive(false);
            command = "";

            pause = !pause;
            EscPanel.SetActive(pause);

            if (pause) Time.timeScale = 0;
            else Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            switch (command) {
                case "player_outfit":
                    charOutfit.SetActive(true);
                    break;
                default:
                    StartCoroutine(W8er());
                    break;
            }
            Clear();
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            StartCoroutine(W8er());
            Clear();
        }
    }
    public void OnEndEdit(string text) {
        command = text;
    }
    void Clear() {
        EscPanel.GetComponentInChildren<TMP_InputField>().text = "";
        command = "";
    }
    IEnumerator W8er() {
        KNotP.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        KNotP.SetActive(false);
    }
}
