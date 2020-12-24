using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMana : MonoBehaviour {
    [Range(0, 100)]
    public float health;
    [Range(0, 100)]
    public float mana;

    public float regenerationSpeed;

    Slider _hp;
    Slider _mana;
    private void Start() {
        _hp = transform.GetChild(0).GetComponent<Slider>();
        _mana = transform.GetChild(1).GetComponent<Slider>();

        Actualize();
    }
    private void Update() {
        GiveMANA(regenerationSpeed * Time.deltaTime);
    }
    public void Actualize() {
        _hp.value = health / 100;
        _mana.value = mana / 100;
    }
    //
    //HP
    //
    public bool GiveHP(float val) {
        health = Mathf.Clamp(health + val, 0, 100);
        Actualize();

        if (health == 0) return false;
        else return true;
    }
    /// <summary>
    /// It doesnt add or subtract HP, it only returns true or false depending on if player has higher amount of HP, or not
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public bool CheckHP(float val) {
        if (health + val < 0) return false;
        else return true;
    }
    //
    //MANA
    //
    public bool GiveMANA(float val) {
        mana = Mathf.Clamp(mana + val, 0, 100);
        Actualize();

        if (mana == 0) return false;
        else return true;
    }
    /// <summary>
    /// It doesnt add or subtract MANA, it only returns true or false depending on if player has higher amount of MANA, or not
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public bool CheckMANA(float val) {
        if (mana + val < 0) return false;
        else return true;
    }
}
