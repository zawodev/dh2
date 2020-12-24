using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpEvent : EventHolder {

    [Header("ItemPickUp")]
    public Item item;
    public int amount;

    public float farness;
    public float frequency;

    public bool infinity;

    GameObject child1; //child 0 first ofc, so this is second

    public override void OurStart() {
        child1 = transform.GetChild(1).gameObject;
        LeanTween.moveLocalY(child1, child1.transform.localPosition.y + farness, frequency).setEaseInOutQuad().setLoopPingPong();

        child1.GetComponent<SpriteRenderer>().sprite = item.inHand;
    }
    public override void TriggerEvent() {
        Inventory.inventory.AddMany(item, amount);

        if (!infinity) Destroy(gameObject);
    }
    public void ActualizeItem(Item _item, int _amount) {
        item = Resources.Load<Item>("Items/" + _item.type.ToString() + "/" + _item.name);
        amount = _amount;

        //OnStart();
    }
}
