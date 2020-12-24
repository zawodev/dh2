using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cable : MonoBehaviour {

    public bool isDone;
    public Image img;
    public CablesMG CMG;
    public LineRendererHUD LR;
    Vector2 startPos;
    public int index;

    public void Actualize() {
        startPos = LR.points[0];
        LR.color = (index == 0 ? Color.red : index == 1 ? Color.blue : index == 2 ? Color.green : Color.yellow);
        img.color = LR.color;

        LR.SetPosition(1, startPos);
    }
    public void DragCable() {
        if (!isDone) {
            CMG.cable = this;
            LR.SetPosition(1, new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height));
        }
    }
    public void EndDragCable() {
        if (!isDone) {
            CMG.cable = null;
            if (CMG.cableEnd != null && index == CMG.cableEnd.index) {
                isDone = true;
                CMG.AddPoint();
                LR.SetPosition(1, new Vector2(.85f, (CMG.cableEnd.transform.localPosition.y + 225f) / 450f));
            }
            else {
                LR.SetPosition(1, startPos);
            }
        }
    }
}
