using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour {

    [Header("Settings")]

    public bool moveCamera;

    public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public Transform point;
    public Transform endPos;

    [Space]
    public Animator anim;
    public float duration;

    [Space]
    public bool freeze = true;
    public bool shotable = false;
    public bool turnable = false;

    [Header("On Start")]
    public DialogTrigger startDtg;
    public string musicToPlay;

    [Space]
    public string startAnimKey;

    [Header("On End")]
    public DialogTrigger endDtg;
    [Space]
    public string endAnimKey;

    public void Prepare() {
        if (moveCamera) {
            CameraFollow.CF.presentCameraSettings.target = point; //use camera settings please its better!
            CameraFollow.CF.presentCameraSettings.moveSpeed = 100f;// if mid coś to te linijke na dół wtedy cutscenka płynna, ale na start to tutaj także nwm jakiś bool może idk
        }

        PlayerController.playerController.freeze = freeze;
        PlayerController.playerController.shotable = shotable;
    }
    public void StartCutScene() {
        if (musicToPlay != "") AudioMenager.audioMenager.PlayMusic(musicToPlay);
        if (startDtg != null) startDtg.TriggerEvent();
        if (anim != null && startAnimKey != "") anim.SetTrigger(startAnimKey);

        if (point != null && endPos != null) LeanTween.moveLocal(point.gameObject, endPos.position, duration).setEase(curve);
        Invoke("EndCutScene", duration); //make sure dont to call another
    }
    public void EndCutScene() {
        //stop music
        if (endDtg != null) endDtg.TriggerEvent();
        if (anim != null && endAnimKey != "") anim.SetTrigger(endAnimKey);

        if (moveCamera) {
            CameraFollow.CF.ActualizeCameraSettings();
        }

        PlayerController.playerController.freeze = false;
        PlayerController.playerController.shotable = true;
    }
    /*
    private void Update() {
        if (!blocked) {
            if (timed > timer) {
                CameraFollow.camFollow.dynamicOffset = target * (1 - (timer / timed));
                timer += Time.deltaTime;
            }
            else if (timer != timed) {
                CameraFollow.camFollow.dynamicOffset = new Vector3(0f, 0f, 0f);
                timer = timed;

                Invoke("EndCutScene", cameraTransitionSpeed);
            }
            else Destroy(gameObject);

            if (Mathf.Abs(timer - 1) < 0.9f) CameraFollow.camFollow.sync = true;
        }
    }
    public void EndCutScene() {
        CameraFollow.camFollow.smoothSpeed = smoothSpeed;
        if (endDtg != null) endDtg.TriggerEvent();
    }
    public void StartCutScene() {
        AudioMenager.audioMenager.PlayMusic(musicToPlay);
        if (startDtg != null) startDtg.TriggerEvent();

        target = camSlideFrom;
        timer = 0;
        CameraFollow.camFollow.dynamicOffset = target * (1 - (timer / timed));

        smoothSpeed = CameraFollow.camFollow.smoothSpeed;
        CameraFollow.camFollow.smoothSpeed = cinematicSmoothSpeed;
        CameraFollow.camFollow.sync = false;
    }
    public void TriggerMovement() {
        blocked = false;
    }
    */
}

