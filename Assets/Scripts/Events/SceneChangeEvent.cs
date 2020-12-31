using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeEvent : EventHolder {

    [Header("Scene Change")]
    [Range(-1, 13)]
    public int port = -1;

    /*
    [Range(-1, 9)][HideInInspector]
    public int sceneIndex = -1;
    */

    public enum SceneName { None, Menu, Garden, House, Home, KarkowD, KarkowG, Bar, PathHQ, HQ};
    public SceneName sceneName;

    float transitionTime;

    public override void OurStart() {
        transitionTime = GameMenager.gameMenager.transitionTime;
        if (port != -1 && port == PlayerPrefs.GetInt("sceneport")) {
            PlayerController.playerController.SetPose(transform.position);
            PlayerPrefs.SetInt("sceneport", 0);
        }
    }
    public override void TriggerEvent() {
        if (sceneName != 0) StartCoroutine(Fader());
        else Debug.LogWarning("There is no scene name in " + gameObject.name);
    }
    IEnumerator Fader() {
        //before
        GameMenager.gameMenager.ShowHint();
        GameMenager.gameMenager.SceneExitTransition();
        //CameraFollow.CF.OnChangeScene(); // this dont work lol

        yield return new WaitForSeconds(transitionTime);
        //after
        PlayerPrefs.SetInt("sceneport", port);
        //CameraFollow.CF.ActualizeCameraSettings(); // this dont work lol

        if (sceneName != 0) SceneManager.LoadScene((int)sceneName - 1);
    }
}
