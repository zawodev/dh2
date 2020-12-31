using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventHolder : MonoBehaviour {

    SpriteRenderer avatar;
    Sprite onn;
    Sprite off;

    //tutaj do konfiguracji wszystkie
    [Header("Event Holder")]
    public bool autoTrigger;
    public bool interactable;

    //hidem oznaczamy pomocnicze, one dzialaja automatycznie przy roznych eventach swiata
    [HideInInspector]
    public bool priorityComeBack;

    [HideInInspector]
    public bool block; // pernament block, one time use, may needs player prefs? (for fabula and shit)

    bool inPlace;

    public void OnStart() {
        if (interactable) {
            avatar = transform.GetChild(0).GetComponent<SpriteRenderer>();
            onn = Resources.Load<Sprite>("HUD/onn");
            off = Resources.Load<Sprite>("HUD/off");

            ChangeSprite(off);
        }
        else if (!autoTrigger) { //&&!interactible
            gameObject.SetActive(false);
        }
    }
    private void Start() {
        OnStart();
        OurStart();
    }
    public abstract void OurStart();
    public abstract void TriggerEvent();
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            ChangeSprite(onn);
            inPlace = true;
        }
    }
    private void Update() { //uwaga na tego player freeze w środku! w razie problemow zakomentuj i tyle :p
        if (!block && !PlayerController.playerController.freeze && (inPlace || priorityComeBack)) {
            if (autoTrigger)
                TriggerEvent();
            else if (interactable) {
                if (Input.GetKeyDown(KeyCode.E)) TriggerEvent();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            ChangeSprite(off);
            inPlace = false;
        }
    }

    void ChangeSprite(Sprite _sprite) {
        if (avatar != null) avatar.sprite = _sprite;
    }
}
