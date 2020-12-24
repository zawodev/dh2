using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour {

    public static CrossHair crossHair;

    [HideInInspector]
    public Vector2 cursorPos;
    Vector2 childPos;
    Sprite cel;

    Transform ArmL;
    Transform ArmR;
    Transform child0;

    [HideInInspector]
    public bool IsTargeting;
    [HideInInspector]
    public bool right;
    private void Awake() {
        if (crossHair != null) {
            Destroy(gameObject);
            return;
        }
        else crossHair = this;
        DontDestroyOnLoad(gameObject);

        cel = GetComponent<SpriteRenderer>().sprite;
        ArmL = PlayerController.playerController.transform.GetChild(0).GetChild(2);
        ArmR = PlayerController.playerController.transform.GetChild(0).GetChild(3);

        if (ArmL.name != "lewalapa" || ArmR.name != "prawalapa") Debug.LogError("Change child order or smth idk");
    }
    private void Start() {
        child0 = ArmR.GetChild(0).transform;
        right = true;
        Off();
    }
    private void Update() {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
        if (IsTargeting) {
            childPos = child0.localPosition;
            if(right) childPos.x = -childPos.x;

            ArmR.position = cursorPos - childPos;
        }
        else {
            ArmR.localPosition = new Vector3(0, 0, 0);
        }

        if (Input.GetMouseButtonUp(1) || Input.GetKeyDown(KeyCode.LeftShift)) {
            Off();
        }
    }
    public void Onn() {
        IsTargeting = true;
        InHandItem.inHandItem.IsTargeting = true;

        Cursor.visible = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = cel;
    }
    public void Off() {
        IsTargeting = false;
        InHandItem.inHandItem.IsTargeting = false;

        Cursor.visible = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
    }
}
