using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController playerController;

    public Animator anim;
    public Transform orderPoint; //używany przez inne skrypty

    Camera _camera;
    public Rigidbody2D rb;
    HealthMana healthMana;

    Vector2 movement;
    Vector2 crouchment;

    [Space]
    public float sneakSpeed = 2;
    public float rollOverSpeed = 3;
    public float normalSpeed = 5;
    public float sprintSpeed = 12;
    float speed;

    [Space]
    public float sprintCost;
    public float rollOverCost;
    [Range(1f, 3f)]
    public float spreadPowerBonus;
    [HideInInspector]
    public bool isMoving;

    [Space]
    public bool freeze; //just freeze maybe?
    public bool sithid;

    [HideInInspector]
    public bool shotable;
    [HideInInspector]
    public bool sprintable;
    [HideInInspector]
    public bool barrelrolling;

    bool facingRight;
    bool sneaking;
    bool turnable;

    private void Awake() {
        if (playerController != null) {
            Destroy(gameObject);
            return;
        }
        else playerController = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start() {
        _camera = CameraFollow.CF.GetComponent<Camera>();
        healthMana = FindObjectOfType<HealthMana>();
        facingRight = true;
        anim.SetBool("sit", sithid);

        if (!sithid) {
            shotable = true;
            turnable = true;
        }
    }
    void Update() {
        if (!freeze) {
            movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (turnable) {
                if (Input.GetAxis("Horizontal") > 0) RotatePlayer(180f);
                else if (Input.GetAxis("Horizontal") < 0) RotatePlayer(0f);
                else isMoving = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift)) sprintable = true;

            if (Input.GetMouseButtonDown(1)) sneaking = true;
            if (Input.GetMouseButtonUp(1)) sneaking = false;
            anim.SetBool("sneaking", sneaking);

            if (!barrelrolling) {

                //SPEED
                if (sithid) {
                    speed = 0;
                }
                else if (Input.GetMouseButton(1)) {
                    speed = sneakSpeed;
                }
                else if (Input.GetKey(KeyCode.LeftShift) && sprintable) {
                    if (!healthMana.GiveMANA(-sprintCost * Time.deltaTime)) sprintable = false;
                    speed = sprintSpeed;
                }
                else {
                    speed = normalSpeed;
                }

                //BARRELROLL
                if (!sneaking && Input.GetKeyDown(KeyCode.LeftControl)) {
                    if (healthMana.CheckMANA(-rollOverCost)) {

                        healthMana.GiveMANA(-rollOverCost);

                        crouchment = new Vector2(rollOverSpeed * (facingRight ? 1 : -1), 0);
                        anim.SetTrigger("rollover");

                        barrelrolling = true;
                        speed = normalSpeed;
                        turnable = false;

                        Invoke("ResetCrouch", 1.1f);
                    }
                }
            }
        }
    }
    void ResetCrouch() {
        crouchment = new Vector2(0, 0);
        barrelrolling = false;
        turnable = true;
    }
    private void FixedUpdate() {
        Move(10f * (movement + crouchment), speed);
        anim.SetFloat("speed", Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical"))) * speed / normalSpeed);
    }
    public void Move(Vector2 velocity, float force) {
        velocity += velocity.normalized * 0.2f * rb.drag;
        force = Mathf.Clamp(force, -rb.mass / Time.fixedDeltaTime, rb.mass / Time.fixedDeltaTime);

        if (velocity.magnitude == 0) {
            rb.AddForce(velocity * force, ForceMode2D.Force);
        }
        else {
            Vector2 vel = velocity.normalized * Vector2.Dot(velocity, rb.velocity) / velocity.magnitude;
            rb.AddForce((velocity - vel) * force, ForceMode2D.Force);
        }
    }
    public void RotatePlayer(float y) {
        isMoving = true;

        transform.rotation = new Quaternion(0f, y, 0f, 0f);
        facingRight = (y == 180 ? true : false);
        CrossHair.crossHair.right = facingRight;
    }
    public void GaveImpact(float power) {
        //Vector2 playerCenter = _camera.WorldToScreenPoint(transform.position);
        Vector2 crossHair = CrossHair.crossHair.cursorPos;
        if (!sithid) rb.AddForce(((Vector2)transform.position - crossHair).normalized * power * 1000f, ForceMode2D.Force);
    }

    public bool AnimateSitHid(string _sithid) {
        sithid = !sithid;
        anim.SetBool(_sithid, sithid);

        return sithid;
    }

    Vector2 h1;
    Vector2 h2;
    public Vector2 SwapPose(Vector2 temp) {
        h1 = temp;
        h2 = rb.position;
        rb.position = h1;
        return h2;
    }
    public void SetPose(Vector2 temp) {
        rb.position = temp;
    }

    public IEnumerator Frozer(float timer) {
        freeze = true;
        yield return new WaitForSeconds(timer);
        freeze = false;
    }
    public void ToggleShotable(bool state) {
        shotable = state;
    }
    public void JumpTowards(Vector2 target) {

    }

    //public void MoveCharacter(Vector2 direction) {
    //    if (!freeze && !freeze2 && !sit && !hid) {
    //        rb.MovePosition((Vector2)transform.position + (direction * speed * 0.01f));
    //    }
    //    anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal") * speed));
    //}
}