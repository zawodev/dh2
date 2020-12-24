using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItem : MonoBehaviour {
    public void Give(Item item) {
        Inventory.inventory.AddMany(item, 10);
    }
    public void Steel(Item item) {
        Inventory.inventory.Remove(item, 10);
    }
}
