using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeEvent : EventHolder {

    [Header("Scene Change")]
    [Range(0, 13)]
    public int port;
    public string sceneName;

    float transitionTime;

    public override void OurStart() {
        transitionTime = GameMenager.gameMenager.transitionTime;
        if (port != 0 && port == PlayerPrefs.GetInt("sceneport")) {
            PlayerController.playerController.SetPose(transform.position);
            PlayerPrefs.SetInt("sceneport", 0);
        }
    }
    public override void TriggerEvent() {
        if (sceneName != "") StartCoroutine(Fader());
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

        SceneManager.LoadScene(sceneName);
    }
}
