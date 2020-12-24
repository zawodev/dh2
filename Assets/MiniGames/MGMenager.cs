using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGMenager : MonoBehaviour {

    public static MGMenager mgMenager;
    [Range(0.1f, 2f)]
    public float animationSpeed = 1f;
    public bool isPlayin;//public for debuging purpose only

    [HideInInspector]
    public MiniGame[] minigames;

    private void Awake() {
        if (mgMenager != null) {
            Destroy(gameObject);
            return;
        }
        else mgMenager = this;
    }
    private void Start() {
        minigames = Resources.LoadAll<MiniGame>("MiniGames");
    }
    public void MoveShowPanel(bool state) {
        isPlayin = state;
        if (state) { //from off to onn
            LeanTween.moveLocalY(gameObject, 0f, animationSpeed);
        }
        else {       //from onn to off
            LeanTween.moveLocalY(gameObject, -469f, animationSpeed);
        }
    }
    MiniGame worldMG;
    public void StartMiniGame(MGType mgt) {
        if (!isPlayin) {
            foreach (MiniGame mg in minigames) {
                if (mg.MGT.type == mgt.type) {
                    worldMG = Instantiate(mg, transform);
                    worldMG.MiniGamePrepare();
                    MoveShowPanel(true);
                    //Debug.Log("Mini Game found succesfuly");
                    return;
                }
            }
            Debug.LogError("Mini Game not found");
        }
        else {
            Debug.LogError("Mini Game played while playing other minigame!");
        }
    }
    public float EndMiniGame() {
        MoveShowPanel(false);
        worldMG = null;
        PlayerController.playerController.freeze = false;

        return animationSpeed;
    }
}
