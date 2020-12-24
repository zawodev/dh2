using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour {

    //PlayerController playerController;

    [Tooltip("onn = wall exist while player hid, off = wall exist always from sides as below")]
    public bool whileHidOnly = true;
    [Space]
    public bool wallFromLeft = true;
    public bool wallFromRight = true;

    public bool StopBulletLeft() {
        if (!whileHidOnly || PlayerController.playerController.sithid) return wallFromLeft;
        else return false;
    }
    public bool StopBulletRight() {
        if (!whileHidOnly || PlayerController.playerController.sithid) return wallFromRight;
        else return false;
    }
}
