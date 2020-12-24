using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : EventHolder {

    [Header("Door Event")]
    public bool passwordProtected;
    public bool reOpenable;

    public SpriteRenderer door;
    public Sprite openedDoor;
    public Sprite closedDoor;

    public Collider2D wall;

    //true = open, false = closed, ez;
    bool state;

    public bool F;
    public Transform poss;
    Vector3 pos;

    public override void OurStart() {
        if (closedDoor == null) closedDoor = door.sprite;

        //if (F) pos = door.gameObject.GetComponent<OrderController>().point2.position;
    }
    public override void TriggerEvent() {
        switch (state) {
            case true:
                if (!passwordProtected) {
                    door.sprite = closedDoor;
                    //door.gameObject.GetComponent<OrderController>().point2.position = pos;

                    wall.enabled = true;
                }
                break;
            case false:
                if (!passwordProtected) {
                    door.sprite = openedDoor;
                    //door.gameObject.GetComponent<OrderController>().point2.position = poss.position;

                    wall.enabled = false;
                }
                break;
        }
        if (reOpenable) state = !state;
    }
}
