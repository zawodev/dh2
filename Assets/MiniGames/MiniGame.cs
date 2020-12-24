using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.U2D.IK;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class MiniGame : MonoBehaviour {

    public MGType MGT;

    [HideInInspector]
    public bool isPlaying;

    [Space]
    public int points;
    public int pointsGoal;

    public void AddPoint() {
        points++;
        if (points >= pointsGoal) AddAllPoints();
    }
    public void AddAllPoints() {
        Invoke(nameof(DestroyThisObject), MGMenager.mgMenager.EndMiniGame()); //yee
    }
    public abstract void MiniGamePrepare();
    public abstract void MiniGameFinish();
    void DestroyThisObject() {
        MiniGameFinish();
        Destroy(gameObject);
    }
}
