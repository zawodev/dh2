using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CableEnd : MonoBehaviour {

    public CablesMG CMG;
    public int index;
    public void StartActualizeColor() {
        Image img = GetComponent<Image>();
        img.color = (index == 0 ? Color.red : index == 1 ? Color.blue : index == 2 ? Color.green : Color.yellow);
    }
    public void PointOnn() {
        CMG.cableEnd = this;
    }
    public void PointOff() {
        CMG.cableEnd = null;
    }
}
