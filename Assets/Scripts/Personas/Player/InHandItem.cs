using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class InHandItem : MonoBehaviour {

    public static InHandItem inHandItem;
    Camera _camera;
    Transform crosshair;

    [HideInInspector]
    public Transform firePoint;
    [HideInInspector]
    public GameObject flashLightObject;
    [HideInInspector]
    public GameObject swordTrailObject;
    [HideInInspector]
    public ParticleSystem magazineGenerator;
    [HideInInspector]
    public ParticleSystem fireGenerator;

    Transform player;
    Vector2 playerCenter;

    public GameObject bullet;
    public GameObject gunFire;

    [HideInInspector]
    public bool IsTargeting;
    [HideInInspector]
    public bool right;

    private void Awake() {
        if (inHandItem != null) {
            Destroy(gameObject);
            return;
        }
        else inHandItem = this;
    }
    private void Start() {
        _camera = CameraFollow.CF.GetComponent<Camera>();
        player = PlayerController.playerController.transform;
        crosshair = CrossHair.crossHair.transform;

        firePoint = transform.GetChild(0);
        fireGenerator = firePoint.GetComponent<ParticleSystem>();
        flashLightObject = transform.GetChild(1).gameObject;
        swordTrailObject = transform.GetChild(2).gameObject;
        magazineGenerator = transform.GetChild(3).gameObject.GetComponent<ParticleSystem>();

        flashLightObject.SetActive(false);
    }
    void Update() {
        if (IsTargeting) {
            playerCenter = _camera.WorldToScreenPoint(player.position);
            transform.right = (Vector2)(crosshair.position - transform.position);

            if (Input.mousePosition.x < playerCenter.x)
                transform.Rotate(180f, 0f, 0f);
        }
    }
    public void UpdateFirePoint(Vector3 newPos) {
        firePoint.localPosition = newPos;
    }
    public void GenerateBullets(float _damage, float _spread, float _speed, float _enemyRecoil, int _bulletCount) {
        GameObject _gunFire = Instantiate(gunFire, firePoint.position, firePoint.rotation);
        _gunFire.GetComponent<Light2D>().intensity = Mathf.Clamp(_bulletCount, 1, 3);
        for (int i = 0; i < _bulletCount; i++) {
            Quaternion q = Random.rotation;
            GameObject _bullet = Instantiate(bullet, firePoint.position, firePoint.rotation);

            _bullet.transform.rotation = Quaternion.RotateTowards(_bullet.transform.rotation, q, _spread);
            _bullet.GetComponent<Bullet>().damage = _damage;
            _bullet.GetComponent<Bullet>().enemyRecoil = _enemyRecoil;
            _bullet.GetComponent<Bullet>().speed = _speed;
        }
    }
    public void GenerateMagazine() {
        magazineGenerator.Emit(1);
    }
    public void GenerateFire() {
        fireGenerator.Play();
    }
}
