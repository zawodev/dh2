using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    #region Singleton
    public static ItemInfo instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Znaleziono wiecej niz jeden instance w iteminfo!");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject panel;
    public Image img;
    public Text title;
    public Text desc;
    public Text cena;
    public Text value;

    private void Start()
    {
        panel.SetActive(false);
    }
    public void TurnOn(Sprite newSprite, string newString, string description, int price, int quelit)
    {
        panel.SetActive(true);

        img.sprite = newSprite;
        title.text = newString;

        desc.text = description;
        cena.text = "wartość: " + price.ToString() + "$";
        if (quelit == 0) value.text = "";
        else value.text = "jakość: " + quelit.ToString() + "p"; ;
    }
    public void TurnOff()
    {
        panel.SetActive(false);

        img.sprite = null;
        title.text = null;

        desc.text = null;
        cena.text = null;
        value.text = null;
    }
}