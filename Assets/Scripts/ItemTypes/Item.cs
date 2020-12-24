using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    public bool isActive;
    [HideInInspector]
    public bool isChosen;
    public bool canOnShift;
    public enum Type {
        Gun, Souvenir, Sword, Food
    }
    private void OnEnable() {
        isActive = true;
        if (generateFromCalculations) {
            firePointPos += new Vector3(Mathf.Clamp((64 - inHand.pivot.x) / 5f, 0f, 100f), 0f, 0f);
        }
    }

    [Header("ITEM")]
    public bool isInfinitiv;
    public new string name;
    public Type type;

    [Space]
    public Sprite inHand;
    public bool generateFromCalculations = true;
    public Vector3 firePointPos;

    public abstract void Use();

    /*
    //LPM
    public abstract void LPMStart();
    public abstract void LPMEnd();
    public abstract void LPMHold();

    //PPM
    public abstract void PPMStart();
    public abstract void PPMEnd();
    public abstract void PPMHold();
    */

    public abstract void ChosenUpdate();
    public abstract void AlwaysUpdate();
    
    private void Update() {
        if (isActive && isChosen && (canOnShift || !Input.GetKey(KeyCode.LeftShift))) ChosenUpdate();
        AlwaysUpdate();
    }
}
