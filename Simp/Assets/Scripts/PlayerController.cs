using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputs inputActions;
    Vector2 move;
    float dash;
    public float moveSpd = 3f;
    public float dashSpd = 20f;
    public Camera playerCamera;

    private void OnEnable() {
        inputActions.Player.Enable();
    }

    private void OnDisable() {
        inputActions.Player.Disable();
    }

    private void Awake() {
        inputActions = new PlayerInputs();

        inputActions.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => move = Vector2.zero;

        inputActions.Player.Move.performed += ctx => Dash();

    }

    private void Dash() {
        if(dash + 1f < Time.time)
            dash = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        dash = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(dash + 0.5f > Time.time) {
            // transform.Translate((Vector3.forward * 8f * Time.deltaTime), Space.Self);
        } else if (dash + 1f > Time.time) {
            // transform.Translate((Vector3.forward * 8f * Time.deltaTime), Space.Self);
        }
    }
}
