using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int amount = 1;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Inventory.inventory.AddMany(Resources.Load<Item>("Items/Souvenir/coin"), amount);
            AudioMenager.audioMenager.PlaySound("coin_collect");
            Destroy(gameObject);
        }
    }
}
