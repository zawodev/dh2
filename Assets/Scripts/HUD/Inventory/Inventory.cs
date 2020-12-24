using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public static Inventory inventory;
    public GameObject itemPickUp;

    SpriteRenderer handItemSprite;
    //public Sprite[] on;
    //public Sprite[] off;
    public Color onnn;
    public Color offf;

    [Range(0, 8)] public int chose;
    public Item[] items;
    public List<Item> myItems = new List<Item>();
    public List<Slot> slots = new List<Slot>();

    private void Awake() {
        if (inventory != null) {
            Destroy(gameObject);
            return;
        }
        else inventory = this;
    }
    public void Start() {
        handItemSprite = InHandItem.inHandItem.GetComponent<SpriteRenderer>();
        items = Resources.LoadAll<Item>("Items");
        for (int i = 0; i < items.Length; i++) {
            int value = PlayerPrefs.GetInt(items[i].name);
            if (value > 0 || items[i].isInfinitiv && PlayerPrefs.GetInt(items[i].name + "inf") == 1) {
                CreateInWorld(items[i]);
            }
        }
        UpdateUI();
        UpdateScroll();
    }
    void CreateInWorld(Item item) {
        myItems.Add(Instantiate(item, slots[myItems.Count].transform));
        //item.isActive = true;

        if (item.isInfinitiv) PlayerPrefs.SetInt(item.name + "inf", 1);
    }
    public void DropItem(Item item) {
        int amount = PlayerPrefs.GetInt(item.name);

        GameObject itemPickUpEvent = Instantiate(itemPickUp, PlayerController.playerController.transform.position, Quaternion.identity);
        itemPickUpEvent.GetComponent<ItemPickUpEvent>().ActualizeItem(item, amount);

        Remove(item, amount, true);
    }
    public void AddMany(Item item, int value) {
        //pick up sound
        item.isActive = PlayerPrefs.GetInt(item.name) + value > 0 ? true : false;
        if (PlayerPrefs.GetInt(item.name) == 0) {
            CheckAdd(item);
        }
        PlayerPrefs.SetInt(item.name, PlayerPrefs.GetInt(item.name) + value);
        UpdateUI();
        UpdateScroll();
    }
    public void Remove(Item item, int value, bool pernament = false) {

        item.isActive = PlayerPrefs.GetInt(item.name) + value > 0 ? true : false;

        int amount = PlayerPrefs.GetInt(item.name);
        if (amount >= value) {
            if (amount == value) {
                item.isChosen = false;
                //item.isActive = false;

                if (!item.isInfinitiv || pernament)
                    myItems.Remove(item);
            }

            PlayerPrefs.SetInt(item.name, amount - value);

            CheckRemove();
            UpdateUI();
            UpdateScroll();
        }
    }
    public void CheckAdd(Item item) {
        for (int i = 0; i < myItems.Count; i++) {
            if (myItems[i].name == item.name) {
                //myItems[i].isActive = true;
                return;
            }
        }
        CreateInWorld(item);
    }
    public void CheckRemove() {
        for (int i = 0; i < myItems.Count; i++) {
            if (PlayerPrefs.GetInt(myItems[i].name) == 0 && !myItems[i].isInfinitiv) myItems.RemoveAt(i);
        }
    }
    public void UpdateUI() {
        for (int i = 0; i < slots.Count; i++) {
            if (i < myItems.Count) {
                slots[i].AddItem(myItems[i]);
            }
            else {
                slots[i].ClearSlot();
            }
        }
    }
    public void UpdateScroll() {
        for (int i = 0; i <= 8; i++) {
            if (i == chose) {
                //slots[i].gameObject.GetComponent<Image>().sprite = on;
                slots[i].transform.GetChild(0).GetComponent<Image>().color = onnn;
                if (i < myItems.Count) {
                    myItems[i].isChosen = true;
                    handItemSprite.sprite = myItems[i].inHand;

                    InHandItem.inHandItem.UpdateFirePoint(myItems[i].firePointPos);
                }
                else handItemSprite.sprite = null;
            }
            else {
                //slots[i].gameObject.GetComponent<Image>().sprite = off;
                slots[i].transform.GetChild(0).GetComponent<Image>().color = offf;
                if (i < myItems.Count) myItems[i].isChosen = false;
            }
        }
    }
    private void Update() {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            if (chose > 0) chose--;
            else chose = 8;

            UpdateScroll();
            CrossHair.crossHair.Off();
            UpperBar.UB.ToggleBar(true);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            if (chose < 8) chose++;
            else chose = 0;

            UpdateScroll();
            CrossHair.crossHair.Off();
            UpperBar.UB.ToggleBar(true);
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (myItems.Count > chose) DropItem(myItems[chose]);
        }
    }
}
