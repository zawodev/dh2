using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour {

    [HideInInspector]
    public Item item;

    public Sprite[] bckg;
    public Image icon;
    public TextMeshProUGUI value;

    private void Start() {
        gameObject.GetComponent<Image>().sprite = bckg[Random.Range(0, bckg.Length)];
    }
    public void AddItem(Item newItem) {
        item = newItem;

        icon.sprite = item.gameObject.GetComponent<SpriteRenderer>().sprite;
        icon.enabled = true;
        value.text = PlayerPrefs.GetInt(item.name).ToString();
        value.enabled = true;
    }
    public void ClearSlot() {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        value.text = null;
        value.enabled = false;
    }
    public void UseItem() {
        if (item != null) {
            item.Use();
        }
    }
    /*public void OnMouseOver()
    {
        if (item != null) ItemInfo.instance.TurnOn(item.icon, item.name, item.description, item.price, item.quality);
    }
    public void OnMouseExit()
    {
        if (item != null) ItemInfo.instance.TurnOff();
    }/*/
}