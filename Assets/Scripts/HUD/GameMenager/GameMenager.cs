using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenager : MonoBehaviour {

    public static GameMenager gameMenager;

    public float transitionTime = 1;
    public float transitionBonus = 1;

    Image loadingCircle;
    TextMeshProUGUI hint;
    public List<string> hints;

    GameObject panel;
    public Vector2 closePos;

    private void Awake() {
        if (gameMenager != null) {
            Destroy(gameObject);
            return;
        }
        else gameMenager = this;
    }
    private void Start() {
        panel = LoadingInstance.LI.gameObject;
        //closePos = panel.transform.localPosition;

        loadingCircle = panel.transform.GetChild(0).GetComponent<Image>();
        hint = panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        Invoke(nameof(SceneEnterTransition), transitionBonus);
    }
    public void SceneExitTransition() {
        LeanTween.moveLocalX(panel, 0, transitionTime);
    }
    public void SceneEnterTransition() {
        LeanTween.moveLocalX(panel, closePos.x, transitionTime);
    }
    public void ShowHint() {
        if (hints.Count > 0) hint.text = hints[Random.Range(0, hints.Count)];
        LeanTween.rotate(loadingCircle.gameObject, new Vector3(0, 0, 3000f), 10f);
    }
}
