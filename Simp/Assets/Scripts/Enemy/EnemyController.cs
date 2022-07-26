using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveLength = 4f; //velocity scalar of movement dash
    public float attackLength = 20f; //velocity scalar of attack dash
    public float attackDrag = 5f; //drag modifier for attack dash
    public float moveFrequency = 1.5f; //frequency in seconds of movement dash
    public float attackPrepTime = 1f; //charging time in seconds before attack dash
    public float cooldownTime = 2f; //incactive time in seconds after attack dash
    public PlayerInputs enemyControls;

    Vector2 moveDirection = Vector2.zero;
    public Rigidbody2D toFollow;

    private bool attacking = false;

    public float attackRange = 4f; //range of vision for attack dash
    public LayerMask playerLayer;
    
    void Start()
    {
        InvokeRepeating("MoveTowardsPlayer", 1.0f, moveFrequency);
    }

    void Update()
    {
        moveDirection = toFollow.position - new Vector2(transform.position.x, transform.position.y);
        Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if(hitPlayer && !attacking){
            Debug.Log("attacking!");
            attacking = true;
            StartCoroutine(Attack());
        }
    }

    private void MoveTowardsPlayer(){
        rb.velocity = moveDirection.normalized * moveLength;
    }

    private void FixedUpdate() {
        if(!attacking){
            if(moveDirection.x > 0){
                gameObject.transform.localScale = new Vector2(1,1);
            }
            if(moveDirection.x < 0){
                gameObject.transform.localScale = new Vector2(-1,1);
            }
        }
    }

    private IEnumerator Attack() {
        CancelInvoke();
        Vector2 attackDirection = moveDirection.normalized;
        gameObject.GetComponent<Rigidbody2D>().drag = attackDrag;
        yield return new WaitForSeconds(attackPrepTime);
        rb.velocity = attackDirection * attackLength;
        yield return new WaitForSeconds(cooldownTime);
        gameObject.GetComponent<Rigidbody2D>().drag = 2f;
        attacking = false;
        gameObject.GetComponent<EnemyController>().enabled = false;
        gameObject.GetComponent<EnemyController>().enabled = true;
        Start();
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

