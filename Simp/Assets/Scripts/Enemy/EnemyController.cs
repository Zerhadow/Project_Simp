using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 20f;
    public PlayerInputs enemyControls;

    Vector2 moveDirection = Vector2.zero;
    public Rigidbody2D toFollow;

    public Transform attackPoint;
    private float attackRange = 0.5f;
    public LayerMask playerLayer;
    
    void Start()
    {
        InvokeRepeating("MoveTowardsPlayer", 1.0f, 2.0f);
    }

    void Update()
    {
        moveDirection = toFollow.position - new Vector2(transform.position.x, transform.position.y);
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if(hitPlayer){
            Melee();
        }
    }

    private void MoveTowardsPlayer(){
        rb.velocity = moveDirection.normalized * moveSpeed;
    }

    private void FixedUpdate() {
        if(moveDirection.x > 0){
            gameObject.transform.localScale = new Vector2(1,1);
        }
        if(moveDirection.x < 0){
            gameObject.transform.localScale = new Vector2(-1,1);
        }
    }

    private void Melee() {
        Debug.Log("Player in range");
    }

    void OnDrawGizmosSelected(){
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

