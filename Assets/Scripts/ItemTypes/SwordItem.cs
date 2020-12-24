using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItem : Item {

    HealthMana healthMana;

    [Header("SWORD")]
    public GameObject effect;

    public float damage = 2.3f;
    public float energyCost = 4f;

    private void Start() {
        healthMana = FindObjectOfType<HealthMana>();
    }
    public override void Use() {
        if (healthMana.CheckMANA(-energyCost)) {
            PlayerController.playerController.anim.SetTrigger("sword");
            healthMana.GiveMANA(-energyCost);
        }
    }
    public override void AlwaysUpdate() {
        //if (btwHits > 0) btwHits -= Time.deltaTime;
    }
    public override void ChosenUpdate() {

        if (Input.GetMouseButtonDown(0)) {
            if (PlayerSensors.playerSensors.haveEnemiesInRange /*only if player facing right way by hitbox do it*/) {
                Transform enemy = PlayerSensors.playerSensors.GetClosestEnemy();
                PlayerController.playerController.JumpTowards(enemy.position);
                enemy.GetComponent<Enemy>().TakeDamage(damage, Vector2.zero);


                //znajdz, nakieruj gracza, animacja ciosu, obrażenia z shaderem białym
            }
            else {
                //uderzenie bez sensu
            }
        }
    }
}
