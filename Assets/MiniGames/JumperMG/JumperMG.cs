using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperMG : MiniGame {

    public GameObject game;
    GameObject _game;

    public override void MiniGamePrepare() {
        _game = Instantiate(game);
    }
    public override void MiniGameFinish() {
        Destroy(_game);
    }
}
