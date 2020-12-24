using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SquareControll : MonoBehaviour {

    Rigidbody2D rb;
    float movement, rotament;

    [Range(.1f, 10f)]
    public float speed = 10f;
    [Range(.1f, 10f)]
    public float jumpPower = 10f;

    [HideInInspector]
    public int jump;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update() {
        movement = Input.GetAxis("Horizontal");
        rotament = Input.GetAxis("Rotational");

        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
    }
    void FixedUpdate() {
        rb.velocity = (new Vector2(movement * speed, rb.velocity.y));
    }
    public void Jump() {
        if (jump > 0) 
            rb.AddForce(new Vector2(0f, jumpPower - rb.velocity.y), ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            jump++;
            if (Input.GetButton("Jump")) Jump();
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            jump--;
        }
    }
}
