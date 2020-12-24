using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : EventHolder {
    [Header("START INFO")]
    public CutScene startCutScene;

    [Header("END INFO")]
    public CutScene endCutScene;
    public DialogTrigger option1;
    public string answer1;

    public DialogTrigger option2;
    public string answer2;
    [Header("TALKER INFO")]
    public string _name;
    [Space]
    public Sprite Happy;
    public Sprite Scared;
    public Sprite Sad;

    public bool isContinuated;

    [Space]
    public bool freeze = true;
    public bool shotable = false;
    public bool turnable = false;

    public Dialog[] sentences;

    public override void OurStart() {
        
    }
    public override void TriggerEvent() {
        if (autoTrigger) block = true;
        autoTrigger = false;

        if (startCutScene != null) startCutScene.StartCutScene();
        DialogSystem.dialogSystem.StartConservation(this);
    }
}
