using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSensors : MonoBehaviour {

    public static PlayerSensors playerSensors;

    [HideInInspector]
    public bool haveEnemiesInRange;
    [HideInInspector]
    public List<Transform> enemys;

    private void Awake() {
        if (playerSensors != null) {
            Destroy(gameObject);
            return;
        }
        else playerSensors = this;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            Transform newEnemy = collision.transform;
            if (newEnemy != null) {
                enemys.Add(newEnemy);
                haveEnemiesInRange = enemys.Count > 0;
            }
            else {
                Debug.LogError("There should be enemy there lol");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            Transform oldEnemy = collision.transform;
            if (oldEnemy != null) {
                enemys.Remove(oldEnemy);
                haveEnemiesInRange = enemys.Count > 0;
            }
            else {
                Debug.LogError("There should be enemy there lol");
            }
        }
    }
    public Transform GetClosestEnemy() {
        Transform closestEnemy = null;
        float bestDist = float.MaxValue;
        foreach (Transform enemy in enemys) {
            float newDist = Vector2.Distance(enemy.position, transform.position);
            if (newDist < bestDist) {
                bestDist = newDist;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}
