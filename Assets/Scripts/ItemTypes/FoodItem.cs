using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : Item {
    [Header("FOOD")]
    public float value;
    HealthMana healthMana;

    private void Start() {
        healthMana = FindObjectOfType<HealthMana>();
    }
    public override void Use() {
        PlayerController.playerController.anim.SetTrigger("eat");

        StopCoroutine(PlayerController.playerController.Frozer(.01f));
        StartCoroutine(PlayerController.playerController.Frozer(.6f));

        healthMana.GiveHP(value);
        healthMana.GiveMANA(value / 2);
    }
    public override void AlwaysUpdate() {

    }
    public override void ChosenUpdate() {
        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0)) {
            Inventory.inventory.Remove(this, 1);
            Use();
        }
    }
}
