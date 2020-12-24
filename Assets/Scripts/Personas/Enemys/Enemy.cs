using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public HitBox bodyHitBox;

    Animator anim;
    Rigidbody2D rb;
    Transform target;
    GameObject ragdoll;
    Vector2 direction;

    [Space]
    public float health;
    [Range(3f, 20f)]
    public float speed;

    public float minSeeDistance;
    public float maxSeeDistance;

    public bool seePlayer;

    //Vector3 bonus;

    private void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = PlayerController.playerController.transform;

        ragdoll = Resources.Load<GameObject>("Ragdoll/ragdoll2");
    }
    private void Update() {
        
    }
    private void FixedUpdate() {
        direction = Vector2.ClampMagnitude(target.position - transform.position, 10);

        if (direction.x > 0) transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        else if (direction.x < 0) transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

        if (target != null) {
            //rb.MovePosition((Vector2)transform.position + (KPos * speed * 0.01f));
            //rb.velocity = (direction * speed * 0.01f);

            if (direction.magnitude > 5f) {
                Move(direction, speed);
                anim.SetFloat("speed", speed);
            }
            else anim.SetFloat("speed", 0);
        }
    }
    public void Move(Vector2 velocity, float force) {
        velocity = velocity + velocity.normalized * 0.2f * rb.drag;
        force = Mathf.Clamp(force, -rb.mass / Time.fixedDeltaTime, rb.mass / Time.fixedDeltaTime);

        if (rb.velocity.magnitude == 0) {
            rb.AddForce(velocity * force, ForceMode2D.Force);
        }
        else {
            var vel = (velocity.normalized * Vector2.Dot(velocity, rb.velocity) / velocity.magnitude);
            rb.AddForce((velocity - vel) * force, ForceMode2D.Force);
        }
    }
    public void TakeDamage(float damage, Vector2 power) {
        health -= damage;
        anim.SetTrigger("hit");
        rb.AddForce(power, ForceMode2D.Force); //gave impact

        if (health <= 0) {
            health = 10000;
            Die(power);
        }
    }
    private void Die(Vector2 power) {
        //bonus = (transform.position.y < -3f ? new Vector3(0f, -transform.position.y - 3f, 0f) : new Vector3(0f, 0f, 0f));
        Rigidbody2D r = Instantiate(ragdoll, transform.GetChild(1).position + new Vector3(0f, 2f, 0f), Quaternion.identity).transform.GetChild(1).GetComponent<Rigidbody2D>();
        r.AddForce(power, ForceMode2D.Force);

        //PlayerSensors.playerSensors.DisableHitBox(bodyHitBox);
        Destroy(gameObject);
    }
}
