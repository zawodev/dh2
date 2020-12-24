using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    Button[] buttons = new Button[4];

    public CutScene cutScene;
    //public DialogTrigger dtg;

    private void Awake() {
        cutScene.Prepare();
        for(int i = 0; i<4; i++) buttons[i] = transform.GetChild(i).GetComponent<Button>();
    }
    public void Play() {
        cutScene.StartCutScene();
        //dtg.TriggerEvent();

        DeactivateAllButtons();
    }
    public void Options() {

    }
    public void Credits() {

    }
    public void Quit() {

    }

    public void DeactivateAllButtons() {
        for (int i = 0; i < 4; i++) buttons[i].interactable = false;
    }
}
