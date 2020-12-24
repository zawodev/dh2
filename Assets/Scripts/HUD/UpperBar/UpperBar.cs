using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperBar : MonoBehaviour {

    public static UpperBar UB;

    public bool state;
    public bool alwaysOn;

    [Range(1f, 5f)]
    public float stayTime;
    float _stayTimer;

    [Range(.1f, 1f)]
    public float transitionTime = .4f;
    float closePos;
    
    void Awake() {
        if (UB != null) {
            Destroy(gameObject);
            return;
        }
        else UB = this;
    }
    private void Start() {
        closePos = transform.localPosition.y;

        switch (state) {
            case true:
                LeanTween.moveLocalY(gameObject, closePos, 0f);
                break;
            case false:
                LeanTween.moveLocalY(gameObject, closePos + 50, 0f);
                break;
        }
    }
    public void ToggleBar(bool newState) {
        if (state != newState) {
            state = newState;
            switch (state) {
                case true:
                    _stayTimer = stayTime;
                    LeanTween.moveLocalY(gameObject, closePos, transitionTime).setEaseInOutCubic();
                    break;
                case false:
                    _stayTimer = 0;
                    LeanTween.moveLocalY(gameObject, closePos + 50, transitionTime).setEaseInOutCubic();
                    break;
            }
            //Debug.Log(newState ? "open" : "close");
        }
    }
    private void Update() {

        if (_stayTimer > 0) {
            _stayTimer -= Time.deltaTime;
            if (_stayTimer == 0 && !alwaysOn) ToggleBar(false);
        }
        else if (_stayTimer < 0 && !alwaysOn) {
            ToggleBar(false);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            alwaysOn = !alwaysOn;
            ToggleBar(true);
        }
    }
}
