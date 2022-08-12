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
    private InputAction fire;
    private InputAction dash;

    private void Awake() {
        playerControls = new PlayerInputs();
    }

    private void OnEnable() {
        move = playerControls.Player.Move;
        move.Enable();

        melee = playerControls.Player.Melee;
        melee.Enable();
        melee.performed += Melee;

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;

        dash = playerControls.Player.Dash;
        dash.Enable();
        dash.performed += Dash;
    }

    private void OnDisable() {
        move.Disable();
        melee.Disable();
        fire.Disable();
        dash.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(moveDirection.x * moveSpd, moveDirection.y * moveSpd);
    }

    private void Melee(InputAction.CallbackContext context) {
        Debug.Log("Player used melee");
    }

    private void Fire(InputAction.CallbackContext context) {
        Debug.Log("Player Fired projectile");
    }

    private void Dash(InputAction.CallbackContext context) {
        Debug.Log("Player dashed");
    }
}

