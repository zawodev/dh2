using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : Item {

    //private PlayerController playerController;
    //private InHandItem handItem;
    //private CrossHair crossHair;

    public enum SoundType {
        rifle,
        sniper,
        pistol,
        shotgun,
    }
    [Header("GUN")]
    public Item ammo;

    public SoundType soundType;

    [Range(1f, 200f)]
    public float damage = 5f;

    [Range(5, 60)]
    public int maxAmmo = 30;

    [Range(0.01f, 4f)]
    public float timeBtwShots = 0.1f;
    float btwShots;
    [Range(0.5f, 4f)]
    public float reloadTime = 1f;
    float reload;
    [Range(0.5f, 4f)] //fsuhfidfhfiudfhgidigdf TUTAJ NAPISZ
    public float pickTime = 1f;
    float pick;

    [Space]
    [Range(1, 10)]
    public int bullets = 1;

    [Range(0f, 50f)]
    public float spread = 1f;
    [Range(5f, 50f)]
    public float moveSpread = 2f;
    [Range(30f, 40f)]
    public float bulletSpeed = 30f;

    [Space]
    [Range(0.05f, 2f)]
    public float playerRecoil = 1f;
    [Range(0.1f, 10f)]
    public float enemyRecoil = 1f;

    //[Space]
    //[Range(0f, 3f)]
    //public float camShakeLenght = .15f;
    //[Range(0f, 3f)]
    //public float camShakeStrength = .2f;

    private void Awake() {
        //handItem = FindObjectOfType<InHandItem>();
        //crossHair = FindObjectOfType<CrossHair>();
    }
    public override void Use() {
        AudioMenager.audioMenager.PlaySound(soundType.ToString());
        InHandItem.inHandItem.GenerateBullets(damage, (PlayerController.playerController.isMoving ? moveSpread : spread), bulletSpeed, enemyRecoil, bullets);
        InHandItem.inHandItem.GenerateFire(); //napraw te particle pls, teraz sa brzydkie moze jakis dym, albo cos nwm

        CameraShake.ShakeOnce(.2f, damage * bullets / 69f);
        PlayerController.playerController.GaveImpact(playerRecoil);


        isActive = PlayerPrefs.GetInt(name) > 0;
        //isActive = PlayerPrefs.GetInt(name) > 0 ? true : false;
    }
    public override void AlwaysUpdate() {

    }
    public override void ChosenUpdate() {
        if (reload > 0) reload -= Time.deltaTime;
        if (btwShots > 0) btwShots -= Time.deltaTime;


        //RELOAD
        if (Input.GetKeyDown(KeyCode.R)) {
            if (PlayerPrefs.GetInt(name) < maxAmmo && PlayerPrefs.GetInt(ammo.name) > 0) {
                Inventory.inventory.AddMany(this, maxAmmo);
                Inventory.inventory.Remove(ammo, 1);
                isActive = true;

                InHandItem.inHandItem.GenerateMagazine();

                //slowe player speed
                StopCoroutine(PlayerController.playerController.Frozer(.01f));
                StartCoroutine(PlayerController.playerController.Frozer(.6f));

                PlayerController.playerController.anim.SetTrigger("reload");
                CrossHair.crossHair.Off();

                reload = reloadTime;
            }
        }

        //AIMING
        if (Input.GetMouseButton(1)) {
            if (reload <= 0 && PlayerController.playerController.shotable) {
                CrossHair.crossHair.Onn();
                //SHOOTING
                if (Input.GetMouseButton(0)) {
                    if (btwShots <= 0) {
                        Inventory.inventory.Remove(this, 1);
                        Use();

                        btwShots = timeBtwShots;
                    }
                }
            }
        }

    }
}
