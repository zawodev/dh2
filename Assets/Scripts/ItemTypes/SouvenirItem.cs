using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouvenirItem : Item {
    //CrossHair crossHair;
    //InHandItem handItem;

    [Header("SOUVENIR")]
    public bool flashLight;
    private void Awake() {
        //crossHair = FindObjectOfType<CrossHair>();
        //handItem = FindObjectOfType<InHandItem>();
    }
    public override void Use() {
        InHandItem.inHandItem.flashLightObject.SetActive(true);
    }
    public override void AlwaysUpdate() {
        
    }
    public override void ChosenUpdate() {
        if (flashLight && Input.GetMouseButton(1)) {

            CrossHair.crossHair.Onn();

            if (Input.GetMouseButton(0)) Use();
            if (Input.GetMouseButtonUp(0)) InHandItem.inHandItem.flashLightObject.SetActive(false);
        }
    }
}
