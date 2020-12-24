using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameEvent : EventHolder {

    [Space]
    public bool freezePlayer = true;
    public MGType MGT;
    public override void OurStart() {
        
    }
    public override void TriggerEvent() {
        PlayerController.playerController.freeze = freezePlayer;
        MGMenager.mgMenager.StartMiniGame(MGT);
    }
}
