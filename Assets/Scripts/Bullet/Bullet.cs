using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Rigidbody2D rb;
    public GameObject effect;
    public GameObject bulletHole;

    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float enemyRecoil;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    HitBox hitBox;
    HideObject hideObject;
    Explosive explosive;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (hitBox == null) {
            hitBox = collision.GetComponent<HitBox>();
            if (hitBox != null) {
                hitBox.DamageEnemy(damage, enemyRecoil, rb.velocity);
                Instantiate(effect, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
        }

        hideObject = collision.GetComponent<HideObject>();
        if (hideObject != null) {
            if(rb.velocity.x > 0 && hideObject.StopBulletLeft()) {
                Instantiate(bulletHole, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if (rb.velocity.x < 0 && hideObject.StopBulletRight()) {
                Instantiate(bulletHole, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        explosive = collision.GetComponent<Explosive>();
        if (explosive != null) {
            explosive.Explode(transform.position, rb.velocity);
        }
    }
}
