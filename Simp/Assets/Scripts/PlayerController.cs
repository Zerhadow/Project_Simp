using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpd = 6f;
    public PlayerInputs playerControls;

    Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction melee;
    private InputAction shoot;
    private InputAction dash;

    public GameObject projectilePrefab;
    Vector2 lookDirection = new Vector2(1,0);

    public float shootCooldown = 0.3f;
    public float dashCooldown = 0.3f;
    bool canCast = true, canDash = true, isDashing = false;

    public float dashPwr = 24f;
    float dashingTime = 0.2f;

    public TrailRenderer tr;

    private void Awake() {
        playerControls = new PlayerInputs();
    }

    private void OnEnable() {
        move = playerControls.Player.Move;
        move.Enable();

        melee = playerControls.Player.Melee;
        melee.Enable();
        melee.performed += Melee;

        shoot = playerControls.Player.Shoot;
        shoot.Enable();
        shoot.performed += Shoot;

        dash = playerControls.Player.Dash;
        dash.Enable();
        dash.performed += Dash;
    }

    private void OnDisable() {
        move.Disable();
        melee.Disable();
        shoot.Disable();
        dash.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDashing) {return;}

        moveDirection = move.ReadValue<Vector2>();

        if(!Mathf.Approximately(moveDirection.x, 0.0f) || !Mathf.Approximately(moveDirection.y, 0.0f))
        {
            lookDirection.Set(moveDirection.x, moveDirection.y);
            lookDirection.Normalize();
        }
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(moveDirection.x * moveSpd, moveDirection.y * moveSpd);

        if(isDashing) {return;}
    }

    private void Melee(InputAction.CallbackContext context) {
        Debug.Log("Player used melee");
    }

    private void Shoot(InputAction.CallbackContext context) {
        if(canCast) {
            StartCoroutine(castTimer(shootCooldown));
            Debug.Log("Player Shot projectile");

            GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300);

            // animator.SetTrigger("Launch");
            
            // PlaySound(throwSound);
        }
    }

    private void Dash(InputAction.CallbackContext context) {
        if(canDash) {
            StartCoroutine(dashTimer(dashCooldown));
            Debug.Log("Player dashed");


        }
    }

    IEnumerator castTimer(float timer){
        canCast = false;
        while(timer > 0){
            yield return null;
            timer -= Time.deltaTime;
        }
        canCast = true;
    }

    IEnumerator dashTimer(float timer){
        canDash = false;
        isDashing = true;
        rb.AddForce(lookDirection * dashPwr);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}

