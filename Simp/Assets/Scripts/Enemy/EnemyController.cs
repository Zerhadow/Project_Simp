using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 6f;
    public PlayerInputs enemyControls;

    Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction melee;

    private void Awake() {
        enemyControls = new PlayerInputs();
    }

    private void OnEnable() {
        move = enemyControls.Enemy.Move;
        move.Enable();

        melee = enemyControls.Enemy.Melee;
        melee.Enable();
        melee.performed += Melee;
    }

    private void OnDisable() {
        move.Disable();
        melee.Disable();
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
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void Melee(InputAction.CallbackContext context) {
        Debug.Log("Enemy used melee");
    }
}

